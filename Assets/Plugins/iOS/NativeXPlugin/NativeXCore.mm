//
//  NativeXCore.h
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import <Foundation/Foundation.h>
#import "NativeXSDK.h"
#import "NativeXAdView.h"
//
//extern UIView * UnityGetGLView();
//extern UIViewController * UnityGetGLViewController();

@interface NativeXCore : NSObject <NativeXSDKDelegate, NativeXInAppPurchaseTrackDelegate, NativeXAdViewDelegate >
{
@private
    BOOL showInterstitial;
    
}

@property (nonatomic) BOOL showMessages;
@property (nonatomic, strong) NativeXAdView* bannerView;

+ (NativeXCore*) instance;

-(void)startWithApplicationId:(NSString*)appId publisherId:(NSString*)pubId enableLogging:(bool) enableLogging;
-(BOOL)isAdReady:(NSString*)name;
-(void)fetchAd:(NSString*)name;
-(void)showAd:(NSString*)name;
-(void)showReadyAd:(NSString*)name;
-(void)dismissAd:(NSString*)name;
//-(void)fetchBanner:(NSString*)name withRect:(CGRect)position;
//-(void)showBanner:(NSString*)name withRect:(CGRect)position;
-(void)fetchBanner:(NSString *)name withPosition:(NSString*)position;
-(void)showBanner:(NSString *)name withPosition:(NSString*)position;
-(void)dismissBanner:(NSString*)name;
-(void)actionTaken:(NSString*)actionId;
-(void)redeemCurrency:(bool)show __deprecated_msg("method is deprecated; use redeemRewards instead");
-(void)redeemRewards:(bool)show;
-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord*)record;
//-(void)close;

@property (nonatomic, retain) NSString *URL;


@end
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          //
//  NativeXCore.m
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import "NativeXCore.h"
#import "NativeXPublisherSBJsonWriter.h"

#define kNativeXTestAppURL		@"NativeXTestAppURL"

UIViewController *UnityGetGLViewController();

void UnityPause( bool pause );

void UnitySendMessage( const char * className, const char * methodName, const char * param );

static NativeXCore *sharedInstance;

@implementation NativeXCore

+ (void)inititialize {
    if(!sharedInstance) {
        sharedInstance = [[[self class]alloc]init];
        }
}

+ (NativeXCore*) instance
{
    if(!sharedInstance) {
        return sharedInstance = [[[self class]alloc]init];
    }
    return sharedInstance;
    
}

-(void)startWithApplicationId:(NSString*)appId publisherId:(NSString*)pubId enableLogging:(bool)enableLogging
{
    [[NSUserDefaults standardUserDefaults] setObject:nil forKey:@"NativeXPreviousCacheTime"];
    [[NSUserDefaults standardUserDefaults] synchronize];
    if([[[NSBundle mainBundle] bundleIdentifier] isEqualToString:@"com.w3i.W3iUnityTest"]){
        [[NSUserDefaults standardUserDefaults] setObject:_URL forKey:kNativeXTestAppURL];
    }
    [[NSUserDefaults standardUserDefaults] setObject:@"Unity" forKey:@"NativeXBuildType"];
    [[NativeXSDK sharedInstance] createSessionWithAppId:appId andPublisherUserId:pubId];
    [[NativeXSDK sharedInstance] setDelegate:(id)self];
    if(enableLogging){
        [[NativeXSDK sharedInstance] setShouldOutputDebugLog:YES];
    }else{
        [[NativeXSDK sharedInstance] setShouldOutputDebugLog:NO];
    }
}

-(BOOL) isAdReady:(NSString *)name
{
    return [[NativeXSDK sharedInstance] isAdReadyWithCustomPlacement:name];
}

-(void)fetchAd:(NSString*)customPlacement
{
    [[NativeXSDK sharedInstance] fetchAdWithCustomPlacement:customPlacement delegate:self];
}

-(void)showAd:(NSString *)customPlacement
{
    [[NativeXSDK sharedInstance] fetchAdWithCustomPlacement:customPlacement delegate:self];
    [[NativeXSDK sharedInstance] showAdWithCustomPlacement:customPlacement];
}

-(void)showReadyAd:(NSString *)customPlacement
{
    [[NativeXSDK sharedInstance] showReadyAdWithCustomPlacement:customPlacement];
}

-(void)dismissAd:(NSString *)name
{
    [[NativeXSDK sharedInstance] dismissAdWithCustomPlacement:name];
}

//-(void)fetchBanner:(NSString *)name withRect:(CGRect)position
//{
//    self.bannerView = [[NativeXAdView alloc] initWithFrame:position placement:name delegate:self];
//    
//}
//
//-(void)showBanner:(NSString *)name withRect:(CGRect)position
//{
//        if(self.bannerView)
//        {
//            [self.bannerView displayAdView];
//        }else{
//            self.bannerView = [[NativeXAdView alloc] initWithFrame:position placement:name delegate:self];
//            [self.bannerView displayAdView];
//        }
//}

-(void)fetchBanner:(NSString *)name withPosition:(NSString *)position
{
    if([position isEqualToString:@"NATIVEX_BANNER_TOP"]){
        [[NativeXSDK sharedInstance] fetchBannerWithCustomPlacement:name position:kBannerPositionTop delegate:self];
        return;
    }else if ([position isEqualToString:@"NATIVEX_BANNER_BOTTOM"]){
        [[NativeXSDK sharedInstance] fetchBannerWithCustomPlacement:name position:kBannerPositionBottom delegate:self];
        return;
    }
}

-(void)showBanner:(NSString *)name withPosition:(NSString *)position
{
    if([position isEqualToString:@"NATIVEX_BANNER_TOP"]){
        [[NativeXSDK sharedInstance] fetchBannerWithCustomPlacement:name position:kBannerPositionTop delegate:self];
        [[NativeXSDK sharedInstance] showBannerWithCustomPlacement:name position:kBannerPositionTop];
        return;
    }else if ([position isEqualToString:@"NATIVEX_BANNER_BOTTOM"]){
        [[NativeXSDK sharedInstance] fetchBannerWithCustomPlacement:name position:kBannerPositionBottom delegate:self];
        [[NativeXSDK sharedInstance] showBannerWithCustomPlacement:name position:kBannerPositionBottom];
        return;
    }
}

-(void)dismissBanner:(NSString *)name
{
    [[NativeXSDK sharedInstance] dismissBannerWithCustomPlacement:name];
}

-(void)actionTaken:(NSString *)actionId
{
    [[NativeXSDK sharedInstance] actionTakenWithActionID:actionId];
}

-(void)redeemCurrency:(bool)show
{
    // force use of new reward classes
    self.showMessages = show;
    [[NativeXSDK sharedInstance] redeemRewards];
}

-(void)redeemRewards:(bool)show
{
    self.showMessages = show;
    [[NativeXSDK sharedInstance] redeemRewards];
}

-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord *)record
{
    [[NativeXSDK sharedInstance]trackInAppPurchaseWithTrackRecord:record delegate:self];
}

// TODO: actually hook this up to sdk
//-(void)close
//{
//    [[NativeXSDK sharedInstance] close];
//}

- (uint64_t)folderSizeInBytes:(NSString *)folderPath
{
    
    NSArray *filesArray = [[NSFileManager defaultManager] subpathsOfDirectoryAtPath:folderPath error:nil];
    NSEnumerator *filesEnumerator = [filesArray objectEnumerator];
    NSString *fileName;
    uint64_t fileSize = 0;
    
    while (fileName = [filesEnumerator nextObject]) {
        NSDictionary *fileDictionary = [[NSFileManager defaultManager] attributesOfItemAtPath:[folderPath stringByAppendingPathComponent:fileName] error:nil];
        fileSize += [fileDictionary fileSize];
    }
    
    return fileSize;
}


//_________________________________________________________________________________________________
//Publisher Delegates
//_________________________________________________________________________________________________
#pragma mark NativeXSDKDelegates

-(void)nativeXSDKDidCreateSession
{
    UnitySendMessage("NativeXHandler", "didSDKinitialize", "1");
    UnitySendMessage("NativeXHandler", "sessionId", [[[NativeXSDK sharedInstance] getSessionId] UTF8String]);
}

-(void)nativeXSDKDidFailToCreateSession:(NSError *)error
{
    NSLog(@"SDK Inititalization failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "didSDKinitialize", "0");
}

-(void)nativeXSDKDidRedeemWithRewardInfo:(NativeXRewardInfo *)rewardInfo
{
    if (rewardInfo != nil) {
        NativeXPublisherSBJsonWriter *jsonWriter = [NativeXPublisherSBJsonWriter new];
        NSString* json = [jsonWriter stringWithObject:rewardInfo];
        if (json == nil) {
            NSLog(@"nativeXSDKDidRedeemWithRewardInfo - Failed to parse NativeXRewardInfo into Json");
        } else {
            NSLog(@"nativeXSDKDidRedeemWithRewardInfo - JSON(inXCode): %s", [json UTF8String]);
            UnitySendMessage("NativeXHandler", "balanceTransfered", [json UTF8String]);
            if (self.showMessages) {
                [rewardInfo showRedeemAlert];
            }
        }
    } else {
        NSLog(@"nativeXSDKDidRedeemWithRewardInfo - No balance returned");
    }
}

-(void)didRedeemWithBalances:(NSArray *)balances andReceiptId:(NSString *)receiptId
{
    NSLog(@"We hit the old redeem balance delegate call");
}

-(void)nativeXSDKDidRedeemWithError:(NSError *)error
{
    NSLog(@"Redemption failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "actionFailed", "0");
} 

-(void)SDKWillRedirectUser
{
    UnitySendMessage("NativeXHandler", "userLeavingApplication", "1");
}

#pragma mark NativeXAdViewDelegates

-(UIViewController *)presentingViewControllerForAdView:(NativeXAdView *)adView
{
    NSLog(@"We have hit the Instuction View");
    return UnityGetGLViewController();
}

//_________________________________________________________________________________________________
//Ad Delegates
//_________________________________________________________________________________________________
-(void)nativeXAdView:(NativeXAdView *)adView didLoadWithPlacement: