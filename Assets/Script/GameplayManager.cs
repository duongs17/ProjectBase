using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    // Start is called before the first frame update
    void Start()
    {
        UICanvasManager.Ins.OpenPopUI<PlayGameUI>();
        UICanvasManager.Ins.ClosePopUI<WinUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
