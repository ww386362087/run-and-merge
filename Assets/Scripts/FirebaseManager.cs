using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.Messaging;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseManager : Singleton<FirebaseManager>
{

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    //DontDestroyOnLoad(this.gameObject);
    //}

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
        FirebaseAnalytics.SetUserId(device_id());
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;


        AnalyticsLogin();
        MessengeCallStart();
        Module.Event_StartGame += Module_Event_StartGame;
        //Module.Event_Purchase_Complete += Module_Event_Purchase_Complete;
    }

    //private void Module_Event_Purchase_Complete(UnityEngine.Purchasing.Product obj)
    //{
    //    throw new NotImplementedException();
    //}

    public void AnalyticsLogin()
    {
        // Log an event with no parameters.
        Debug.Log("Logging a login event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }
    #region Event Game Analytic

    private string levelCurrent()
    {
        return PlayerPrefs.GetInt("level_general",1).ToString();
    }

    private string device_id()
    {
        return SystemInfo.deviceUniqueIdentifier!=null? SystemInfo.deviceUniqueIdentifier:string.Empty;
    }

    public void LogEvent_StartLevel()
    {
        if (firebaseInitialized)
        {
            
            string str = "firebase_level_start_" + levelCurrent();
            FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }
    }

    public void LogEvent_FinishLevel()
    {
        if (firebaseInitialized)
        {
            string str = "firebase_level_complete_" + levelCurrent();
            FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_level_complete();
    }

    public void LogEvent_FailLevel()
    {
        if (firebaseInitialized)
        {
            string str = "firebase_level_fail_" + levelCurrent();
            FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_level_fail();
    }


    public void LogEvent_Ad(string _name,string _param)
    {
        if (firebaseInitialized)
        {
            string str = _name;
            FirebaseAnalytics.LogEvent(str, new Parameter(FirebaseAnalytics.ParameterAdNetworkClickID, _param),
                                            new Parameter(FirebaseAnalytics.ParameterLevel, levelCurrent()));

            Debug.Log(str);
        }
    }


    //Có bao nhiêu lượt hiển thị ads?
    public void LogEvent_firebase_ad_impression()
    {

    }

    //số lần nút reward ad xuất hiện
    public void LogEvent_firebase_ads_reward_offer()
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_reward_offer";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward",AdsMAXManager.Instance.rewardType),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_offer();
    }

    //Số lần click vào ads khi ads đang hiển thị
    public void LogEvent_firebase_ads_reward_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_reward_offer";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", AdsMAXManager.Instance.rewardType),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_click(adInfo);
    }

    //số lần ad show thành công
    public void LogEvent_firebase_ads_reward_show(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_reward_show";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", AdsMAXManager.Instance.rewardType),                                    
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_show(adInfo);
    }

    //số lần ad show thành công
    public void LogEvent_firebase_ads_reward_complete(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_reward_complete";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", AdsMAXManager.Instance.rewardType),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("revenue", adInfo.Revenue),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_complete(adInfo);
    }


    //số lần ad show bị fail
    public void LogEvent_firebase_ads_reward_fail(MaxSdkBase.ErrorInfo adInfo,string placement_id)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_reward_show";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", AdsMAXManager.Instance.rewardType),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("errormsg", adInfo.Message),
                                            new Parameter("placement_id", placement_id),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_fail(adInfo, placement_id);
    }

    //số lần ads_inter load về máy người chơi
    public void LogEvent_firebase_ads_inter_load(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_inter_load";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", "inter"),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }
        EventTracking.Instance.Event_af_ads_inter_load(adInfo);
    }

    //số lần ad_inter show thành công
    public void LogEvent_firebase_ads_inter_show(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_inter_show";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", "inter"),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("revenue", adInfo.Revenue),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_inter_show(adInfo);
    }

    //số lần ad_inter show bị fail
    public void LogEvent_firebase_ads_inter_fail(MaxSdkBase.ErrorInfo adInfo, string placement_id)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_inter_fail";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", "inter"),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("errormsg", adInfo.Message),
                                            new Parameter("placement_id", placement_id),
                                            new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_inter_fail(adInfo,placement_id);
    }

    //Số lần click vào ads khi ads đang hiển thị
    public void LogEvent_firebase_ads_inter_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "firebase_ads_inter_click";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward", "inter"),
                                            new Parameter("customer_user_id", device_id()),
                                            new Parameter("placement_id", adInfo.AdUnitIdentifier),
                                            new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_inter_click(adInfo);
    }

    // Có bao nhiêu lượt mua cho từng sản phẩm?
    // Có bao nhiêu user mua hàng
    public void LogEvent_firebase_purchase()
    {
        if (firebaseInitialized)
        {
            string str = "firebase_purchase";
            //UnityEngine.Purchasing.ProductMetadata productMetadata = _product.metadata;
            //double dm = (double)(productMetadata.localizedPrice);
            FirebaseAnalytics.LogEvent(str, new Parameter("customer_user_id", device_id()),
                                            new Parameter("level", levelCurrent())
                                           // new Parameter("price", dm),
                                            //new Parameter("content", productMetadata.localizedTitle)
                                            );
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_purchase();
    }

    // Reset analytics data for this app instance.
    public void ResetAnalyticsData()
    {
        Debug.Log("Reset analytics data.");
        FirebaseAnalytics.ResetAnalyticsData();
    }



    #endregion

    #region Messaging
    public void MessengeCallStart()
    {
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }
    #endregion

}
