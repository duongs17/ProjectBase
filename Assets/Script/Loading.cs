using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    public Image LoadingImage;
    //public Text text;
    public float speedLoad = 0.5f;
    float percent;
    float ValueLoad;

    public static bool firstTime;

    // load type 2
    private void Start()
    {
        //DataManager.instance.saveData.session++;
        
        StartCoroutine(Load_IE());
    }
    public void LoadNow()
    {
        StartCoroutine(Load_IE());
    }
    IEnumerator Load_IE()
    {
        yield return new WaitForSeconds(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;


        float progress = 0;
        while (!operation.isDone)
        {
            progress = Mathf.MoveTowards(progress, operation.progress, speedLoad * Time.deltaTime);
            LoadingImage.fillAmount = progress;
            if (progress >= 0.9f)
            {
                LoadingImage.fillAmount = 1;
                //DataParam.LoadInfoLevel();
                firstTime = true;
                operation.allowSceneActivation = true;
            }
            //Debug.LogError("is loading :"+ asyncOperation.progress);
            yield return null;
        }
    }
}