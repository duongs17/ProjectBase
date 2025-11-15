using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshot : Singleton<Screenshot>
{
    private string screenshotFolder;

    void Start()
    {
#if UNITY_EDITOR
        // Định nghĩa thư mục lưu ảnh trong Unity Editor
        screenshotFolder = Path.Combine(Application.dataPath, "../Screenshots");
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P)) // Nhấn phím P để chụp ảnh
        {
            CaptureScreenshot();
        }
#endif
    }

    void CaptureScreenshot()
    {
        string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string filepath = Path.Combine(screenshotFolder, filename);

        ScreenCapture.CaptureScreenshot(filepath);
        Debug.Log("Screenshot saved to: " + filepath);
    }
}
