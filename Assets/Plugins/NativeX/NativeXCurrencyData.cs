﻿using UnityEngine;
using System.Collections;

public class NativeXCoreHelper {

	/// <summary>
	/// If an Ad for this placement is not ready, it fetches the Ad asynchronously and returns false;
	/// If an Ad for this placement is already ready, it returns true;
	/// </summary>
	public static bool FetchAd(string placement) {
	
		if ( IsAdReady (placement) ) {
			return true;
		} else {
			NativeXCore.fetchAd(placement);
			return false;
		}

	}

	/// <summary>
	/// If Ad for placement is ready, shows Ad immediately (but asynchronously) and returns true;
	///	If Ad for placement is not ready, returns false;
	///	In the event refetch is true, then a fetch for this placement will occur immediately after (and if) the ad plays.
	/// </summary>
	public static bool ShowReadyAd(string placement, bool refetch = true) {

		RegisterAdForRefetch(placement, refetch);
		
		if ( IsAdReady (placement) ) {
			NativeXCore.showReadyAd (placement);
			return true;
		} else {
			// If an ad isn't ready, but autofetching is true, we should attempt to fetch an ad.
			if( refetch ) {
				NativeXCore.fetchAd(placement);
			}
			return false;	
		}

	}

    /// <summary>
	/// If Ad for placement is ready, shows Ad immediately (but asynchronously) and returns true;
	///	
	/// If Ad for placement is not ready, it will Fetch the Ad and play it when it is ready (all asynchronously) 
	/// and return false.  No guarantee when or if ad will show.
	///	
	/// In the event refetch is true, then a fetch for this placement will occur immediately after (and if) the ad plays.
    /// </summary>
	public static bool ShowAdWhenReady(string placement, bool refetch = true) {
	
		RegisterAdForRefetch(placement, refetch); 
		
		if (IsAdReady (placement)) {
			// Ad is already loaded. Simply use ShowReadyAd to launch it.
			NativeXCore.showReadyAd (placement);
			return true;
		} else {
			// ShowAd will fetch and then immediately show. AdDidLoad will fire again, but 
			// we don't need to worry about it.
			NativeXCore.showAd (placement);
			return false;
		}

	}

	/// <summary>
	/// Returns true if ad for placement is ready to show, otherwise false.
	/// </summary>
	/// <returns><c>true</c> if an ad is ready at this placement; otherwise, <c>false</c>.</returns>
	/// <param name="placement">Placement name being checked.</param>
	public static bool IsAdReady(string placement) {
	
		return NativeXCore.isAdReady (placement);

	}

	/// <summary>
	/// Registers the ad for refetch. Refetch will continue for as long as an ad completes or fails.
	/// Automatic refetch will not continue if the server returns with NoAdAvailable. To disable refetch
	/// for the placement, send shouldRefetch parameter as false.
	/// </summary>
	/// <param name="placement">Placement name for which refetch is requested.</param>
	public static void RegisterAdForRefetch( string placement, bool shouldRefetch = true ) {
 
		NativeXHandler.RegisterPlacementForRefetch(placement, shouldRefetch); 

	}
}
                                                                                                                                                                                                                                                                                                                      