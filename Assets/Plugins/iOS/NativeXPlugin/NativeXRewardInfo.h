//
//  NativeXCurrencyInfo.h
//  NativeXSDK
//
//  Created by Ash Lindquist on 11/15/13.
//
//

#import <Foundation/Foundation.h>

__deprecated_msg("Class is deprecated along with [redeemCurrency] method and [nativeXSDKDidRedeemWithCurrencyInfo] delegate.  Please use [redeemRewards], [nativeXSDKDidRedeemWithRewards], NativeXRewardInfo and NativeXReward classes instead.")
@interface NativeXRedeemedCurrencyInfo : NSObject

/**
 *  An array of messages that can be displayed to the user on successful redemption
 */
@property (nonatomic, readonly) NSArray *messages;

/**
 *  an array of balances that should be paid to user. Each object in the array represents one type of currency for your game
 */
@property (nonatomic, readonly) NSArray *balances;

/**
 *  an array of identifiers you can us