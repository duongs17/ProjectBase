using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InternetChecking : MonoBehaviour
{
    public static InternetChecking instance;
    public GraphicRaycaster raycaster;
    public Text statusText;
    public Canvas UIcanvas;
    private string textDes = "An Internet connection is required to play this game. Please turn on your network and try again.";
    private string testUrl = "http://google.com"; // URL kiểm tra kết nối

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UIcanvas.gameObject.SetActive(false);
        UIcanvas.gameObject.SetActive(false);
        StartCoroutine(CheckInternetRoutine());
    }

    IEnumerator CheckInternetRoutine()
    {
        while (true)
        {
            yield return CheckInternet();
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    IEnumerator CheckInternet()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            request.timeout = 3; // Đặt thời gian chờ
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                statusText.text = "Internet is available.";
                UIcanvas.gameObject.SetActive(false);
                raycaster.enabled = false;
            }
            else
            {
                //statusText.text = "No internet connection. Please enable your network.";
                statusText.text = textDes;
                UIcanvas.gameObject.SetActive(true);
                raycaster.enabled = true;
            }
        }
    }
}
