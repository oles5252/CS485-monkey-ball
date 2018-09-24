using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{

    int remaining;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            remaining = PlayerPrefs.GetInt("currentLives") - 1;
            if (remaining > 0)
            {
                PlayerPrefs.SetInt("currentLives", remaining);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene("Menu");
                //later this will go to game over
            }
        }
    }
}
