using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using Firebase;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;

[System.Serializable]
public class RemoteDefaultInfo
{
    public string key, value;
}
public class EventController : MonoBehaviour
{
    public static bool fireBaseInitDone
    {
        get
        {
            return RemoteConfigFb.fireBaseInitDone;
        }
    }

   
    public static void SUM_VIDEO_SHOW_NAME(string value)
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("video_show_all_game_para", "video_showed_" + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("video_show_all_game", param);
        }
    }
    static string nameTempParam;
    
    
    public static void SUM_INTER_ALL_GAME()
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("inter_show_all_game_para", "inter_show_all_game_value");
            Firebase.Analytics.FirebaseAnalytics.LogEvent("inter_show_all_game", param);
        }
    }

    public static void SUM_OPENADS_ALL_GAME()
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("openads_show_all_game_para", "openads_show_all_game_value");
            Firebase.Analytics.FirebaseAnalytics.LogEvent("sum_app_open", param);
        }
    }
    public static void PLAY_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("play_level_para", "play_level_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("play_level_event", param);

        }
    }
    public static void WIN_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("win_level_para", "win_level_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("win_level_event", param);
        }
    }
   

    public static void TUTORIAL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("tutorial_para", "tutorial_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("tutorial_event", param);
        }
    }

    public static void LOSE_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("lose_level_para", "lose_level_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("lose_level_event", param);
        }
    }
    public static void REMOVE_ADS(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("remove_ads_para", "removeads");
            Firebase.Analytics.FirebaseAnalytics.LogEvent("remove_ads", param);
        }
    }
    

}
