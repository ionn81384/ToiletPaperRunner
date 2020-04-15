

using System;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameData : MonoBehaviour
{
    // Put here all the data to use
    [SerializeField]
    public class Data
    {
        // Score
        public int bestScore = 0;
        public int score = 0;

        public int toiletPaper = 0;

        // Purchases
        public bool[] isSkinUnlocked;
        public int currentSkin;
        public bool isAddOn = true;
        public bool didLogin = false;

        
        // TimeOut
        public DateTime lastsave = DateTime.Now;

        // config
        public double clickHouseHold = 1;
        public bool musicOn = true;
        public bool soundOn = true;

        public bool isReload = false;


    }

    public static Data d = new Data();
    // TO be able to show in inspector, terrible code but hell with it
    [SerializeField]

    public static GameData instance = null;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // If sucess then keep it
        DontDestroyOnLoad(this.gameObject);
        LoadGame();
    }



    #region saveLoad

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void LoadGame()
    {
        LoadAsJSON();
    }

    public void SaveGame()
    {
        SaveAsJSON();
    }

    private void SaveAsJSON()
    {
        d.lastsave = DateTime.Now;
        string json = JsonUtility.ToJson(d);

        PlayerPrefs.SetString("save1", json);
        Debug.Log("Saving as JSON: " + json);
    }

    private void LoadAsJSON()
    {
        string json = PlayerPrefs.GetString("save1", "");
        if (json == "")
        {
            Debug.Log("Error on load as JSON:");
            return; // bail out or load default
        }
        Data save = JsonUtility.FromJson<Data>(json);
        d = save; // Not sure mono behaviour is going toacept this
        Debug.Log("Loaded as JSON: " + json);
        AudioListener.pause = !GameData.d.soundOn;
    }

    private void OnApplicationQuit()
    {
        SaveAsJSON();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveAsJSON();
    }

    #endregion saveLoad
}