using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class RemoteDefault
{
    public string key, value;
}
public class RemoteConfigFb : MonoBehaviour
{
    public RemoteDefault[] remoteDefault;
    public static bool fireBaseInitDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
    //+= ConfigUpdateListenerEventHandler;


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                SetDefault();         // Thiết lập giá trị mặc định
                fireBaseInitDone = true;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    // Handle real-time Remote Config events.
    void ConfigUpdateListenerEventHandler(
       object sender, Firebase.RemoteConfig.ConfigUpdateEventArgs args)
    {
        if (args.Error != Firebase.RemoteConfig.RemoteConfigError.None)
        {
            Debug.Log(String.Format("Error occurred while listening: {0}", args.Error));
            return;
        }

        Debug.Log("Updated keys: " + string.Join(", ", args.UpdatedKeys));
        // Activate all fetched values and then display a welcome message.

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        remoteConfig.ActivateAsync().ContinueWithOnMainThread(
          task =>
          {
              //DisplayWelcomeMessage();
          });
    }

    // Stop the listener.
    void OnDestroy()
    {
        //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
        //  -= ConfigUpdateListenerEventHandler;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start a fetch request.
    // FetchAsync only fetches new data if the current data is older than the provided
    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    // By default the timespan is 12 hours, and for production apps, this is a good
    // number. For this example though, it's set to a timespan of zero, so that
    // changes in the console will always show up immediately.
    public Task FetchDataAsync()
    {
        Debug.Log("========= Fetching data...");
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("===========Retrieval hasn't finished.");

            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {

            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task =>
            {

                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");


                var  default_ = FirebaseApp.DefaultInstance; ;
                DataParam.timeDelayShowAds = float.Parse(FirebaseRemoteConfig.GetInstance(default_).GetValue(remoteDefault[0].key).StringValue);
                DataParam.ShowOpenAds = FirebaseRemoteConfig.GetInstance(default_).GetValue(remoteDefault[1].key).StringValue == "0" ? false : true;

                
            });
    }

    private void SetDefault()
    {
        System.Collections.Generic.Dictionary<string, object> defaults =
  new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server
        // yet, or if we ask for values that the server doesn't have:

        if(remoteDefault.Length <= 0)
        {
            Debug.LogError("not set remoteData firebase");
        }
        for(int i=0;i< remoteDefault.Length;i++)
        {
            var rm = remoteDefault[i];
            defaults.Add(rm.key, rm.value);
        }
        

        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
          .ContinueWithOnMainThread(task =>
          {
              FetchDataAsync();
          });
    }
}
