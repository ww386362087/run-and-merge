using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class EventTracking : Singleton<EventTracking>
{

	public string str_Start = "";
	public string str_End	= "";
	string idDevice;

	// Use this for initialization
	void Start()
	{
		AppsFlyer.setIsDebug(true);
		Application.runInBackground = true;
		idDevice = SystemInfo.deviceUniqueIdentifier;

		Event_af_login();
		#region Demo
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
		#endregion
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

		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CURRENCY, "USD");
		eventValues.Add(AFInAppEvents.REVENUE, "0.99");
		eventValues.Add("af_quantity", "1");
		AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);

	}

	public void Event_LEVEL_ACHIEVED(string _level)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, idDevice);
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.EVENT_START, str_Start);
		eventValues.Add(AFInAppEvents.EVENT_END, str_End);

		AppsFlyer.sendEvent(AFInAppEvents.LEVEL_ACHIEVED +"_"+ _level, eventValues);
	}

	
	public void Event_AD_CLICK(string _level,string _rewradType)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, idDevice);
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.ADREV_TYPE, "rewarded");
		eventValues.Add(AFInAppEvents.REWARD_TYPE, _rewradType);

		AppsFlyer.sendEvent(AFInAppEvents.AD_CLICK, eventValues);
	}

	public void Event_AD_View(string _level, string _rewradType, string _rewarded= "rewarded")
	{
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, idDevice);
		eventValues.Add(AFInAppEvents.LEVEL, _level);
		eventValues.Add(AFInAppEvents.ADREV_TYPE, _rewarded);
		eventValues.Add(AFInAppEvents.REWARD_TYPE, _rewradType);

		AppsFlyer.sendEvent(AFInAppEvents.AD_VIEW, eventValues);
	}


	public void Event_af_login()
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.CUSTOMER_USER_ID, idDevice);
		eventValues.Add("login_time", DateTime.Now.ToString());

		AppsFlyer.sendEvent("af_login", eventValues);
	}

	public void Event_af_ad_impression()
    {

    }

	public void Event_af_ads_reward_offer()
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_reward_offer", eventValues);
	}

	private string levelCurrent()
	{
		return PlayerPrefs.GetInt("level_general",1).ToString();
	}

	private string device_id()
	{
		return SystemInfo.deviceUniqueIdentifier != null ? SystemInfo.deviceUniqueIdentifier : string.Empty;
	}
	public void Event_af_ads_reward_click(MaxSdkBase.AdInfo adInfo)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent()); 

		AppsFlyer.sendEvent("af_ads_reward_click", eventValues);
	}
	
	public void Event_af_ads_reward_show(MaxSdkBase.AdInfo adInfo)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent()); 

		AppsFlyer.sendEvent("af_ads_reward_show", eventValues);
	}
	
	public void Event_af_ads_reward_fail(MaxSdkBase.ErrorInfo adInfo, string placement_id)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("errormsg", adInfo.Message);
		eventValues.Add("level", levelCurrent()); 

		AppsFlyer.sendEvent("af_ads_reward_fail", eventValues);
	}

	public void Event_af_ads_reward_complete(MaxSdkBase.AdInfo adInfo)
	{
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_reward_complete", eventValues);
	}

	public void Event_af_ads_inter_load(MaxSdkBase.AdInfo adInfo)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_inter_load", eventValues);
	}
	
	public void Event_af_ads_inter_show(MaxSdkBase.AdInfo adInfo)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_inter_show", eventValues);
	}
	
	public void Event_af_ads_inter_fail(MaxSdkBase.ErrorInfo adInfo, string placement_id)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("errormsg", adInfo.Message);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_inter_fail", eventValues);
	}

	public void Event_af_ads_inter_click(MaxSdkBase.AdInfo adInfo)
    {
		if (!AppsFlyer.instance.isInit)
			return;
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("customer_user_id", device_id());
		eventValues.Add("placement_id", adInfo.AdUnitIdentifier);
		eventValues.Add("level", levelCurrent());

		AppsFlyer.sendEvent("af_ads_inter_click", eventValues);
	}

	public void Event_af_purchase()
    {

    }



	public void Event_Other(string _key, string _value)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(_key, _value);

		AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
	}

}
