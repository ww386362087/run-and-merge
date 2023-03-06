using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class EventTracking : Singleton<EventTracking>
{
    // Use this for initialization
    void Start()
	{
		AppsFlyer.setIsDebug(true);
		Application.runInBackground = true;
        //#if UNITY_IOS

        //		AppsFlyer.setAppsFlyerKey ("YOUR_DEV_KEY");
        //		AppsFlyer.setAppID ("YOUR_APP_ID");
        //		AppsFlyer.setIsDebug (true);
        //		AppsFlyer.getConversionData ();
        //		AppsFlyer.trackAppLaunch ();

        //		// register to push notifications for iOS uninstall
        //		UnityEngine.iOS.NotificationServices.RegisterForNotifications (UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
        //		Screen.orientation = ScreenOrientation.Portrait;

        //#elif UNITY_ANDROID

        //AppsFlyer.setAppInviteOneLinkID("BTx32xGv4UiaS6gNYsf5Gj");

        //AppsFlyer.setAppID ("YOUR_APP_ID"); 

        // for getting the conversion data
        //AppsFlyer.loadConversionData("StartUp");

        // for in app billing validation
        //		 AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure"); 

        //For Android Uninstall
        //AppsFlyer.setGCMProjectNumber ("YOUR_GCM_PROJECT_NUMBER");


        //#endif
    }


    // Update is called once per frame
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//go to background when pressing back button
#if UNITY_ANDROID
			AndroidJavaObject activity =
				new AndroidJavaClass("com.unity3d.player.UnityPlayer")
					.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call<bool>("moveTaskToBack", true);
#endif
		}


//#if UNITY_IOS
//		if (!tokenSent) { 
//			byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;           
//			if (token != null) {     
//			//For iOS uninstall
//				AppsFlyer.registerUninstall (token);
//				tokenSent = true;
//			}
//		}    
//#endif
	}
	//A custom event tracking
	public void Purchase()
	{
		Dictionary<string, string> eventValue = new Dictionary<string, string>();
		eventValue.Add("af_revenue", "300");
		eventValue.Add("af_content_type", "category_a");
		eventValue.Add("af_content_id", "1234567");
		eventValue.Add("af_currency", "USD");
		//AppsFlyer.trackRichEvent("af_purchase", eventValue);

	}

	public void SentEvent()
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CURRENCY, "USD");
		eventValues.Add(AFInAppEvents.REVENUE, "0.99");
		eventValues.Add("af_quantity", "1");
		AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
	}

	public void Event_LEVEL_ACHIEVED(string _level,string _start,string _end)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, "");
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.EVENT_START, _start);
		eventValues.Add(AFInAppEvents.EVENT_END, _end);

		AppsFlyer.sendEvent(AFInAppEvents.LEVEL_ACHIEVED, eventValues);
	}

	public void Event_AD_CLICK(string _level,string _rewradType)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, "");
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.ADREV_TYPE, "rewarded");
		eventValues.Add(AFInAppEvents.REWARD_TYPE, _rewradType);

		AppsFlyer.sendEvent(AFInAppEvents.AD_CLICK, eventValues);
	}

	public void Event_AD_View(string _level, string _rewradType)
	{
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, "");
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.ADREV_TYPE, "rewarded");
		eventValues.Add(AFInAppEvents.REWARD_TYPE, _rewradType);

		AppsFlyer.sendEvent(AFInAppEvents.AD_VIEW, eventValues);
	}

	public void Event_Other(string _key, string _value)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(_key, _value);

		AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
	}

}
