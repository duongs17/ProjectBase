using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : Singleton<UICanvasManager>
{
    public UICanvas[] uiResources;
    public Transform CanvasParent;
    public T OpenPopUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetPopUPUI<T>();

        canvas.OnOpen();

        return canvas as T;
    }

    //close UI directly
    //dong UI canvas ngay lap tuc
    public void ClosePopUI<T>() where T : UICanvas
    {
        
            GetPopUPUI<T>().OnClose();
        
    }
    public T GetPopUPUI<T>() where T : UICanvas
    {
        for (int i = 0; i < uiResources.Length; i++)
        {
            if (uiResources[i] is T)
            {
                return uiResources[i] as T;
            }
        }
        Debug.LogError("cant get T ");
        return null;
    }
}
