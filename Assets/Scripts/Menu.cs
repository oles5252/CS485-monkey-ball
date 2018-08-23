using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public List<Text> menuItems;

    public List<string> scenes;

    public Text level;
    public Text levelbg;

    public int pos = 0;

    public int continueLevel = 1;

    public int maxUnlocked = 1;

    public GameObject pointer;

    Vector3 pointerPos;

    //JOYCON GYRO CONTROLS
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_ind = 0;
    bool stickReset;

    private void Start()
    {
        menuItems[pos].color = Color.red;

        maxUnlocked = PlayerPrefs.GetInt("maxUnlocked", 1);
        continueLevel = maxUnlocked;
        level.text = continueLevel.ToString();
        levelbg.text = continueLevel.ToString();

        //JOYCONS START
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null)
        {
            joycons = JoyconManager.Instance.j;
        }
    }

    private void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            // stick position values
            stick = j.GetStick(); //element 0 is x, element 1 is y
            if (j.GetButtonDown(Joycon.Button.DPAD_UP) || (stick[1] > 0.7 && stickReset))
            {
                if (pos > 0)
                {
                    menuItems[pos].color = Color.white;
                    pos--;
                    pointerPos = pointer.transform.position;
                    pointerPos.y = menuItems[pos].transform.position.y - 15;
                    pointer.transform.position = pointerPos;
                    menuItems[pos].color = Color.red;
                    stickReset = false;
                }
            }
            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN) || (stick[1] < -0.7 && stickReset))
            {
                if (pos < menuItems.Count - 1)
                {
                    menuItems[pos].color = Color.white;
                    pos++;
                    pointerPos = pointer.transform.position;
                    pointerPos.y = menuItems[pos].transform.position.y - 15;
                    pointer.transform.position = pointerPos;
                    menuItems[pos].color = Color.red;
                    stickReset = false;
                }
            }
            if (stick[1] < 0.2 && stick[1] > -0.2)
            {
                stickReset = true;
            }
            if (j.GetButtonDown(Joycon.Button.SHOULDER_1) || j.GetButtonDown(Joycon.Button.SHOULDER_2) || j.GetButtonDown(Joycon.Button.STICK))
            {
                j.SetRumble(160, 320, 0.6f, 200);
                if (menuItems[pos].name == "New Game")
                {
                    PlayerPrefs.SetInt("currentLives", 3);
                    PlayerPrefs.SetFloat("currentTime", 0.0f);
                    SceneManager.LoadScene(scenes[0]);
                }
                if (menuItems[pos].name == "Continue")
                {
                    SceneManager.LoadScene(scenes[int.Parse(level.text)]);
                }
                if (menuItems[pos].name == "Options")
                {
                    SceneManager.LoadScene("Options");
                }
                if (menuItems[pos].name == "Exit")
                {
                    Application.Quit();
                }
                if (menuItems[pos].name == "Back")
                {
                    SceneManager.LoadScene("Menu");
                }
                if (menuItems[pos].name == "FOV")
                {

                }
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (pos < menuItems.Count - 1)
                {
                    menuItems[pos].color = Color.white;
                    pos++;
                    pointerPos = pointer.transform.position;
                    pointerPos.y = menuItems[pos].transform.position.y - 15;
                    pointer.transform.position = pointerPos;
                    menuItems[pos].color = Color.red;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (pos > 0)
                {
                    menuItems[pos].color = Color.white;
                    pos--;
                    pointerPos = pointer.transform.position;
                    pointerPos.y = menuItems[pos].transform.position.y - 15;
                    pointer.transform.position = pointerPos;
                    menuItems[pos].color = Color.red;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(continueLevel > 1 && menuItems[pos].name == "Continue")
                {
                    continueLevel--;
                    level.text = continueLevel.ToString();
                    levelbg.text = continueLevel.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (continueLevel < maxUnlocked && menuItems[pos].name == "Continue")
                {
                    continueLevel++;
                    level.text = continueLevel.ToString();
                    levelbg.text = continueLevel.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (menuItems[pos].name == "New Game")
                {
                    PlayerPrefs.SetInt("currentLives", 3);
                    PlayerPrefs.SetFloat("currentTime", 0.0f);
                    SceneManager.LoadScene(scenes[0]);
                }
                if (menuItems[pos].name == "Continue")
                {
                    SceneManager.LoadScene(scenes[int.Parse(level.text)]);
                }
                if (menuItems[pos].name == "Options")
                {
                    SceneManager.LoadScene("Options");
                }
                if (menuItems[pos].name == "Exit")
                {
                    Application.Quit();
                }
                if (menuItems[pos].name == "Back")
                {
                    SceneManager.LoadScene("Menu");
                }
                if (menuItems[pos].name == "FOV")
                {

                }
            }
        }
    }
}
