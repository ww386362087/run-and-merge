using AppsFlyerSDK;
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
          FirebaseAnalytics.UserPropertySignUpMethod,"Google");

        // Set the user ID.
        //FirebaseAnalytics.SetUserId(device_id());
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;


        AnalyticsLogin();
        MessengeCallStart();
        Module.Event_StartGame += Module_Event_StartGame;
       
    }


    public void AnalyticsLogin()
    {
        // Log an event with no parameters.
        Debug.Log("Logging a login event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        FirebaseAnalytics.LogEvent("fbs_login");
    }

    #region Event Game Analytic
    private string levelCurrent()
    {
        return PlayerPrefs.GetInt("level_general",1).ToString();
    }

    public void LogEvent_StartLevel()
    {
        //if (firebaseInitialized)
        //{
        //    string str = "fbs_level_start_" + levelCurrent();
        //    FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
        //    Debug.Log(str);
        //}
    }

    public void LogEvent_FinishLevel()
    {
        if (firebaseInitialized)
        {
            string str = "fbs_level_complete_" + levelCurrent();
            FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_level_complete();
    }

    public void LogEvent_FailLevel()
    {
        if (firebaseInitialized)
        {
            string str = "fbs_level_fail_" + levelCurrent();
            FirebaseAnalytics.LogEvent(str, new Parameter("level", levelCurrent()));
            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_level_fail();
    }


    //trigger: record ad views of any ads displayed in the app
    public void LogEvent_fbs_ad_view(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ad_view";
            FirebaseAnalytics.LogEvent(str, 
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ad_view(adInfo);
    }

    //trigger: record ad views of any ads displayed in the app
    public void LogEvent_fbs_ad_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ad_click";
            FirebaseAnalytics.LogEvent(str,
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ad_click(adInfo);
    }


    //trigger: record when any ads displayed in the app
    public void LogEvent_firebase_ad_impression(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            double revenue = adInfo.Revenue;
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            };
            FirebaseAnalytics.LogEvent("fbs_ad_impression", impressionParameters);
        }
       
        EventTracking.Instance.Event_af_ad_impression(adInfo);
    }

    //trigger: record when banner ads displayed in the app
    public void LogEvent_fbs_ads_banner_show(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ads_banner_show";
            FirebaseAnalytics.LogEvent(str,
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_banner_show(adInfo);

        LogEvent_fbs_ad_view(adInfo);
    }

    //trigger: record when banner ads not displayed in the app
    public void LogEvent_fbs_ads_banner_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ads_banner_fail";
            FirebaseAnalytics.LogEvent(str,
            new Parameter("ad_platform", "AppLovin"),
            //new Parameter("adFormat", adInfo.AdFormat),
            //new Parameter("networkName", adInfo.NetworkName),
            //new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            //new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            //new Parameter("value", adInfo.Revenue),
            //new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            new Parameter("errorMessage", errInfo.ToString()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_inter_fail(adInfo,errInfo);
    }

    //trigger: record when banner ads is clicked
    public void LogEvent_fbs_ads_banner_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_banner_click", impressionParameters);

        }

        EventTracking.Instance.Event_af_ads_banner_click(adInfo);

        LogEvent_fbs_ad_click(adInfo);
    }


    //trigger: record when rewards button displayed in the app
    public void LogEvent_firebase_ads_reward_offer()
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ads_reward_offer";
            FirebaseAnalytics.LogEvent(str, new Parameter("type_reward",AdsMAXManager.Instance.rewardType),
                                            new Parameter("level", levelCurrent()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_offer();
    }

    //trigger: record when rewards ad is clicked when displaying
    public void LogEvent_firebase_ads_reward_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_reward_click", impressionParameters);

        }

        EventTracking.Instance.Event_af_ads_reward_click(adInfo);

        LogEvent_fbs_ad_click(adInfo);
    }

    //trigger: record when rewards ad is displayed
    public void LogEvent_fbs_ads_reward_show(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_reward_show", impressionParameters);
        }

        EventTracking.Instance.Event_af_ads_reward_show(adInfo);
        LogEvent_fbs_ad_view(adInfo);
    }

    //số lần ad show thành công
    public void LogEvent_firebase_ads_reward_complete(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_reward_complete", impressionParameters);
        }

        EventTracking.Instance.Event_af_ads_reward_complete(adInfo);
    }


    //trigger: record when rewards ad is not displayed
    public void LogEvent_firebase_ads_reward_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
    {

        if (firebaseInitialized)
        {
            string str = "fbs_ads_reward_fail";
            FirebaseAnalytics.LogEvent(str,
            new Parameter("ad_platform", "AppLovin"),
            //new Parameter("adFormat", adInfo.AdFormat),
            //new Parameter("networkName", adInfo.NetworkName),
            //new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            //new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            //new Parameter("value", adInfo.Revenue),
            //new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            new Parameter("errorMessage", errInfo.ToString()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_reward_fail(adInfo, errInfo);
    }

    //trigger: record when intertitial ads is loaded to user's app
    public void LogEvent_firebase_ads_inter_load(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_inter_load", impressionParameters);
        }

        EventTracking.Instance.Event_af_ads_inter_load(adInfo);
    }

    //trigger: record when intertitial ads is displayed in app
    public void LogEvent_firebase_ads_inter_show(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_inter_show", impressionParameters);
        }

        EventTracking.Instance.Event_af_ads_inter_show(adInfo);
        LogEvent_fbs_ad_view(adInfo);
    }

    //trigger: record when intertitial ads is not displayed in app
    public void LogEvent_firebase_ads_inter_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
    {
        if (firebaseInitialized)
        {
            string str = "fbs_ads_reward_fail";
            FirebaseAnalytics.LogEvent(str,
            new Parameter("ad_platform", "AppLovin"),
           // new Parameter("adFormat", adInfo.AdFormat),
           // new Parameter("networkName", adInfo.NetworkName),
           // new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
           // new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
           // new Parameter("value", adInfo.Revenue),
           // new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            new Parameter("errorMessage", errInfo.ToString()));

            Debug.Log(str);
        }

        EventTracking.Instance.Event_af_ads_inter_fail(adInfo, errInfo);
    }

    //trigger: record when intertitial ads is clicked when displaying
    public void LogEvent_firebase_ads_inter_click(MaxSdkBase.AdInfo adInfo)
    {
        if (firebaseInitialized)
        {
            var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("adFormat", adInfo.AdFormat),
            new Parameter("networkName", adInfo.NetworkName),
            new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
            new Parameter("networkPlacement", adInfo.Placement), // Please check this - as we couldn't find format refereced in your unity docs https://dash.applovin.com/documentation/mediation/unity/getting-started/advanced-settings#impression-level-user-revenue - api
            new Parameter("value", adInfo.Revenue),
            new Parameter("revenuePrecision", adInfo.RevenuePrecision),
            new Parameter("currency", "USD"), // All Applovin revenue is sent in USD
            new Parameter("fbs_level", Module.lv_current),
            };
            FirebaseAnalytics.LogEvent("fbs_ads_inter_click", impressionParameters);
        }

        EventTracking.Instance.Event_af_ads_inter_click(adInfo);
        LogEvent_fbs_ad_click(adInfo);
    }

    // trigger: record purchase events 
    public void LogEvent_firebase_purchase()
    {
        if (firebaseInitialized)
        {
            string str = "firebase_purchase";
            //UnityEngine.Purchasing.ProductMetadata productMetadata = _product.metadata;
            //double dm = (double)(productMetadata.localizedPrice);
            FirebaseAnalytics.LogEvent(str, 
                    new Parameter("fbs_level", Module.lv_current),
                    new Parameter("fbs_product", "adsremove"),
                    new Parameter("fbs_product_id", "adsremove"),
                    new Parameter("fbs_price", "79000"),
                    new Parameter("fbs_currency", "VND")
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
#if UNITY_ANDROID
        AppsFlyer.updateServerUninstallToken(token.Token);
#endif
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }
    #endregion

}
