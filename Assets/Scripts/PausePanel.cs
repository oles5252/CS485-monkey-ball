using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{

    public GameObject pausePanel;
    public AudioSource pauseSound;
    public AudioSource movementSound;

    //JOYCON GYRO CONTROLS
    private List<Joycon> joycons;
    public int jc_ind = 0;

    void Start()
    {
        pausePanel = GameObject.Find("Pause");
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        //JOYCONS START
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null)
        {
            joycons = JoyconManager.Instance.j;
        }
    }
    void Update()
    {
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            if(j.GetButtonDown(Joycon.Button.MINUS) || j.GetButtonDown(Joycon.Button.PLUS))
            {
                if (!pausePanel.activeInHierarchy)
                {
                    movementSound.Stop();
                    pauseSound.Play();
                    Time.timeScale = 0;
                    pausePanel.SetActive(true);
                }
                else
                {
                    pauseSound.Play();
                    Time.timeScale = 1;
                    pausePanel.SetActive(false);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pausePanel.activeInHierarchy)
                {
                    movementSound.Stop();
                    pauseSound.Play();
                    Time.timeScale = 0;
                    pausePanel.SetActive(true);
                }
                else
                {
                    pauseSound.Play();
                    Time.timeScale = 1;
                    pausePanel.SetActive(false);
                }
            }
        }
    }
}