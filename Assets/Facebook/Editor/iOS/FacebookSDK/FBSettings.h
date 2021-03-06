me:
  mainRepresentation:
    name: pollfish
    thumbnail:
      m_Format: 5
      m_Width: 16
      m_Height: 16
      m_RowBytes: 64
      image data: 1024
      _typelessdata: 00af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af343700af3437ffea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea464bffea464bffea464bffea4c51ffea484dffea494effea484dffea484cffea4b50ffea484dffea474cffea474cffea454affea454affea454affea454affee6c70fff29598fff29799fff4a9abfff3a1a3fff4a4a6fff39c9ffff2979afff2989bfff4a7a9fff18b8efff28e91ffea454affea454affea454affea454affee6d71fff6babcfff29598fff29093fff29497fff29294fff5aeb0fff39d9fffef797dffef7b7efff6b6b8ffed6469ffea454affea454affea454affea454affea454affea4a4fffea454affea454affeb4c51ffeb4c51ffeb5054ffeb565affea454affea474cffeb4b50ffea454affea454affea454affea454affea454affea454affea454affea454affeb5257ffea454affeb5459fff18689ffee7175ffeb4f53ffea454affea454affea454affea454affea454affea454affea454affea454affea454affea454afff39c9efff4a7a9fff4a6a8fff5a9abfff4a4a6fff4a7a9ffea464bffea454affea454affea454affea454affea454affea454affea454affea454affea454afff07f82ffeb5559ffef7b7efff08083ffef767affef7377ffea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affeb5055ffea484dffea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affea454affd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44ffd63f44
    object: {fileID: 2800000, guid: af98ed1337cca4f598096f6cf1e34226, type: 3}
    thumbnailClassID: 28
    flags: 1
    scriptClassName: 
  representations: []
  labels:
    m_Labels: []
  assetImporterClassID: 1006
  externalReferencesForValidation: []
AssetInfo_______�	                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          /*
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
#import <CoreGraphics/CGBase.h>

#import "FBSDKMacros.h"

/*
 * Constants defining logging behavior.  Use with <[FBSettings setLoggingBehavior]>.
 */

/*! Log requests from FBRequest* classes */
FBSDK_EXTERN NSString *const FBLoggingBehaviorFBRequests;

/*! Log requests from FBURLConnection* classes */
FBSDK_EXTERN NSString *const FBLoggingBehaviorFBURLConnections;

/*! Include access token in logging. */
FBSDK_EXTERN NSString *const FBLoggingBehaviorAccessTokens;

/*! Log session state transitions. */
FBSDK_EXTERN NSString *const FBLoggingBehaviorSessionStateTransitions;

/*! Log performance characteristics */
FBSDK_EXTERN NSString *const FBLoggingBehaviorPerformanceCharacteristics;

/*! Log FBAppEvents interactions */
FBSDK_EXTERN NSString *const FBLoggingBehaviorAppEvents;

/*! Log Informational occurrences */
FBSDK_EXTERN NSString *const FBLoggingBehaviorInformational;

/*! Log cache errors. */
FBSDK_EXTERN NSString *const FBLoggingBehaviorCacheErrors;

/*! Log errors from SDK UI controls */
FBSDK_EXTERN NSString *const FBLoggingBehaviorUIControlErrors;

/*! Log errors likely to be preventable by the developer. This is in the default set of enabled logging behaviors. */
FBSDK_EXTERN NSString *const FBLoggingBehaviorDeveloperErrors;

/*!
 @typedef

 @abstract A list of beta features that can be enabled for the SDK. Beta features are for evaluation only,
 and are therefore only enabled for DEBUG builds. Beta features should not be enabled
 in release builds.
 */
typedef NS_ENUM(NSUInteger, FBBetaFeatures) {
    /*! Default value indicating no beta features */
    FBBetaFeaturesNone                  = 0,
};

/*!
 @typedef

 @abstract Indicates if this app should be restricted
 */
typedef NS_ENUM(NSUInteger, FBRestrictedTreatment) {
    /*! The default treatment indicating the app is not restricted. */
    FBRestrictedTreatmentNO = 0,

    /*! Indicates the app is restricted. */
    FBRestrictedTreatmentYES = 1
};

/*!
 @class FBSettings

 @abstract Allows configuration of SDK behavior.
*/
@interface FBSettings : NSObject

/*!
 @method

 @abstract Retrieve the current iOS SDK version.

 */
+ (NSString *)sdkVersion;

/*!
 @method

 @abstract Retrieve the current Facebook SDK logging behavior.

 */
+ (NSSet *)loggingBehavior;

/*!
 @method

 @abstract Set the current Facebook SDK logging behavior.  This should consist of strings defined as
 constants with FBLogBehavior*, and can be constructed with, e.g., [NSSet initWithObjects:].

 @param loggingBehavior A set of strings indicating what information should be logged.  If nil is provided, the logging
 behavior is reset to the default set of enabled behaviors.  Set in an empty set in order to disable all logging.
 */
+ (void)setLoggingBehavior:(NSSet *)loggingBehavior;

/*!
 @method

 @abstract
 This method is deprecated -- App Events favors using bundle identifiers to this.
 */
+ (NSString *)appVersion __attribute__ ((deprecated("App Events favors use of bundle identifiers for version identification.")));

/*!
 @method

 @abstract
 This method is deprecated -- App Events favors using bundle identifiers to this.
 @param appVersion deprecated
 */
+ (void)setAppVersion:(NSString *)appVersion __attribute__ ((deprecated("App Events favors use of bundle identifiers for version identification.")));

/*!
 @method

 @abstract Retrieve the Client Token that has been set via [FBSettings setClientToken]
 */
+ (NSString *)clientToken;

/*!
 @method

 @abstract Sets the Client Token for the Facebook App.  This is needed for certain API calls when made anonymously,
 without a user-based Session.

 @param clientToken  The Facebook App's "client token", which, for a given appid can be found in the Security
 section of the Advanced tab of the Facebook App settings found at <https://developers.facebook.com/apps/[your-app-id]>

 */
+ (void)setClientToken:(NSString *)clientToken;

/*!
 @method

 @abstract Set the default Facebook Display Name to be used by the SDK. This should match
 the Display Name that has been set for the app with the corresponding Facebook App ID, in
 the Facebook App Dashboard

 @param displayName The default Facebook Display Name to be used by the SDK.
 */
+ (void)setDefaultDisplayName:(NSString *)displayName;

/*!
 @method

 @abstract Get the default Facebook Display Name used by the SDK. If not explicitly
 set, the default will be read from the application's plist.
 */
+ (NSString *)defaultDisplayName;

/*!
 @method

 @abstract Set the default Fac