using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMeow : MonoBehaviour {

    private AudioSource meow;

    private void Start()
    {
        meow = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "keepInside")
        {
            print(collision.impulse.magnitude);
            if (Mathf.Abs(collision.impulse.magnitude) > 0.5 && collision.impulse.magnitude != 0)
            {
                if (!meow.isPlaying)
                {
                    meow.Play();
                }
            }
        }
    }
}
