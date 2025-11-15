using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameUI : UICanvas
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBtnWin()
    {
        UICanvasManager.Ins.OpenPopUI<WinUI>();
    }
    public void OnBtnSetting()
    {
        UIPopUpManager.Ins.OpenPopUI<SettingPopUp>();
    }
}
