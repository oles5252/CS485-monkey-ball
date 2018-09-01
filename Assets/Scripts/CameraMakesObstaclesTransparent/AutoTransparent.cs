//https://answers.unity.com/questions/44815/make-object-transparent-when-between-camera-and-pl.html

using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour
{

    private Material[] oldMaterials = null;

    private float m_Transparency = 1.0f;
    public float TargetTransparency = 1.0f;

    public float FadeInTimeout = 1.0f;

    public float FadeOutTimeout = 2.0f;

    public Material TransparentMaterial;

    private bool shouldBeTransparent = true;

    public void BeTransparent()
    {
        shouldBeTransparent = true;
    }

    private void Start()
    {

        // reset the transparency;
        m_Transparency = 1.0f;

        if (oldMaterials == null)
        {
            // Save the current materials
            oldMaterials = GetComponent<Renderer>().materials;

            Material[] materialsList = new Material[oldMaterials.Length];

            for (int i = 0; i < materialsList.Length; i++)
            {
                // repalce material with transparent
                materialsList[i] = Object.Instantiate(TransparentMaterial);
                materialsList[i].SetColor("_Color", oldMaterials[i].GetColor("_Color"));
            }

            // make transparent
            GetComponent<Renderer>().materials = materialsList;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!shouldBeTransparent && m_Transparency >= 1.0)
        {
            Destroy(this);
        }

        //Are we fading in our out?
        if (shouldBeTransparent)
        {
            //Fading out
            if (m_Transparency > TargetTransparency)
            {
                m_Transparency -= ((1.0f - TargetTransparency) * Time.deltaTime) / FadeOutTimeout;
            }
        }
        else
        {
            //Fading in
            m_Transparency += ((1.0f - TargetTransparency) * Time.deltaTime) / FadeInTimeout;
        }

        Material[] materialsList = GetComponent<Renderer>().materials;
        for (int i = 0; i < materialsList.Length; i++)
        {
            Color C = oldMaterials[i].GetColor("_Color");

            C.a = m_Transparency;
            materialsList[i].color = C;
        }

        //The object will start to become visible again if BeTransparent() is not called
        shouldBeTransparent = false;
    }

    private void OnDestroy()
    {
        // restore old materials
        GetComponent<Renderer>().materials = oldMaterials;
    }
}