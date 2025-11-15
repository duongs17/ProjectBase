using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Sample;
public class Test : MonoBehaviour
{
    public int iStart, iEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //BannerViewController.instance.ShowAd();
            AppOpenAdController.instance.ShowAppOpenAd();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //.instance.LoadAd();
        }
    }
    public void LoadCollapBanner()
    {
        BannerViewController.instance.LoadAd();
    }
    public void ShowBanner()
    {
        AdsController.instance.ShowBanner();
    }
    public void ShowInter()
    {
        AdsController.instance.ShowInter();
    }
    public void ShowReward()
    {
        AdsController.instance.ShowReward(Reward, "");
    }
    private void Reward()
    {

    }
}
