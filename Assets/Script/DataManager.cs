using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class SaveData
{
    public bool offmusic, offsound, offvibra, removeAds;
    public int currentLevel, session, highLevel, Coin;
    public bool FirstOpenGame;
}

public class DataManager : MonoBehaviour
{
    public SaveData saveData;
    public static DataManager instance;


    private void Start()
    {
    }

    private void Awake()
    {
        if (instance == null)
        {
            Application.targetFrameRate = 300;
            CultureInfo ci = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllData();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void LoadAllData()
    {
        saveData = new SaveData();
        string jsonSaved = string.Empty;
        jsonSaved = PlayerPrefs.GetString(DataParam.SAVEDATA);
        if (!string.IsNullOrEmpty(jsonSaved) && jsonSaved != "" && jsonSaved != "[]")
        {
            var jData = JsonMapper.ToObject(jsonSaved);
            if (jData != null)
            {
                saveData = JsonMapper.ToObject<SaveData>(jData.ToJson());
            }
        }
        DataParam.beginShowInter = DataParam.lastShowInter = System.DateTime.Now;

        saveData.session++;
    }

    void SaveData()
    {
        var tempsaveData = JsonMapper.ToJson(saveData);
        PlayerPrefs.SetString(DataParam.SAVEDATA, tempsaveData);
        PlayerPrefs.Save();
        //Debug.LogError("save");
    }
    void OnApplicationQuit()
    {
        SaveData();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
        }
    }
    public void RemoveAdsFunc()
    {
        saveData.removeAds = true;
        //GameController.gameController.settingPopUp.DisplayBtn();
        AdsController.instance.HideBanner();
        AdsController.instance.HideMrec();
    }
    
}
