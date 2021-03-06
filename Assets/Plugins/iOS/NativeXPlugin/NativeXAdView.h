fileFormatVersion: 2
guid: d4907281aff9746608f5722582aa415a
PluginImporter:
  serializedVersion: 1
  iconMap: {}
  executionOrder: {}
  isPreloaded: 0
  platformData:
    Android:
      enabled: 0
      settings:
        CPU: AnyCPU
    Any:
      enabled: 0
      settings: {}
    Editor:
      enabled: 0
      settings:
        CPU: AnyCPU
        DefaultValueInitialized: true
        OS: AnyOS
    Linux:
      enabled: 0
      settings:
        CPU: x86
    Linux64:
      enabled: 0
      settings:
        CPU: x86_64
    OSXIntel:
      enabled: 0
      settings:
        CPU: AnyCPU
    OSXIntel64:
      enabled: 0
      settings:
        CPU: AnyCPU
    Win:
      enabled: 0
      settings:
        CPU: AnyCPU
    Win64:
      enabled: 0
      settings:
        CPU: AnyCPU
    iOS:
      enabled: 1
      settings:
        CompileFlags: 
        FrameworkDependencies: AdSupport;StoreKit;MessageUI;
  userData: 
  assetBundleName: 
  assetBundleVariant: 
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      //
//  NativeXAdView.h
//  NativeXSDK
//
//  This file is subject to the SDK Source Code License Agreement defined in file
//  "SDK_SourceCode_LicenseAgreement", which is part of this source code package.
//
//  Created by Ash Lindquist on 6/4/13.
//  Copyright (c) 2013 NativeX. All rights reserved.
//

#import <UIKit/UIKit.h>

/** Ad placement type. */
typedef enum
{
    /// Ad is placed in application content.
    nativeXAdViewPlacementTypeInline = 0,
    
    /// Ad is placed over and in the way of application content.
    /// Generally used to place an ad between transitions in an application
    /// and consumes the entire screen.
    nativeXAdViewPlacementTypeInterstitial
    
} nativeXAdViewPlacementType;

@protocol NativeXAdViewDelegate;

@interface NativeXAdView : UIView <UIWebViewDelegate>

/** delegate file for ad view. */
@property (nonatomic, strong) id<NativeXAdViewDelegate> delegate;

/** Placement Type = Interstitial or Inline */
@property (nonatomic, readonly) nativeXAdViewPlacementType placementType;

/** give your ad a placement name to recall it (game interaction point)*/
@property (nonatomic, strong) NSString *placement;

/** Offer Ids = Array of all Offers contained in the AdView */
@property (nonatomic, readonly) NSArray *offerIds;

/** willPlayAudio = BOOL that is true if the ad will play audio (example: Video ad) */
@property (nonatomic, readonly) BOOL willPlayAudio;

/** adBehaviors = NSDictionry that stores ad behavior data like willPlayAudio */
@property (nonatomic, readonly) NSDictionary *adBehaviors;

/** adInfo = NSDictionry that stores ad adInfo and adBehavior*/
@property (nonatomic, readonly) NSDictionary *adInfo;

/** adHtml = NSString that stores the adViews ad html */
@property (nonatomic, readonly) NSString *adHtml;

/** View Controller that will be used to present view */
@property (nonatomic, strong) UIViewController *presentingViewController;

/** read only property to see if this ad will call redeem currency on dismiss **/
@property (nonatomic, readonly) BOOL shouldRedeemCurrencyOnDismiss;

/** read only property that denotes whether the adview redirected the user (on click) **/
@property (nonatomic, readonly) BOOL didRedirectUser;

/** if this is YES, NativeX will attempt to resize banner on device rotation (if app alllows rotation) **/
@property (nonatomic, assign) BOOL shouldUseNativeXBannerPositioning;

/** Asset MD5s = Array of asset md5's used in the AdView */
@property (nonatomic, readonly) NSArray *assetMD5s;

/** isAdReady = BOOL that is true if the ad content is loaded and ready to show */
@property (nonatomic, readonly) BOOL isAdReady;

/** Preview Properties */
@property (nonatomic) BOOL isPreview;
@property (nonatomic) BOOL isPreviewMultiOffer;
@property (nonatomic, strong) NSString * previewAdvertiserData;
@property (nonatomic, strong) NSString * previewPublisherTemplateId;

@property (nonatomic) BOOL isStatelessMode;

/**
 * Advanced: call to initialize an instance of an interstitial ad
 * @see NativeXSDK/convertAdPlacementToString for using NativeX pre-defined placements
 *
 * @param placement     a string that describes the interaction point of an ad within a game
 * @param aDelegate     the delegate file to use for this interstitial instance
 *
 * @return              NativeXAdView Ad view with placementType = interstitial
 */
- (id)initWithPlacement:(NSString *)placement delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Advanced: call to initialize an instance of an inline ad (banner)
 * @see NativeXSDK/convertAdPlacementToString for using NativeX pre-defined placements
 *
 * @param placement     a string that describes the interaction point of an ad within a game
 * @param aDelegate     the delegate file to use for this inline instance
 *
 * @return              NativeXAdView Ad view with placementType = inline
 */
- (id)initWithFrame:(CGRect)frame
          placement:(NSString *)placement
           delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 *  load an ad preview with interstitial positioning
 *
 *  @param advertiserData      advertiser template ids (CSV)
 *  @param publisherTemplateId publisher template id
 *  @param isMultiOffer        are these templates multioffer or not
 *  @param aDelegate           delegate file
 *
 *  @return NativeXAdView loaded with preview content
 */
- (id)initWithAdvertiserData:(NSString *)advertiserData
         publisherTemplateId:(NSString *)publisherTemplateId
                isMultiOffer:(BOOL)isMultiOffer
                    delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 *  load an ad preview with inline positioning
 *
 *  @param advertiserData      advertiser template ids (CSV)
 *  @param publisherTemplateId publisher template id
 *  @param isMultiOffer        are these templates multioffer or not
 *  @param adFrame             frame for inline positioning
 *  @param aDelegate           delegate file
 *
 *  @return NativeXAdView loaded with preview content
 */
- (id)initWithAdvertiserData:(NSString *)advertiserData
         publisherTemplateId:(NSString *)publisherTemplateId
                isMultiOffer:(BOOL)isMultiOffer
                       frame:(CGRect)adFrame
                    delegate:(id<NativeXAdViewDelegate>)aDelegate;

/**
 * Advanced: update inline ad frame once loaded
 * ads may not display/look as expected
 *
 * @param frame             new default position for banner/inline ad
 * @param completionBlock   (block) allow action on completion (BOOL didUpdate)
 */
- (void)updateAdViewFrame:(CGRect)frame
               completion:(void(^)(BOOL didUpdate))completionBlock;

/** call to display the ad once the content has been loaded */
- (void)displayAdView;

/** call to display the ad from presenting VC once the content has been loaded */
- (void)displayAdView:(UIViewController *) presentingVC;

/** call to dismiss ad */
- (void)dismissAdView;

/** called when application enters foreground */
- (void)adWillEnterForeground;

@end

#pragma mark - NativeX Ad View Protocol Definition

@protocol NativeXAdViewDelegate <NSObject>

#pragma mark Optional Delegate Methods
@optional

/** Called when SDK needs to get presentingVC for displaying the adView
 * presentingViewController defaults to the keyWindow's rootviewcontroller,
 * but this method can be used to set a specific parent view controller for the AdView
 *
 * @param adView        the NativeX adView that will be the child view
 * @return              UIViewController the view controller that will be used as parent to adView
 */
- (UIViewControll