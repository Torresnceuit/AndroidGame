t)placement;

/**
 * Dismiss the adview (Banner)
 *
 * @param   placement (required)
 *          A custom game interaction point
 *
 */
- (void)dismissBannerWithCustomPlacement:(NSString*)placement;

#pragma mark NativeX Ads Common Methods

/**
 *  Converts a NativeX pre-defined placement to a string
 *
 *  @param  placement (required)
 *          NativeX pre-defined placement using enum
 *
 *  @return NSString representation of NativeX placement
 */
- (NSString *)convertAdPlacementToString:(NativeXAdPlacement)placement;

#pragma mark - In App Purchase Tracking (IAPT) methods
/**
 * Used for In App Purchase Tracking
 * 
 * @param   trackRecord     
 *          data record of purchase
 * @param   delegate
 *          used to set delegate file
 * 
 * @return  NativeXInAppPurchaseRequest
 */
- (NativeXInAppPurchaseTrackRequest *)trackInAppPurchaseWithTrackRecord:(NativeXInAppPurchaseTrackRecord *)trackRecord
                                                               delegate:(id<NativeXInAppPurchaseTrackDelegate>)delegate; //if the delegate is about to be deallocated clear return value's delegate property

#pragma mark - NativeX Advertiser API (CPE Campaigns)

/**
 * call this to connect to NativeX and inform that the app "actionID" was performed
 * 
 * @param   actionID (required) 
 *          the unique Action Identifier for cost-per-event campaigns, you receive from NativeX
 */
- (void)actionTakenWithActionID:(NSString*)actionID;

@end

#pragma mark - Monetization Protocol Definition

@protocol NativeXSDKDelegate <NSObject>

#pragma mark Required Delegate Methods
@required

/** Called when the Offer Wall is successfully initialized. */
- (void)nativeXSDKDidCreateSession;

/** Called when there is an error trying to initialize the Offer Wall. 
 *
 * @param   error
 *          reason why create session cal