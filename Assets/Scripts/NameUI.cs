using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class NameUI : MonoBehaviour
{
    public Text entryText;
    public string playerName;
    public SavedName sName;
    //public NameManager nameM;

    // Start is called before the first frame update
    void Start()
    {
        entryText = GameObject.Find("Player Name").GetComponent<Text>();
        sName = GameObject.Find("Player Save Name").GetComponent<SavedName>();
        //nameM = GameObject.Find("NameManager").GetComponent<NameManager>();
        LoadName();

    }

    // Update is called once per frame
    void Update()
    { }

    [System.Serializable]
    class SaveData
    {
        public string savedPlayerName;
    }

    public void SaveName()
    {
        playerName = entryText.text;
        SaveData data = new SaveData();
        data.savedPlayerName = playerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Display new name:
        sName.pName.text = playerName;
        //nameM.pName.text = playerName;
        NameManager.Instance.pName = playerName;
        Debug.Log("Name saved");
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.savedPlayerName;
            sName.pName.text = playerName;
            //nameM.pName.text = playerName;
            NameManager.Instance.pName = playerName;
            Debug.Log("Name loaded");
        }
    }

    //Load main scene:
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
}
