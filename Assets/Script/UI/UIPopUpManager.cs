using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUpManager : Singleton<UIPopUpManager>
{
    public UIPopUp[] uiResources;
    public Transform CanvasParent;
    public T OpenPopUI<T>() where T : UIPopUp
    {
        UIPopUp canvas = GetPopUPUI<T>();

        canvas.OnOpen();

        return canvas as T;
    }

    //close UI directly
    //dong UI canvas ngay lap tuc
    public void ClosePopUI<T>() where T : UIPopUp
    {

        GetPopUPUI<T>().OnClose();

    }
    public T GetPopUPUI<T>() where T : UIPopUp
    {


        //popUpCanvas[typeof(T)] = canvas;

        for(int i=0;i< uiResources.Length; i++)
        {
            if(uiResources[i] is T)
            {
                return uiResources[i] as T;
            }
        }
        Debug.LogError("cant get T ");
        return null;
    }
}
