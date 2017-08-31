//
//  NativeXReward.h
//  NativeXSDK
//
//  Created by Joel Braun on 2015.02.06.
//
//

#import <Foundation/Foundation.h>

@interface NativeXReward : NSObject


@property (nonatomic, readonly) NSNumber* amount;
@property (nonatomic, readonly) NSString* rewardName;
@property (nonatomic, readonly) NSString* rewardId;

-(id)initWithDictionary:(NSDictionary *)dictionary;
- (id)proxyForJson;

@end
