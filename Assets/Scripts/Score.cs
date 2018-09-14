using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveData
{
    public List<float> scores = new List<float>();


    /// <summary>
    /// Writes the instance of this class to the specified file in JSON format.
    /// </summary>
    /// <param name="filePath">The file name and full path to write to.</param>
    public void WriteToFile(string filePath)
    {
        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        string json = JsonUtility.ToJson(this, true);

        // Write that JSON string to the specified file.
        File.WriteAllText(filePath, json);
    }


    /// <summary>
    /// Returns a new SaveData object read from the data in the specified file.
    /// </summary>
    /// <param name="filePath">The file to attempt to read from.</param>
    public static SaveData ReadFromFile(string filePath)
    {
        // If the file doesn't exist then just return the default object.
        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new SaveData();
        }
        else
        {
            // If the file does exist then read the entire file to a string.
            string contents = File.ReadAllText(filePath);

            // If it happens that the file is somehow empty then tell us and return a new SaveData object.
            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new SaveData();
            }

            // Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
            return JsonUtility.FromJson<SaveData>(contents);
        }
    }

}



public class Score : MonoBehaviour {

    public GameObject scrollbar;
    private Scrollbar scroll;

    public GameObject content;

    public GameObject scorePrefab;

    private string gameDataFileName = "data.json";

    private SaveData saveData;

    private string SAVE_DIR;

    private string SAVE_FILE;

    private float timer;

    //JOYCON GYRO CONTROLS
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_ind = 0;
    bool stickReset;


    // Use this for initialization
    void Start () {

        SAVE_DIR = Application.dataPath + "/saves/";

        SAVE_FILE = Application.dataPath + "/saves/" + "scores";

        scroll = scrollbar.GetComponent<Scrollbar>();

        timer = PlayerPrefs.GetFloat("currentTime", 0.0f);

        if (Directory.Exists(SAVE_DIR))
        {
            if (File.Exists(SAVE_FILE))
            {
                saveData = SaveData.ReadFromFile(SAVE_FILE);
                saveData.scores.Add(timer);
                saveData.scores.Sort();
                saveData.WriteToFile(SAVE_FILE);
                for(int i = 0; i < saveData.scores.Count; i++)
                {
                    GameObject scoreElement = Instantiate(scorePrefab);
                    scoreElement.transform.SetParent(content.transform, false);
                    float min = Mathf.Floor(saveData.scores[i] / 60);
                    float sec = Mathf.Floor(saveData.scores[i] - min * 60);
                    scoreElement.GetComponentInChildren<Text>().text = min + " min " + sec + " sec";
                }
            }
            else
            {
                saveData = new SaveData();
                saveData.scores.Add(timer);
                saveData.WriteToFile(SAVE_FILE);
                GameObject scoreElement = Instantiate(scorePrefab);
                scoreElement.transform.SetParent(content.transform, false);
                float min = Mathf.Floor(saveData.scores[0] / 60);
                float sec = Mathf.Floor(saveData.scores[0] - min * 60);
                scoreElement.GetComponentInChildren<Text>().text = min + " min " + sec + " sec";
            }
        }
        else
        {
            Directory.CreateDirectory(SAVE_DIR);
            saveData = new SaveData();
            saveData.scores.Add(timer);
            saveData.WriteToFile(SAVE_FILE);
            GameObject scoreElement = Instantiate(scorePrefab);
            scoreElement.transform.SetParent(content.transform, false);
            float min = Mathf.Floor(saveData.scores[0] / 60);
            float sec = Mathf.Floor(saveData.scores[0] - min * 60);
            scoreElement.GetComponentInChildren<Text>().text = min + " min " + sec + " sec";
        }

        //JOYCONS START
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null)
        {
            joycons = JoyconManager.Instance.j;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            // stick position values
            stick = j.GetStick(); //element 0 is x, element 1 is y
            if (j.GetButton(Joycon.Button.DPAD_UP) || (stick[1] > 0.7))
            {
                scroll.value += 0.05f;
            }
            if (j.GetButton(Joycon.Button.DPAD_DOWN) || (stick[1] < -0.7))
            {
                scroll.value -= 0.05f;
            }
            //if (stick[1] < 0.2 && stick[1] > -0.2)
           // {
           //     stickReset = true;
           // }

            if (j.GetButtonDown(Joycon.Button.SHOULDER_1) || j.GetButtonDown(Joycon.Button.SHOULDER_2) || j.GetButtonDown(Joycon.Button.STICK) || j.GetButtonDown(Joycon.Button.PLUS) || j.GetButtonDown(Joycon.Button.MINUS))
            {
                j.SetRumble(160, 320, 0.6f, 200);
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                scroll.value -= 0.05f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                scroll.value += 0.05f;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        
    }
}
