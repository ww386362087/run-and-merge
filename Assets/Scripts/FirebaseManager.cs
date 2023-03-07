using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;
public class FirebaseManager : Singleton<FirebaseManager>
{

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        Module.Event_StartGame += Module_Event_StartGame;
    }

    private void Module_Event_StartGame()
    {
        LogEvent_StartLevel();
    }

    void InitializeFirebase()
    {
        Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        Debug.Log("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
          FirebaseAnalytics.UserPropertySignUpMethod,
          "Google");
        // Set the user ID.
        FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;


        AnalyticsLogin();
    }

    public void AnalyticsLogin()
    {
        // Log an event with no parameters.
        Debug.Log("Logging a login event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }
    #region Event Game Analytic

    public void LogEvent_StartLevel()
    {
        if (firebaseInitialized)
        {
            string str = "level_start_firebase";
            FirebaseAnalytics.LogEvent(str, new Parameter(FirebaseAnalytics.ParameterLevel, PlayerPrefs.GetInt("level_general")));
        }
    }

    public void LogEvent_FinishLevel()
    {
        if (firebaseInitialized)
        {
            string str = "level_complete_firebase";
            FirebaseAnalytics.LogEvent(str, new Parameter(FirebaseAnalytics.ParameterLevel, PlayerPrefs.GetInt("level_general")));
        }
    }

    public void LogEvent_FailLevel()
    {
        if (firebaseInitialized)
        {
            string str = "level_fail_firebase";
            FirebaseAnalytics.LogEvent(str, new Parameter(FirebaseAnalytics.ParameterLevel, PlayerPrefs.GetInt("level_general")));
        }
    }

    public void LogEvent_Ad(string _name,string _param)
    {
        if (firebaseInitialized)
        {
            string str = _name;
            FirebaseAnalytics.LogEvent(str, new Parameter(FirebaseAnalytics.ParameterAdNetworkClickID, _param));
        }
    }

    // Reset analytics data for this app instance.
    public void ResetAnalyticsData()
    {
        Debug.Log("Reset analytics data.");
        FirebaseAnalytics.ResetAnalyticsData();
    }

   

    #endregion

    #region Messaging
    //public void MessengeCallStart()
    //{
    //    FirebaseMessaging.TokenReceived += OnTokenReceived;
    //    FirebaseMessaging.MessageReceived += OnMessageReceived;
    //}

    //public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    //{
    //    Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    //{
    //    Debug.Log("Received a new message from: " + e.Message.From);
    //}
    #endregion

}
