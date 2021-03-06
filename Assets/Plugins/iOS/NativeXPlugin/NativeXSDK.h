//
//  NativeXRewardInfo.h
//  NativeXSDK
//
//  Created by Joel Braun on 2015.02.09.
//
//

#import <Foundation/Foundation.h>
#include "NativeXReward.h"

@interface NativeXRewardInfo : NSObject

/*
 * Array of NativeXRewards detailing exact rewards to be given to the user
 */
@property (nonatomic, readonly) NSArray* rewards;

/**
 *  Create CurrencyInfo object using API JSON response
 *
 *  @param APIResult    NSDictionary of API results
 *
 *  @return an object version of json response
 */
-(id)initWithRedeemBalancesResult:(NSDictionary *)APIResult;

/**
 *  Calling this method will display a native iOS alert view on success
 */
-(void)showRedeemAlert;

/**
 *  converts object to dictionary (to prep for JSON)
 *
 *  @return NSDictionary version of object
 */
-(id)proxyForJson;

@end
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    //
//  NativeXSDK.h
//  NativeXSDK
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Patrick Brennan on 10/6/11.
//  Copyright 2013 NativeX. All rights reserved.

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "NativeXAdView.h"
#import "NativeXCurrency.h"
#import "NativeXRedeemedCurrencyInfo.h"
#import "NativeXReward.h"
#import "NativeXRewardInfo.h"
#import "NativeXInAppPurchaseTrackRequest.h"

/** These are predefined placements that can be associated with Multi-Format Ads 
 *
 *  AD PLACEMENTS:
 *  - NativeX Ad Placement: a pre-defined list of game interaction points.
 *  - Custom Placement: a string that describes the interaction point of an ad within your game.
 *
 *  BENEFITS:
 *  - Higher eCPMs
 *  - Control over ad format
 *  - Enhanced Reporting
 *  - Turn off ads per placement
 **/

typedef enum {
    kAdPlacementGameLaunch,              //"Game Launch"
    kAdPlacementMainMenuScreen,          //"Main Menu Screen"
    kAdPlacementPauseMenuScreen,         //"Pause Menu Screen"
    kAdPlacementPlayerGeneratedEvent,    //"Player Generated Event"
    kAdPlacementLevelCompleted,          //"Level Completed"
    kAdPlacementLevelFailed,             //"Level Failed"
    kAdPlacementPlayerLevelsUp,          //"Player Levels Up"
    kAdPlacementP2P_CompetitionWon,      //"P2P competition won"
    kAdPlacementP2P_CompetitionLost,     //"P2P competition lost"
    kAdPlacementStoreOpen,               //"Store Open"
    kAdPlacementExitFromApp              //"Exit Ad from Application"
} NativeXAdPlacement;

//portrait: 320x50 or 768x66
//landscape: 480x32 or 1024x66
typedef enum {
    kBannerPositionTop,
    kBannerPositionBottom,
} NativeXBannerPosition;

@protocol NativeXSDKDelegate;

/** Main class for NativeXSDK */
@interface NativeXSDK : NSObject

@property (nonatomic, strong) id<NativeXSDKDelegate> delegate;
@property (nonatomic, strong) UIViewController *presenterViewController;
@property (nonatomic, assign) BOOL shouldOutputDebugLog;

#pragma mark - Monetization API

/** 
 * Provides access to the NativeXSDK shared object.
 *
 * @return a singleton NativeXSDK instance.
 */
+ (NativeXSDK *)sharedInstance;

/** 
 * Provides access to the NativeXSDK version
 *
 * @return NativeXSDK Version
 */
- (NSString *)getSDKVersion;

/**
 * Use this method to get sessionId for current session
 *
 * @return the current sessionId
 */
- (NSString *)getSessionId;

/**
 * Create a session with NativeX offer network.
 * Call this in AppDidFinishLaunchingWithOptions{}
 *
 * @param   appId (required)
 *          the unique Identifier you receive from NativeX
 */
- (void)createSessionWithAppId:(NSString *)appId;

/**
 * Create a session with NativeX offer network.
 * Call this in AppDidFinishLaunchingWithOptions{}
 *
 * @param   appId (required) 
 *          the unique Identifier you receive from NativeX
 * @param   publisherUserId (optional) 
 *          an id that can be used for publisher currency postback
 */
- (void)createSessionWithAppId:(NSString *)appId
       andPublisherUserId:(NSString *)publisherUserId;

/** 
 * Call to fetch and return balances for user
 * Automatically called once on create session
 *
 * @warning **Deprecated**: Please use `<redeemRewards>` instead.
 */
- (void)redeemCurrency __deprecated_msg("method is deprecated; use redeemRewards instead");


/**
 * Call to fetch and return reward balances for user
 * Automatically called once on create session
 */
- (void)redeemRewards;


#pragma mark - NativeX Ad Methods

/**
 * Checks to see if there is an ad of this placement ready 
 * to show.
 *
 * @param   placement (required)
 *          A pre-defined set of game interaction points
 */
- (BOOL)isAdReadyWithPlacement:(NativeXAdPlacement)placement;

/**
 * Checks to see if there is an ad of this placement ready
 * to show.
 *
 * @param   customPlacement (required)
 *          A string that describes the interaction point of an ad within your game
 */
- (BOOL)isAdReadyWithCustomPlacement:(NSString*)customPlacement;

/**
 * Show a multi-format ad with a NativeX placement from key window,
 * used for targeting certain ads for certain in app placements.
 *
 * @param   placement (required) 
 *          A pre-defined set of game interaction points
 */
- (void)showAdWithPlacement:(NativeXAdPlacement)placement;

/**
 * Show a multi-format ad  only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   placement (required)
 *          A pre-defined set of game interaction points
 */
- (void)showReadyAdWithPlacement:(NativeXAdPlacement)placement;

/**
 * Show a multi-format ad  only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   customPlacement (required)
 *          A string that describes the interaction point of an ad within your game
 */
- (void)showReadyAdWithCustomPlacement:(NSString *)customPlacement;

/**
 * Show a multi-format ad  only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   placement (required)
 *          A pre-defined set of game interaction points
 * @param   aDelegate (required)
 *          Delegate
 */
- (void)showReadyAdStatelessWithPlacement:(NativeXAdPlacement)placement delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Show a multi-format ad  only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   placement (required)
 *          A pre-defined set of game interaction points
 * @param   aDelegate (required)
 *          Delegate
 * @param rootViewController (required)
 *          View controller from which to present the ad view controller
 */
- (void)showReadyAdStatelessWithPlacement:(NativeXAdPlacement)placement delegate:(id<NativeXAdViewDelegate>)aDelegate rootViewController:(UIViewController*)rootViewController;


/**
 * Show a multi-format ad (in Stateless Mode) only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   customPlacement (required)
 *          A string that describes the interaction point of an ad within your game
 * @param   aDelegate (required)
 *          Delegate
 */
-(void)showReadyAdStatelessWithCustomPlacement:(NSString *)customPlacement delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Show a multi-format ad (in Stateless Mode) only if it is loaded and ready to instantly show with a custom
 * placement from key window, used for targeting certain ads for certain in app placements.
 *
 * @param   customPlacement (required)
 *          A string that describes the interaction point of an ad within your game
 * @param   aDelegate (required)
 *          Delegate
 * @param rootViewController (required)
 *          View controller from which to present the ad view controller
 */
-(void)showReadyAdStatelessWithCustomPlacement:(NSString *)customPlacement delegate:(id<NativeXAdViewDelegate>)aDelegate rootViewController:(UIViewController*)rootViewController;

/**
 * Show a multi-format ad with a custom placement from key window,
 * used for targeting certain ads for certain in app placements.
 * 
 * @param   customPlacement (required) 
 *          A string that describes the interaction point of an ad within your game
 */
- (void)showAdWithCustomPlacement:(NSString *)customPlacement;

/**
 * Fetch/cache a multi-format ad that presents modally (fullscreen)
 *
 * @param   placement (required) 
 *          A pre-defined set of game interaction points
 * @param   aDelegate (optional) 
 *          used to set delegate
 */
- (void)fetchAdWithPlacement:(NativeXAdPlacement)placement delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Fetch/cache a multi-format ad that presents modally (fullscreen)
 * 
 * @param   customPlacement (required) 
 *          A string that describes the interaction point of an ad within your game
 * @param   aDelegate (optional) 
 *          used to set delegate
 */
- (void)fetchAdWithCustomPlacement:(NSString *)customPlacement delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Fetch/cache a multi-format ad that presents modally (fullscreen)
 *
 * @param   placement (required)
 *          A pre-defined set of game interaction points
 * @param   appId (required)
 *          the unique app identifier you receive from NativeX
 * @param   aDelegate (required)
 *          Delegate
 */
- (void)fetchAdStatelessWithPlacement:(NativeXAdPlacement)placement withAppId:(NSString*)appId delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Fetch/cache a multi-format ad (in Stateless Mode) that presents modally (fullscreen)
 *
 * @param   customPlacement (required)
 *          A string that describes the interaction point of an ad within your game
 * @param   appId (required)
 *          the unique app identifier you receive from NativeX
 * @param   aDelegate (required)
 *          Delegate
 */
- (void)fetchAdStatelessWithCustomPlacement:(NSString *)customPlacement withAppId:(NSString*)appId delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Dismiss the adview (Insterstitial)
 *
 * @param   placement (required)
 *          A pre-defined game interaction point
 *
 */
- (void)dismissAdWithPlacement:(NativeXAdPlacement)placement;

/**
 * Dismiss the adview (Insterstitial)
 *
 * @param   placement (required)
 *          A custom game interaction point
 *
 */
- (void)dismissAdWithCustomPlacement:(NSString*)placement;

#pragma mark NativeX Banner Methods

/**
 *  Fetch/cache a multi-format banner that presents inline
 *
 *  @param  placement  (required)
 *          A pre-defined game interaction point
 *  @param  adPosition (required)
 *          A pre-defined ad position (top or bottom)
 *  @param  aDelegate  (optional)
 *          delegate object for protocol methods
 */
- (void)fetchBannerWithPlacement:(NativeXAdPlacement)placement
                        position:(NativeXBannerPosition)adPosition
                        delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 *  Show a multi-format banner with a NativeX placement from keywindow
 *
 *  @param  placement (required)
 *          A pre-defined game interaction point
 *  @param  adPosition (required)
 *          A pre-defined NativeX ad position (top or botton)
 */
- (void)showBannerWithPlacement:(NativeXAdPlacement)placement position:(NativeXBannerPosition)adPosition;

/**
 *  Fetch/cache a multi-format banner that presents inline
 *
 *  @param  placement  (required)
 *          A string representation of game interaction point
 *  @param  adPosition (required)
 *          A pre-defined NativeX ad position (top or bottom)
 *  @param  aDelegate  (optional)
 *          delegate object