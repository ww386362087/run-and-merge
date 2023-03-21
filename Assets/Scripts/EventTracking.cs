using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase.Messaging;
using Firebase;
using DG.Tweening;


public class EventTracking : Singleton<EventTracking>
{

	public string str_Start = "";
	public string str_End	= "";

	// Use this for initialization
	void Start()
	{
		
		Application.runInBackground = true;

		AppsFlyer.initSDK("BTx32xGv4UiaS6gNYsf5Gj", "app_id");
		AppsFlyer.startSDK();
		AppsFlyer.setIsDebug(true);

		DOVirtual.DelayedCall(5, () => Event_af_login());
	

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


	public void Event_af_level_complete()
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.LEVEL, Module.lv_current);
		eventValues.Add(AFInAppEvents.EVENT_START, str_Start);
		eventValues.Add(AFInAppEvents.EVENT_END, str_End);

		AppsFlyer.sendEvent("af_level_complete_" + Module.lv_current, eventValues);
	}

	public void Event_af_level_fail()
	{

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(AFInAppEvents.LEVEL, Module.lv_current);
		eventValues.Add(AFInAppEvents.EVENT_START, str_Start);
		eventValues.Add(AFInAppEvents.EVENT_END, str_End);

		AppsFlyer.sendEvent("af_level_fail_" + Module.lv_current, eventValues);
	}


	public void Event_af_login()
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("login_time", DateTime.Now.ToString());

		AppsFlyer.sendEvent("af_login", eventValues);
	}


	public void Event_af_ad_impression(MaxSdkBase.AdInfo adInfo)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ad_impression", eventValues);
	}

	public void Event_af_ad_view(MaxSdkBase.AdInfo adInfo)
	{
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ad_view", eventValues);
	}

	public void Event_af_ad_click(MaxSdkBase.AdInfo adInfo)
	{
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ad_click", eventValues);
	}

	public void Event_af_ads_banner_show(MaxSdkBase.AdInfo adInfo)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_banner_show", eventValues);
	}

	public void Event_af_ads_banner_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
	{
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		eventValues.Add("errorMessage", errInfo.ToString());
	
		AppsFlyer.sendEvent("af_ads_banner_fail", eventValues);
	}

	public void Event_af_ads_banner_click(MaxSdkBase.AdInfo adInfo)
	{
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_banner_click", eventValues);
	}

	public void Event_af_ads_reward_offer()
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("type_reward", AdsMAXManager.Instance.rewardType);
		eventValues.Add("af_level", Module.lv_current);

		AppsFlyer.sendEvent("af_ads_reward_offer", eventValues);
	}


	public void Event_af_ads_reward_click(MaxSdkBase.AdInfo adInfo)
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_reward_click", eventValues);
	}
	
	public void Event_af_ads_reward_show(MaxSdkBase.AdInfo adInfo)
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_reward_show", eventValues);
	}
	
	public void Event_af_ads_reward_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		eventValues.Add("errorMessage", errInfo.ToString());

		AppsFlyer.sendEvent("af_ads_reward_fail", eventValues);
	}

	public void Event_af_ads_reward_complete(MaxSdkBase.AdInfo adInfo)
	{

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_reward_complete", eventValues);
	}

	public void Event_af_ads_inter_load(MaxSdkBase.AdInfo adInfo)
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_inter_load", eventValues);
	}
	
	public void Event_af_ads_inter_show(MaxSdkBase.AdInfo adInfo)
    {

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_inter_show", eventValues);
	}
	
	public void Event_af_ads_inter_fail(MaxSdkBase.AdInfo adInfo, MaxSdkBase.ErrorInfo errInfo)
	{

		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		eventValues.Add("errorMessage", errInfo.ToString());

		AppsFlyer.sendEvent("af_ads_inter_fail", eventValues);
	}

	public void Event_af_ads_inter_click(MaxSdkBase.AdInfo adInfo)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_adrev_ad_type", adInfo.NetworkPlacement);
		eventValues.Add("af_currency", "USD");
		eventValues.Add("af_revenue", adInfo.Revenue.ToString());
		eventValues.Add("af_level", Module.lv_current);
		eventValues.Add("networkName", adInfo.NetworkName);
		//eventValues.Add("af_info", adInfo.ToString());

		AppsFlyer.sendEvent("af_ads_inter_click", eventValues);
	}

	public void Event_af_purchase()
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add("af_content", "ads_remove");
		eventValues.Add("af_content_id", "ads_remove");
		eventValues.Add("af_quantity", "1");
		eventValues.Add("af_price", "79000");
		eventValues.Add("aF_level", Module.lv_current);
		eventValues.Add("af_currency", "VND");

		AppsFlyer.sendEvent("af_purchase", eventValues);
	}



	public void Event_Other(string _key, string _value)
    {
		Dictionary<string, string> eventValues = new Dictionary<string, string>();
		eventValues.Add(_key, _value);

		AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
	}

}
