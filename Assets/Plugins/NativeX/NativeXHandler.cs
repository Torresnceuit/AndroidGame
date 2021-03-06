using System;
using System.Collections;
using System.Collections.Generic;

[Obsolete("use NativeXRewardData instead", false)]
public class NativeXCurrencyData : IEnumerable {

    private ArrayList balances; // Using a generic might be okay here, but we've encountered problems in Unity with generics

	public NativeXCurrencyData()
	{
        balances = new ArrayList();
	}

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public NativeXBalances GetEnumerator()
    {
		var enumeratedBalances = new NativeXBalances(( NativeXBalance[]) balances.ToArray(typeof(NativeXBalance)));
		balances.Clear();
		return enumeratedBalances;
    }
 
    public static NativeXCurrencyData convertFromJson(string json)
    {
		#region Example Data
		//	The SDK returns the following example reward data:
		//	{  
		//		"balances":[  
		//		   {  
		//			"ExternalCurrencyId":"freelife",
		//			"DisplayName":"Free Life",
		//			"Amount":1,
		//			"Id":4812
		//		   }
		//		   ],
		//		"receipts":[  
		//		   "2b0c60f0-a70f-11e4-8578-075995452aeb"
		//		   ],
		//		"messages":[  
		//		   {  
		//			"DisplayText":"Congratulations!  You have earned 1 Free Life.",
		//			"ReferenceName":"PaymentSuccessAfpp",
		//			"DisplayName":"Currency Awarded"
		//		   }
		//		   ]
		//	}
		#endregion
		
		NativeXCurrencyData dBalances = new NativeXCurrencyData();
        
        try {
			IDictionary<string, object> deserialized = NativeXMiniJSON.Json.Deserialize(json) as Dictionary<string, object>;

			if (( deserialized != null ) && ( deserialized.ContainsKey("balances") ))
			{
				List<object> balances = (List<object>) deserialized["balances"] ;
				if (balances != null)
				{
					foreach( object b in balances )
					{
						NativeXBalance nxb = new NativeXBalance();
						
						IDictionary<string, object> h = b as Dictionary<string,object>;
						
						if (h != null)
						{
							nxb.DisplayName = (string) h["DisplayName"];
							nxb.RewardType = (string) h["ExternalCurrencyId"];
							
							if( h["Amount"] is Int64 ) 
							{
								nxb.Amount = (Int64) h["Amount"];
							}
							else if (h["Amount"] is Double)
							{
								nxb.Amount = (Double) h["Amount"];
							}
							else if (h["Amount"] is string)
							{
								nxb.Amount = System.Convert.ToDouble((string)h["Amount"]);
							}
							else 
							{
								UnityEngine.Debug.Log("Currency Data: unknown type!!");
								nxb.Amount = 0;
							}
							
							if (h.ContainsKey("Receipt"))
							{
								nxb.ReceiptId = (string) h["ReceiptId"];
							}
							
							dBalances.Add(nxb);
						} else {
							UnityEngine.Debug.Log("unable to convert balance to Dictionary<string,object>!");
						}
					}
				}
			}
			
        } catch (Exception e) {
        	UnityEngine.Debug.Log("Caught exception with currency data: " + e.Message);
        	// don't rethrow, swallow it..
        }
		return dBalances;	// should be either filled or just empty object
	}

	public void Add( NativeXBalance b ) 
	{
        balances.Add( b );
	}

	public override string ToString()
	{
		string stringBuilder = "";
		foreach( var b in balances)
		{
			stringBuilder += "Balance:" + b.ToString();
			stringBuilder += "\n";
		}

		return stringBuilder;
	}

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NativeXHandler : MonoBehaviour {

	/// <summary>
	/// Keeps track of the placements that are currently known to be loaded. If the
	/// placement is available, it will be available in the set. If unavailable, 
	/// it will not exist in the set.
	/// </summary>
	private static HashSet<string> refetchKeeper = new HashSet<string>();
	private static HashSet<string> placementConvertedList = new HashSet<string>();

	#region Event handler registers
	/// <summary>
	/// Called when SDK has finished initializing.
	/// </summary>
	/// <param name="didSucceed">True if the SDK successfully initialized, false if not.</param>
	public static event Action<bool> e_didSDKinitialize;
	
	/// <summary>
	/// Occurs when an ad was available, is loaded, and is ready to display. 
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_didAdLoad;
	
	/// <summary>
	/// Called if no ad was available from the ad server.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_noAdLoaded;
	
	/// <summary>
	/// Called when the Banner Ad Unit will expand to fullscreen.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_adWillExpand;
	
	/// <summary>
	/// Called when the Banner Ad Unit will collapse back to normal banner view.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_adWillCollapse;
	
	/// <summary>
	/// Called when the Banner Ad Unit will resize.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_adWillResize;
	
	/// <summary>
	/// Called when the Ad Unit has been closed by the user after being displayed. This string 
	/// will return the name of the placment or "NAME_UNDEFINED" if the name is unknown.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_actionCompleted;
	
	/// <summary>
	/// Called when the Ad Unit has been closed by the user after being displayed.  This string
	/// will return the name of the placement or "NAME_UNDEFINED" if the name is unknown, as well as 
	/// whether the placement has converted or not.  Conversion only applies to Rewarded Video!
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	/// <param name="converted">True if the placement has completed, false if it has not</param>
	public static event Action<string, bool> e_adDismissed;
	
	/// <summary>
	/// Called when the Ad Unit has failed to either load or display. Passes the name of the placement
	/// that has failed or "NAME_UNDEFINED" if the ad has no placement name.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_actionFailed;
	
	/// <summary>
	/// Called when the User has chosen to go through with the presented offer and will be redirected 
	/// from your application. Will pass the placement name where the redirect originated.
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<bool> e_userLeavingApplication;

	/// <summary>
	/// Obsolete. Use e_rewardTransferred.
	/// </summary>
	[Obsolete("use e_rewardTransferred instead", false)]
	public static event Action<NativeXCurrencyData> e_balanceTransfered;

	/// <summary>
	/// Called when a reward is ready to be presented to the user. Returns a 
	/// NativeXRewardData - object containing any number of NativeXReward objects 
	/// containing information that should be used to award the player with the 
	/// credits they earned.
	/// </summary>
	public static event Action<NativeXRewardData> e_rewardTransferred;
	
	/// <summary>
	/// Called when the SDK has successfully intitialized, with the session id as a string.
	/// </summary>
	/// <param name="sessionId">The session id generated for this ad session.</param>
	public static event Action<string> e_sessionId;
	
	/// <summary>
	/// Called when a user clicks through to either rate your app or sign up for automatic updates(Android only).
	/// </summary>
	[Obsolete("Will be removed in a future version", false)]
	public static event Action<bool> e_didPerformAction;
	
	/// <summary>
	/// Called when the Ad expired to let the developer know he should fetch a new Ad.
	/// Will return the placement name of the Ad that has expired if the interstitial has 
	/// no name it will return "NAME_UNDEFINED".
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_adDidExpire;

	/// <summary>
	/// Called when the ad that we are about to show will contain audio. 
	/// </summary> 
	/// <param name="willPlayAudio">True if the ad uses audio, false if not.</param>
	public static event Action<bool> e_willPlayAudio;
	
	/// <summary>
	/// Called when the video ad that is being displayed has converted, and rewards will be available for the user
	/// </summary>
	/// <param name="placement">The placement name or "NAME_UNDEFINED" if unknown.</param>
	public static event Action<string> e_adConverted;
	
	#endregion
	
	
	#region calls from NativeXCoreHelper
	/// <summary>
	/// Registers the placement for refetch. Will attempt a refetch if the ad has 
	/// shown, if the ad has failed to show, if the user has completed the ad.
	/// </summary>
	/// <param name="placement">The placement name where refetch is requested.</param>
	public static void RegisterPlacementForRefetch( string placement, bool shouldRefetch = true ) {

		if (IsValidPlacement (placement)) {
			if( shouldRefetch ) {
				refetchKeeper.Add(placement);
			} else {
				refetchKeeper.Remove(placement);
			}
		}

	}

	/// <summary>
	/// Attempt to refetch the ad, if the placement is registered for refetch.
	/// </summary>
	/// <param name="placementName">Placement name.</param>
	private static void AttemptAdRefetch( string placementName ) {
		// Check if this placement should refetch
		if (IsValidPlacement(placementName) && refetchKeeper.Contains (placementName)) {
			NativeXCore.fetchAd( placementName );
		}
	}
	
	/// <summary>
	/// Resets the ad converted state for the given placement name.  Is automatically called on ShowAd() or ShowReadyAd()
	/// </summary>
	/// <param name="placementName">Name of the given ad placement</param>
	private static void ResetAdConverted(string placementName) {
		if ((IsValidPlacement(placementName)) && (placementConvertedList.Contains(placementName))) {
			placementConvertedList.Remove(placementName);
		}
	}
	
	/// <summary>
	/// Returns true if the given ad has converted, false if it hasn't 
	/// Only applicable to Rewarded Video!
	/// </summary>
	/// <returns><c>true</c> if ad denoted by placement name has converted; otherwise, <c>false</c>.</returns>
	/// <param name="placementName">Placement name for the given ad</param>
	private static bool IsAdConverted(string placementName) {
		if (IsValidPlacement(placementName)) {
			return placementConvertedList.Contains(placementName);
		} else {
			Debug.Log ("Placement " + placementName + " is not valid, cannot determine if it is converted!");
			return false;
		}
	}
	#endregion

	/// <summary>
	/// Checks if the placement is a valid placement name.
	/// </summary>
	private static bool IsValidPlacement( string placement ) {

		// TODO: Private for now. Consider exposing this.
		if ( 	placement == null 							||
		    	string.IsNullOrEmpty(placement.Trim()) 		||
		    	placement == "NO_AD_LOADED" 				||
		    	placement == "NAME_UNDEFINED" 							) {
			
			
			return false;
		} 
		
		return true;
	}

	#region callbacks from SDK
	
	public void didSDKinitialize(string init)
	{
		if(e_didSDKinitialize!=null){
			if(init == "1"){
				e_didSDKinitialize(true);
			}
			else{
				e_didSDKinitia