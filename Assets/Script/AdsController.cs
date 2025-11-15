
using SolarEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MaxSdkBase;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;
    DataManager _dataController;
    SoundController soundController;

    public string DevKeyAndroid;
    public string DevKeyIOS;

    public string bannerIdAndroid;
    public string interIdAndroid;
    public string videoIdAndroid;
    public string mrecIdAndroid;
    public string AppOpenAdId_Android;
    [Space]
    public string bannerIdIOS;
    public string interIdIOS;
    public string videoIdIOS;
    public string mrecIdIOS;
    public string AppOpenAdId_IOS;

    string bannerId;
    string interId;
    string videoId;
    string mrecId;
    string AppOpenAdUnitId;

    private bool firstLoading = false;
    string appIdTemp;

    bool loadbannerDone;
    bool doneWatchAds = false;
    [HideInInspector]public bool Showing_applovin_ads;
    bool showingBanner;
    bool showingMrec;
    bool bannerOK;

    private void Start()
    {
        if (_dataController == null)
        {
            _dataController = DataManager.instance;
            soundController = SoundController.instance;


#if UNITY_ANDROID
            appIdTemp = DevKeyAndroid;
            bannerId = bannerIdAndroid;
            interId = interIdAndroid;
            videoId = videoIdAndroid;
            mrecId = mrecIdAndroid;
            AppOpenAdUnitId = AppOpenAdId_Android;


#elif UNITY_IOS
                appIdTemp = DevKeyIOS;
                bannerId = bannerIdIOS;
                interId = interIdIOS;
                videoId = videoIdIOS;
                mrecId = mrecIdIOS;
                AppOpenAdUnitId = AppOpenAdId_IOS;
         
#endif
            Init();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        double revenue = impressionData.Revenue;
        var impressionParameters = new[] {
  new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
  new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
  new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
  new Firebase.Analytics.Parameter("ad_format", impressionData.AdFormat),
  new Firebase.Analytics.Parameter("value", revenue),
  new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
};
        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
    }
    public void Init()
    {

        MaxSdk.SetSdkKey(appIdTemp);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds();
            InitializeMRecAds();
            InitializeAppOpenAd();

            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;       
            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        
        };
    }
    #region Mrect

    bool mrecOk;
    public void InitializeMRecAds()
    {
        // MRECs are sized to 300x250 on phones and tablets
        MaxSdk.CreateMRec(mrecId, MaxSdkBase.AdViewPosition.Centered);

        MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
        MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdLoadFailedEvent;
        MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;
        MaxSdkCallbacks.MRec.OnAdExpandedEvent += OnMRecAdExpandedEvent;
        MaxSdkCallbacks.MRec.OnAdCollapsedEvent += OnMRecAdCollapsedEvent;
    }
    public void ShowMrec()
    {
        if (DataManager.instance.saveData.removeAds)
            return;
        showingMrec = true;
        MaxSdk.ShowMRec(mrecId);
    }
    public void HideMrec()
    {
        if (showingMrec)
        {
            MaxSdk.HideMRec(mrecId);
            showingMrec = false;
        }
    }
    public void ShowMrecCentered()
    {
        MaxSdk.UpdateMRecPosition(mrecId, MaxSdkBase.AdViewPosition.Centered);
        ShowMrec();
    }
    public void ShowMrecBottomCenter()
    {
        MaxSdk.UpdateMRecPosition(mrecId, MaxSdkBase.AdViewPosition.BottomCenter);
        ShowMrec();
    }

    public void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        mrecOk = true;
        Debug.LogError("====== load native success:");
    }

    public void OnMRecAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo error)
    {
        mrecOk = false;
        Debug.LogError("====== load native false:" + error.Message);
    }

    public void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    public void OnMRecAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    
    #endregion

    #region banner
    public void InitializeBannerAds()
    {
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += Banner_OnAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadedEvent += Banner_OnAdLoadedEvent;


#if UNITY_EDITOR

#else
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerId, colorBanner);
#endif
        //MaxSdk.SetBannerWidth(bannerId, 750);
    }

    private void Banner_OnAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        Debug.LogError("====load banner success ");
        bannerOK = true;
/*        if(showingBanner == false)
        {
            ShowBanner();
        }*/
    }

    private void Banner_OnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    {
        Debug.LogError("====load banner false ");
        bannerOK = false;
    }

    public void ShowBanner()
    {
        if (DataManager.instance.saveData.removeAds)
        {
            Debug.Log("remove ads = true || remote firebase off");
            return;
        }
            
        Debug.Log("=== show banner");
        if (bannerOK)
        {
            showingBanner = true;
            MaxSdk.ShowBanner(bannerId);
        }

    }
    public void HideBanner()
    {
        Debug.Log("=== hide banner");
        if (showingBanner)
        {
            MaxSdk.HideBanner(bannerId);
            showingBanner = false;
        }

    }
    public Color colorBanner;

    #endregion


    #region Inter
    public void InitializeInterstitialAds()
    {
        // Attach callback

        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += Interstitial_OnAdLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += Interstitial_OnAdLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += Interstitial_OnAdDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += Interstitial_OnAdDisplayFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += Interstitial_OnAdClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += Interstitial_OnAdHiddenEvent;

        RequestInter();
    }

    private void Interstitial_OnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    {
        throw new NotImplementedException();
    }

    private void Interstitial_OnAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        throw new NotImplementedException();
    }

    private void Interstitial_OnAdDisplayedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        
        //    EventController.AB_INTER_ID(DataParam.showInterType);

        MuteSoundAndMusic();

        Time.timeScale = 0;

        Debug.LogError("=== displayed inter");
    }

    private void Interstitial_OnAdHiddenEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        Time.timeScale = 1;
        ChangeSettingAudio();
        RequestInter();
        DataParam.beginShowInter = System.DateTime.Now;
        Showing_applovin_ads = false;
        EventController.SUM_INTER_ALL_GAME();
        Debug.LogError("=== bam' close inter");
    }

    private void Interstitial_OnAdClickedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        throw new NotImplementedException();
    }

    private void Interstitial_OnAdDisplayFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2, MaxSdkBase.AdInfo arg3)
    {
        throw new NotImplementedException();
    }


    private void OnInterstitialDismissedEvent(string adUnitId)
    {

    }

    public void RequestInter()
    {
        MaxSdk.LoadInterstitial(interId);
        //     IronSource.Agent.loadInterstitial();

    }

    public void CheckToShowInter()
    {
        if (MaxSdk.IsInterstitialReady(interId))
        {
            MaxSdk.ShowInterstitial(interId);
            Showing_applovin_ads = true;
        }
        else
        {
            RequestInter();
        }
    }

    public void ShowInter()
    {
        if (DataManager.instance.saveData.removeAds)
            return;
        DataParam.lastShowInter = DateTime.Now;

        if ((DataParam.lastShowInter - DataParam.beginShowInter).TotalSeconds > DataParam.timeDelayShowAds)
        {
            CheckToShowInter();
        }
    }
    #endregion


    #region reward
    int retryAttemptVideo;
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardDisplayedEvent;

        // Load the first rewarded ad
        RequestReward();
    }

    private void OnRewardDisplayedEvent(string arg1, MaxSdkBase.AdInfo info)
    {
        throw new NotImplementedException();
    }

    private void OnRewardedAdReceivedRewardEvent(string arg1, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo info)
    {
        doneWatchAds = true;
    }

    private void OnRewardedAdHiddenEvent(string arg1, MaxSdkBase.AdInfo info)
    {
        Showing_applovin_ads = false;
        ChangeSettingAudio();
        StartCoroutine(delayAction());
        // RequestVideo();
    }

    private void OnRewardedAdFailedToDisplayEvent(string arg1, MaxSdkBase.ErrorInfo info1, MaxSdkBase.AdInfo info2)
    {
        // display failed
    }

    private void OnRewardedAdFailedEvent(string arg1, MaxSdkBase.ErrorInfo info)
    {
        // load failed
    }

    private void OnRewardedAdLoadedEvent(string arg1, MaxSdkBase.AdInfo info)
    {
        Debug.LogError("========= video load sucess");
    }

    public void RequestReward()
    {
        //   IronSource.Agent.init(appIdTemp, IronSourceAdUnits.REWARDED_VIDEO);
        MaxSdk.LoadRewardedAd(videoId);
    }

    IEnumerator delayAction()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (doneWatchAds)
        {
            acreward();
            EventController.SUM_VIDEO_SHOW_NAME(nameEventVideo);
            DataParam.countShowVideo++;
        }
        RequestReward();
        // Debug.LogError("====== close video");
        acreward = null;

        doneWatchAds = false;
        Time.timeScale = 1;
    }
    Action acreward;
    string nameEventVideo;
    public void ShowReward(Action _ac, string name)
    {
        if (MaxSdk.IsRewardedAdReady(videoId))
        {
            acreward = _ac;
            doneWatchAds = false;
            nameEventVideo = name;
            MaxSdk.ShowRewardedAd(videoId);
            Time.timeScale = 0;
            MuteSoundAndMusic();

            //DataParam.afterShowAds = true;
            Showing_applovin_ads = true;
            // Debug.LogError("------ video show video");
        }
        else
        {
            //   Debug.LogError("------ video chua load");
            RequestReward();
        }
    }
    #endregion

    #region AppOpenAd
    private void InitializeAppOpenAd()
    {
        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
        MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += AppOpen_OnAdDisplayedEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += AppOpen_OnAdLoadedEvent;
        MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += AppOpen_OnAdLoadFailedEvent;

        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    private void AppOpen_OnAdLoadFailedEvent(string arg1, ErrorInfo arg2)
    {
        Debug.LogError("AppOpen ad load failed");
    }

    private void AppOpen_OnAdLoadedEvent(string arg1, AdInfo arg2)
    {
        Debug.Log("AppOpen ad load success!");
        //throw new NotImplementedException();
    }

    public void AppOpen_OnAdDisplayedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    {
        Time.timeScale = 0;
        MuteSoundAndMusic();
        string adFormat = arg2.AdFormat;
        Debug.Log($"Max AppOpen Ad displayed with format: {adFormat}");
    }

    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        ChangeSettingAudio();
        Time.timeScale = 1;
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        Debug.Log($"Max AppOpen Ad Dismissed with format");
    }
    private void OnApplicationPause(bool pauseStatus)
    {

        if (!pauseStatus)
        {
            Debug.Log("OnApplicationPause " + pauseStatus);
            if (firstLoading)
            {
                Debug.Log("call show appopen");
                ShowAppOpenAdIfReady();
            }

        }
    }
    public void ShowAppOpenAdIfReady()
    {
        //if (DataParam.ShowOpenAds == false)
        //    return;
        if (Showing_applovin_ads == true)
        {
            Debug.LogError("video ads not closed => dont show appopen");
            return;
        }
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }
    }
    public void ShowAppOpenFirst()
    {
        Debug.Log("call show app open first");
        if (firstLoading == false)
        {
            if(DataManager.instance.saveData.FirstOpenGame == false)
            {
                Debug.Log("dont show app open in first open game");
                DataManager.instance.saveData.FirstOpenGame = true;
            }
            else
            {
                ShowAppOpenAdIfReady();
            }
           
            firstLoading = true;
        }
        else
        {

        }
    }

    #endregion
    void MuteSoundAndMusic()
    {
        soundController.MuteAllMusic();
        soundController.MuteAllSound();
    }
    void ChangeSettingAudio()
    {
        soundController.ChangeSettingMusic();
        soundController.ChangeSettingSound();
    }
    
}
