using UnityEngine;
using System.Collections;

public sealed class NativeXBannerPosition {

	private readonly string name;
	//private readonly int index;

	/*//portrait: 320x50 or 768x66
	//landscape: 480x32 or 1024x66
	kBannerPositionTop,
	kBannerPositionBottom,
	//landscape only: 100x320 or 120x768
	kBannerPositionLandscapeLeft,
	kBannerPositionLandscapeRight,
	*/
	
//  WHY??
//	private static readonly Dictionary<string, NativeXBannerPosition> instance = new Dictionary<string,NativeXBannerPosition>();

	//PORTAIT: 320x50 or 768x66
	//LANDSCAPE: 480x32 or 1024x66
	public static readonly NativeXBannerPosition TOP = new NativeXBannerPosition(1, "NATIVEX_BANNER_TOP");
	public static readonly NativeXBannerPosition BOTTOM = new NativeXBannerPosition(2, "NATIVEX_BANNER_BOTTOM");
//	//LANDSCAPE ONLY: 100x320 or 120x768
//	public static readonly NativeXBannerPosition LEFT = new NativeXBannerPosition(3, "PNATIVEX_BANNER_LEFT");
//	public static readonly NativeXBannerPosition RIGHT = new NativeXBannerPosition(4, "NATIVEX_BANNER_RIGHT");
	
	
	private NativeXBannerPosition(int a, string b)
	{
		//index = a;
		name = b;
//		instance[b] = this; WHY??
	}
	
	public override string ToString()
	{
		return name;	
	}
	
	public static implicit operator string(NativeXBannerPosition rhs )
	{
		if( null != rhs ){
		 
			return rhs.name;
 
		} else {

			throw new UnityException("Was not able to cast provided value: null ");
		
		}
	}

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class NativeXCore {
	
	public static bool isDebugLogEnabled = false;
	public static NativeXiOS iOSDevice;
	public static NativeXAndroid androidDevice;
	public static bool useGenericRedemptionAlerts = false;

	
#if (UNITY_ANDROID && !UNITY_EDITOR) 
	private static AndroidJavaObject instance;
	
	//Create link to our SDK
	private static AndroidJavaClass publisherPlug = new AndroidJavaClass("com.nativex.NativeXMonetizationUnity");
#endif
	
	static NativeXCore()
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			Debug.Log("NativeX - Android Inititialization called");
			instance = publisherPlug.CallStatic<AndroidJavaObject>("getInstance");
		}
#endif
	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uStartWithNameAndApplicationId(string applicationId, string publisherId, bool enableLogging, bool showRedeemAlert);
#endif
	public static void initialization(NativeXAndroid android, NativeXiOS iOS)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		//if(android != null){
			androidDevice = android;
			if(Application.platform == RuntimePlatform.Android){
				Debug.Log("W3i - Initialization called");
				Debug.Log("Android Device: "+androidDevice.ToString());
			instance.Call("init", android.appId, android.publisherUserId, android.enableLogging, useGenericRedemptionAlerts, true);	
			}
		//}else{
		//	Debug.Log("No NativeXAndroid object exists");
		//}
#elif (UNITY_IPHONE && !UNITY_EDITOR)	
		//if(iOS!=null){
			iOSDevice = iOS;
			if(iOS.publisherUserId == null){
				iOS.publisherUserId = "";
			}
			if(Application.platform == RuntimePlatform.IPhonePlayer){
				uStartWithNameAndApplicationId(iOS.appId, iOS.publisherUserId, iOS.enableLogging, useGenericRedemptionAlerts);
			}
		//}else{
		//	Debug.Log("No NativeXiOS object exists.");
		//}
#endif
		if(isDebugLogEnabled){
			Debug.Log("initialization has been hit");
		}
		return;		
	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uFetchAd(string name);
#endif
	/**
 	* Fetch an enhanced ad
 	* 
 	* @param customPlacement      string representation of a Custom Placement
 	*/
	public static void fetchAd(string customPlacement)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("fetchAd", customPlacement);
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uFetchAd(customPlacement);	
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("fetchAd has been hit");
		}
	}
	
	/**
 	* Show an enhanced ad with placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param placement   enum representation of placement within the app
 	*/
	public static void showAd( NativeXAdPlacement placement)
	{
		showAd (placement.ToString());
	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uShowAd(string name);
#endif
	/**
 	* Show an enhanced ad with custom placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param customPlacement   string representation of a Custom Placement 
 	*/
	public static void showAd(string customPlacement)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("showAd",customPlacement);	
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowAd(customPlacement);
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showAd has been hit");
		}
	}
	
	/**
 	* Show an enhanced ad with custom placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param customPlacement   string representation of a Custom Placement 
 	*/
	public static void showReadyAd(NativeXAdPlacement placement)
	{
		showReadyAd(placement.ToString());
	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uShowReadyAd(string name);
#endif
	/**
 	* Show an enhanced ad with custom placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param customPlacement   string representation of a Custom Placement 
 	*/
	public static void showReadyAd(string customPlacement)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("showReadyAd",customPlacement);	
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowReadyAd(customPlacement);
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showAd has been hit");
		}
	}

	/**
 	* Fetch an enhanced ad
 	* 
 	* @param placement      enum representation of placement within the app
 	*/
	public static void fetchAd(NativeXAdPlacement placement)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("fetchAd", placement.ToString());
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uFetchAd(placement.ToString());	
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("fetchAd has been hit");
		}
	}

	#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uDismissAd(string name);
	#endif
	/**
 	* Dismiss an enhanced ad with placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param customPlacement   string representation of a Custom Placement
 	*/
	public static void dismissAd( string customPlacement)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("dismissAd", customPlacement);	
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uDismissAd(customPlacement);
		}
		#endif
		
		if(isDebugLogEnabled){
			Debug.Log(customPlacement);
			Debug.Log("showAd has been hit");
		}
	}
	
	public static bool isAdReady (NativeXAdPlacement nativeXAdPlacement)
	{
		return isAdReady(nativeXAdPlacement.ToString());
	}
	
	#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern bool uIsAdReady(string name);
	#endif
	public static bool isAdReady (string placementName) 
	{
		if (placementName == null) {
			placementName = "";
		}
		
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if (Application.platform == RuntimePlatform.Android) {
			bool ret = instance.Call<bool>("isAdReady", placementName);
			//Debug.Log("IsAdReady android call = " + ret.ToString());
			return ret;
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			return uIsAdReady(placementName);
		}
		#endif
		// fallthrough case
		Debug.LogWarning("Unable to determine player platform; cannot determine if ad is ready!");
		return false;
	}

	/**
 	* Dismiss an enhanced ad with placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param placement   enum representation of placement within the app
 	*/
	public static void dismissAd( NativeXAdPlacement placement)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("dismissAd", placement.ToString());	
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uDismissAd(placement.ToString());
		}
		#endif
		
		if(isDebugLogEnabled){
			Debug.Log(placement.ToString());
			Debug.Log("showAd has been hit");
		}
	}

//#if (UNITY_IPHONE && !UNITY_EDITOR)
//	[DllImport ("__Internal")]
//	public static extern void uFetchBanner(string placement, int x, int y, int height, int width);
//#endif
//	public static void fetchBanner(string customPlacement, Rect position)
//	{
//#if (UNITY_ANDROID && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.Android){
//			if(placement == null){
//				placement = "";
//			}
//			instance.Call("fetchBanner" , customPlacement, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#elif (UNITY_IPHONE && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.IPhonePlayer){
//			uFetchBanner(customPlacement, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#endif
//		if(isDebugLogEnabled){
//			Debug.Log("fetchBanner has been hit");
//		}
//	}
//
//#if (UNITY_IPHONE && !UNITY_EDITOR)
//	[DllImport ("__Internal")]
//	public static extern void uShowBanner(string placement, int x, int y, int height, int width);
//#endif
//	public static void showBanner(string customPlacement, Rect position)
//	{
//#if (UNITY_ANDROID && !UNITY_EDITOR)
//	if(Application.platform == RuntimePlatform.Android){
//			if(placement == null){
//				placement = "";
//			}
//			instance.Call("showBanner" , customPlacement, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#elif (UNITY_IPHONE && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.IPhonePlayer){
//			uShowBanner(customPlacement, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#endif
//		if(isDebugLogEnabled){
//			Debug.Log("showBanner has been hit");
//		}
//	}
//
//	public static void fetchBanner(NativeXAdPlacement placement, Rect position)
//	{
//#if (UNITY_ANDROID && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.Android){
//			if(placement == null){
//				placement = "";
//			}
//			instance.Call("fetchBanner" , placement.ToString(), Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#elif (UNITY_IPHONE && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.IPhonePlayer){
//			uFetchBanner(placement.ToString(), Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#endif
//		if(isDebugLogEnabled){
//			Debug.Log("fetchBanner has been hit");
//		}
//	}
//
//	public static void showBanner(NativeXAdPlacement placement, Rect position)
//	{
//#if (UNITY_ANDROID && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.Android){
//			if(placement == null){
//				placement = "";
//			}
//			instance.Call("showBanner" , placement.ToString(), Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#elif (UNITY_IPHONE && !UNITY_EDITOR)
//		if(Application.platform == RuntimePlatform.IPhonePlayer){
//			uShowBanner(placement.ToString(), Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#endif
//		if(isDebugLogEnabled){
//			Debug.Log("showBanner has been hit");
//		}
//	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
[DllImport ("__Internal")]
	public static extern void uFetchBannerWithPosition(string placement, string position);
[DllImport ("__Internal")]
	public static extern void uShowBannerWithPosition(string placement, string position);
#endif


	public static void fetchBanner(string customPlacement, NativeXBannerPosition position)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("fetchBanner" , customPlacement);
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uFetchBannerWithPosition(customPlacement, position.ToString());
		}
		#endif
		if(isDebugLogEnabled){
			Debug.Log("fetchBanner has been hit");
		}
	}
	
	public static void showBanner(string customPlacement, NativeXBannerPosition position)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("showBanner" , customPlacement, position.ToString());
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowBannerWithPosition(customPlacement, position.ToString());
		}
		#endif
		if(isDebugLogEnabled){
			Debug.Log("showBanner has been hit");
		}
	}

	public static void fetchBanner(NativeXAdPlacement placement, NativeXBannerPosition position)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("fetchBanner" , placement.ToString());
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uFetchBannerWithPosition(placement.ToString(), position.ToString());
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("fetchBanner has been hit");
		}
	}
	
	public static void showBanner(NativeXAdPlacement placement, NativeXBannerPosition position)
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showBanner" , placement.ToString(),position.ToString());
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowBannerWithPosition(placement.ToString(), position.ToString());
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showBanner has been hit");
		}
	}

	#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uDismissBanner(string name);
	#endif
	/**
 	* Dismiss an enhanced ad with placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param customPlacement   string representation of a Custom Placement
 	*/
	public static void dismissBanner( string customPlacement)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			if(customPlacement == null){
				customPlacement = "";
			}
			instance.Call("dismissBanner", customPlacement);	
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uDismissBanner(customPlacement);
		}
		#endif
		
		if(isDebugLogEnabled){
			Debug.Log(customPlacement);
			Debug.Log("showAd has been hit");
		}
	}

	/**
 	* Dismiss an enhanced ad with placement name from key window, 
 	* used for targeting certain ads for certain in app placements.
 	* 
 	* @param placement   enum representation of placement within the app
 	*/
	public static void dismissBanner( NativeXAdPlacement placement)
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("dismissBanner", placement);	
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uDismissBanner(placement);
		}
		#endif
		
		if(isDebugLogEnabled){
			Debug.Log(placement.ToString());
			Debug.Log("showAd has been hit");
		}
	}

#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uRedeemCurrency(bool show);
#endif
	[Obsolete("use redeemRewards instead", false)]
	public static void redeemCurrency()
	{
#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("redeemCurrency", useGenericRedemptionAlerts);
		}
#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uRedeemCurrency(useGenericRedemptionAlerts);	
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("redeemCurrency has been hit");
		}
	}
	
	//-------------------------------------------------------------------------
	#if (UNITY_IPHONE && !UNITY_EDITOR)
	[DllImport ("__Internal")]
	public static extern void uRedeemRewards(bool show);
	#endif
	public static void redeemRewards()
	{
		#if (UNITY_ANDROID && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("redeemRewards", useGenericRedemptionAlerts);
		}
		#elif (UNITY_IPHONE && !UNITY_EDITOR)
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uRede