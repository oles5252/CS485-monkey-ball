using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour {

    public Text levelName;

    public Vector3 maxScale = new Vector3(1, 1, 1);

    public float fadeSpeed = 0.9f;

    public string[] levels;

    private int currLevel = 0;

    static SceneHandler instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StopCoroutine("Fade");
        if(scene.name != "Menu" && scene.name != "Options")
        {
            levelName.transform.localScale = maxScale;
            levelName.text = scene.name;
        }
        StartCoroutine("Fade");

    }

    IEnumerator Fade()
    {
        float i = 1;
        while(i > 0.01) {
        //for (float i = 1; i > 0.01; i -= fadeSpeed)
        //{
            if(i < 0.05)
            {
                levelName.text = "";
            }
            else
            {
                levelName.transform.localScale = new Vector3(i, i, 1);
            }
            i -= Time.deltaTime / 2;
            yield return null;
        }
    }

    public void loadLevel(int levelNum)
    {
        SceneManager.LoadScene(levels[levelNum]);
        currLevel = levelNum;
    }

    public void loadNextLevel()
    {
        currLevel++;
        SceneManager.LoadScene(levels[currLevel]);
    }


}
