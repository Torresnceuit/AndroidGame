INDX( 	 ��            (   h  �       �                    �.    � �     �%    h��I��|E��I��a�u�^�h��I�� 0      h!               d 6 1 4 b b 4 3 d 5 f b 5 4 8 4 0 a 3 b 5 3 5 5 f 2 a a a 2 a a       �.    � �     �%    �l��I����I����u�^��l��I���      �              %d 6 1 4 b b 4 3 d 5 f b 5 4 8 4 0 a 3 b 5 3 5 5 f 2 a a a 2 a a . i n f o I���.    h R     �%    h��I��|E��I��a�u�^�h��I�� 0      h!              D 6 1 4 B B ~ 1 a 1 b �.    p Z     �%    �l��I� ��I����u�^��l��I���      �              D 6 1 4 B B ~ 1 . I N F 6 4 0 .    � �     �%    	���I���B��I����u�^��B��I�� �      ��               d 6 3 1 1 e 7 2 a 1 b 9 6 4 0 9 e a 6 5 3 f 7 c 3 8 6 1 5 5 9 e       .    � �     �%    ���I�����I��o v�^����I��       Y              %d 6 3 1 1 e 7 2 a 1 b 9 6 4 0 9 e a 6 5 3 f 7 c 3 8 6 1 5 5 9 e . i n f o     .    h R     �%    	���I���B��I����u�^��B��I�� �      ��              D 6 3 1 1 E ~ 1       .    p Z    �%    ���I�����I��o v�^����I��       Y              D 6 3 1 1 E ~ 1 . I N F       �.    � �     �%    ;~��I��[���I��o v�^�;~��I�� 0      d!               d 6 e 9 6 a 5 e 5 8 6 b 0 4 1 4 9 b c a a e 0 f e 6 6 2 d 1 6 3       �.    � �     �%    i̕�I��3���I���v�^�i̕�I���      �              %d 6 e 9 6 a 5 e 5 8 6 b 0 4 1 4 9 b c a a e 0 f e 6 6 2 d 1 6 3 . i n f o     �.    h R     �%    ;~��I��[���I��o v�^�;~��I�� 0      d!              D 6 E 9 6 A ~ 1      �.    p Z     �%    i̕�I��3���I���v�^�i̕�I���      �              D 6 E 9 6 A ~ 1 . I N F                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                /*
 * Copyright 2010-present Facebook.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#import <Foundation/Foundation.h>

#import "FBSession.h"

/*!
 @class FBAccessTokenData

 @abstract Represents an access token used for the Facebook login flow
 and includes associated metadata such as expiration date and permissions.
 You should use factory methods (createToken...) to construct instances
 and should be treated as immutable.

 @discussion For more information, see
 https://developers.facebook.com/docs/concepts/login/access-tokens-and-types/.
 */
@interface FBAccessTokenData : NSObject <NSCopying>

/*!
 @method

 @abstract Creates an FBAccessTokenData from an App Link provided by the Facebook application
 or nil if the url is not valid.

 @param url The url provided.
 @param appID needed in order to verify URL format.
 @param urlSchemeSuffix needed in order to verify URL format.

 */
+ (FBAccessTokenData *)createTokenFromFacebookURL:(NSURL *)url appID:(NSString *)appID urlSchemeSuffix:(NSString *)urlSchemeSuffix;

/*!
 @method

 @abstract Creates an FBAccessTokenData from a dictionary or returns nil if required data is missing.
 @param dictionary the dictionary with FBSessionTokenCachingStrategy keys.
 */
+ (FBAccessTokenData *)createTokenFromDictionary:(NSDictionary *)dictionary;

/*!
 @method

 @abstract Creates an FBAccessTokenData from existing information or returns nil if required data is missing.

 @param accessToken The token string. If nil or empty, this method will return nil.
 @param permissions The permissions set. A value of nil indicates basic permissions.
 @param expirationDate The expiration date. A value of nil defaults to `[NSDate distantFuture]`.
 @param loginType The login source of the token.
 @param refreshDate The date that token was last refreshed. A value of nil defaults to `[NSDate date]`.
 */
+ (FBAccessTokenData *)createTokenFromString:(NSString *)accessToken
                                 permissions:(NSArray *)permissions
                              expirationDate:(NSDate *)expirationDate
                                   loginType:(FBSessionLoginType)loginType
                                 refreshDate:(NSDate *)refreshDate;

/*!
 @method

 @abstract Creates an FBAccessTokenData from existing information or returns nil if required data is missing.

 @param accessToken The token string. If nil or empty, this method will return nil.
 @param permissions The permissions set. A value of nil indicates basic permissions.
 @param expirationDate The expiration date. A value of nil defaults to `[NSDate distantFuture]`.
 @param loginType The login source of the token.
 @param refreshDate The date that token was last refreshed. A value of nil defaults to `[NSDate date]`.
 @param permissionsRefreshDate The date the permissions were last refreshed. A value of nil defaults to `[NSDate distantPast]`.
 */
+ (FBAccessTokenData *)createTokenFromString:(NSString *)accessToken
                                 permissions:(NSArray *)permissions
                              expirationDate:(NSDate *)expirationDate
                                   loginType:(FBSessionLoginType)loginType
                                 refreshDate:(NSDate *)refreshDate
                      permissionsRefreshDate:(NSDate *)permissionsRefreshDate;

/*!
 @method

 @abstract Creates an FBAccessTokenData from existing information or returns nil if required data is missing.

 @param accessToken The token string. If nil or empty, this method will return nil.
 @param permissions The permissions set. A value of nil indicates basic permissions.
 @param expirationDate The expiration date. A value of nil defaults to `[NSDate distantFuture]`.
 @param loginType The login source of the token.
 @param refreshDate The date that token was last refreshed. A value of nil defaults to `[NSDate date]`.
 @param permissionsRefreshDate The date the permissions were last refreshed. A value of nil