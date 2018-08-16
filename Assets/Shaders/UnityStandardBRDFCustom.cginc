// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

#ifndef UNITY_STANDARD_BRDF_INCLUDED
#define UNITY_STANDARD_BRDF_INCLUDED

#include "UnityCG.cginc"
#include "UnityStandardConfig.cginc"
#include "UnityLightingCommon.cginc"

//-----------------------------------------------------------------------------
// Helper to convert smoothness to roughness
//-----------------------------------------------------------------------------

float PerceptualRoughnessToRoughness(float perceptualRoughness)
{
	return perceptualRoughness * perceptualRoughness;
}

half RoughnessToPerceptualRoughness(half roughness)
{
	return sqrt(roughness);
}

inline half Pow4(half x)
{
	return x*x*x*x;
}

inline float2 Pow4(float2 x)
{
	return x*x*x*x;
}

inline half3 Pow4(half3 x)
{
	return x*x*x*x;
}

inline half4 Pow4(half4 x)
{
	return x*x*x*x;
}

// Pow5 uses the same amount of instructions as generic pow(), but has 2 advantages:
// 1) better instruction pipelining
// 2) no need to worry about NaNs
inline half Pow5(half x)
{
	return x*x * x*x * x;
}

inline half2 Pow5(half2 x)
{
	return x*x * x*x * x;
}

inline half3 Pow5(half3 x)
{
	return x*x * x*x * x;
}

inline half4 Pow5(half4 x)
{
	return x*x * x*x * x;
}

// approximage Schlick with ^4 instead of ^5
inline half3 FresnelLerpFast(half3 F0, half3 F90, half cosA)
{
	half t = Pow4(1 - cosA);
	return lerp(F0, F90, t);
}

// NOTE: Visibility term here is the full form from Torrance-Sparrow model, it includes Geometric term: V = G / (N.L * N.V)
// This way it is easier to swap Geometric terms and more room for optimizations (except maybe in case of CookTorrance geom term)

// Generic Smith-Schlick visibility term
inline half SmithVisibilityTerm(half NdotL, half NdotV, half k)
{
	half gL = NdotL * (1 - k) + k;
	half gV = NdotV * (1 - k) + k;
	return 1.0 / (gL * gV + 1e-5f); // This function is not intended to be running on Mobile,
									// therefore epsilon is smaller than can be represented by half
}

inline half PerceptualRoughnessToSpecPower(half perceptualRoughness)
{
	half m = PerceptualRoughnessToRoughness(perceptualRoughness);   // m is the true academic roughness.
	half sq = max(1e-4f, m*m);
	half n = (2.0 / sq) - 2.0;                          // https://dl.dropboxusercontent.com/u/55891920/papers/mm_brdf.pdf
	n = max(n, 1e-4f);                                  // prevent possible cases of pow(0,0), which could happen when roughness is 1.0 and NdotH is zero
	return n;
}

// Smoothness is the user facing name
// it should be perceptualSmoothness but we don't want the user to have to deal with this name
half SmoothnessToRoughness(half smoothness)
{
	return (1 - smoothness) * (1 - smoothness);
}

float SmoothnessToPerceptualRoughness(float smoothness)
{
	return (1 - smoothness);
}

inline float3 Unity_SafeNormalize(float3 inVec)
{
	float dp3 = max(0.001f, dot(inVec, inVec));
	return inVec * rsqrt(dp3);
}

// Based on Minimalist CookTorrance BRDF
// Implementation is slightly different from original derivation: http://www.thetenthplanet.de/archives/255
//
// * NDF (depending on UNITY_BRDF_GGX):
//  a) BlinnPhong
//  b) [Modified] GGX
// * Modified Kelemen and Szirmay-​Kalos for Visibility term
// * Fresnel approximated with 1/LdotH
half4 BRDF_Unity_Toon(half3 diffColor, half3 specColor, half oneMinusReflectivity, half smoothness,
	float3 normal, float3 viewDir,
	UnityLight light, UnityIndirect gi)
{
	float3 halfDir = Unity_SafeNormalize(float3(light.dir) + viewDir);

	half nl = smoothstep(0.0,0.1, saturate(dot(normal, light.dir)));
	float nh = saturate(dot(normal, halfDir));
	half nv = saturate(dot(normal, viewDir));
	float lh = saturate(dot(light.dir, halfDir));

	// Specular term
	half perceptualRoughness = SmoothnessToPerceptualRoughness(smoothness);
	half roughness = PerceptualRoughnessToRoughness(perceptualRoughness);

#if UNITY_BRDF_GGX

	// GGX Distribution multiplied by combined approximation of Visibility and Fresnel
	// See "Optimizing PBR for Mobile" from Siggraph 2015 moving mobile graphics course
	// https://community.arm.com/events/1155
	half a = roughness;
	float a2 = a*a;

	float d = nh * nh * (a2 - 1.f) + 1.00001f;
#ifdef UNITY_COLORSPACE_GAMMA
	// Tighter approximation for Gamma only rendering mode!
	// DVF = sqrt(DVF);
	// DVF = (a * sqrt(.25)) / (max(sqrt(0.1), lh)*sqrt(roughness + .5) * d);
	float specularTerm = a / (max(0.32f, lh) * (1.5f + roughness) * d);
#else
	float specularTerm = a2 / (max(0.1f, lh*lh) * (roughness + 0.5f) * (d * d) * 4);
#endif

	// on mobiles (where half actually means something) denominator have risk of overflow
	// clamp below was added specifically to "fix" that, but dx compiler (we convert bytecode to metal/gles)
	// sees that specularTerm have only non-negative terms, so it skips max(0,..) in clamp (leaving only min(100,...))
#if defined (SHADER_API_MOBILE)
	specularTerm = specularTerm - 1e-4f;
#endif

#else

	// Legacy
	half specularPower = PerceptualRoughnessToSpecPower(perceptualRoughness);
	// Modified with approximate Visibility function that takes roughness into account
	// Original ((n+1)*N.H^n) / (8*Pi * L.H^3) didn't take into account roughness
	// and produced extremely bright specular at grazing angles

	half invV = lh * lh * smoothness + perceptualRoughness * perceptualRoughness; // approx ModifiedKelemenVisibilityTerm(lh, perceptualRoughness);
	half invF = lh;

	half specularTerm = ((specularPower + 1) * pow(nh, specularPower)) / (8 * invV * invF + 1e-4h);

#ifdef UNITY_COLORSPACE_GAMMA
	specularTerm = sqrt(max(1e-4f, specularTerm));
#endif

#endif

#if defined (SHADER_API_MOBILE)
	specularTerm = clamp(specularTerm, 0.0, 100.0); // Prevent FP16 overflow on mobiles
#endif
#if defined(_SPECULARHIGHLIGHTS_OFF)
	specularTerm = 0.0;
#endif

	// surfaceReduction = Int D(NdotH) * NdotH * Id(NdotL>0) dH = 1/(realRoughness^2+1)

	// 1-0.28*x^3 as approximation for (1/(x^4+1))^(1/2.2) on the domain [0;1]
	// 1-x^3*(0.6-0.08*x)   approximation for 1/(x^4+1)
#ifdef UNITY_COLORSPACE_GAMMA
	half surfaceReduction = 0.28;
#else
	half surfaceReduction = (0.6 - 0.08*perceptualRoughness);
#endif

	surfaceReduction = 1.0 - roughness*perceptualRoughness*surfaceReduction;

	half grazingTerm = saturate(smoothness + (1 - oneMinusReflectivity));
	half3 color = (diffColor + specularTerm * specColor) * light.color * nl
		+ gi.diffuse * diffColor
		+ surfaceReduction * gi.specular * FresnelLerpFast(specColor, grazingTerm, nv);

	return half4(color, 1);
}

// Include deprecated function
#define INCLUDE_UNITY_STANDARD_BRDF_DEPRECATED
#include "UnityDeprecated.cginc"
#undef INCLUDE_UNITY_STANDARD_BRDF_DEPRECATED

#endif // UNITY_STANDARD_BRDF_INCLUDED