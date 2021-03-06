еобходимые для работы некоторых приложений, не поддерживаются на вашем устройстве. Обратитесь к производителю."</string>
    <string name="common_google_play_services_unsupported_date_text" msgid="4725396522367789365">"Проверьте правильность даты, указанной на устройстве."</string>
    <string name="common_google_play_services_update_button" msgid="8932944190611227642">"Обновить"</string>
    <string name="common_signin_button_text" msgid="9071884888741449141">"Войти"</string>
    <string name="common_signin_button_text_long" msgid="2429381841831957106">"Войти в аккаунт Google"</string>
</resources>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          /*
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

#import <UIKit/UIKit.h>

@class FBFrictionlessRecipientCache;
@class FBSession;
@protocol FBWebDialogsDelegate;

/*!
 @typedef NS_ENUM (NSUInteger, FBWebDialogResult)

 @abstract
 Passed to a handler to indicate the result of a dialog being displayed to the user.

 @discussion Note `FBWebDialogResultDialogCompleted` is also used for cancelled operations.
*/
typedef NS_ENUM(NSUInteger, FBWebDialogResult) {
    /*! Indicates that the dialog action completed successfully. Note, that cancel operations represent completed dialog operations.
     The url argument may be used to distinguish between success and user-cancelled cases */
    FBWebDialogResultDialogCompleted = 0,
    /*! Indicates that the dialog operation was not completed. This occurs in cases such as the closure of the web-view using the X in the upper left corner. */
    FBWebDialogResultDialogNotCompleted
};

/*!
 @typedef

 @abstract Defines a handler that will be called in response to the web dialog
 being dismissed
 */
typedef void (^FBWebDialogHandler)(
    FBWebDialogResult result,
    NSURL *resultURL,
    NSError *error);

/*!
 @class FBWebDialogs

 @abstract
 Provides methods to display web based dialogs to the user.
*/
@interface FBWebDialogs : NSObject

/*!
 @abstract
 Presents a Facebook web dialog (https://developers.facebook.com/docs/reference/dialogs/ )
 such as feed or apprequest.

 @param session Represents the session to use for the dialog. May be nil, which uses
 the active session if present, or returns NO, if not.

 @param dialog Represents the dialog or method name, such as @"feed"

 @param parameters A dictionary of parameters to be passed to the dialog

 @param handler An optional handler that will be called when the dialog is dismissed. Note,
 that if the method returns NO, the handler is not called. May be nil.
 */
+ (void)presentDialogModallyWithSession:(FBSession *)session
                                 dialog:(NSString *)dialog
                             parameters:(NSDictionary *)parameters
                                handler:(FBWebDialogHandler)handler;

/*!
 @abstract
 Presents a Facebook web dialog (https://developers.facebook.com/docs/reference/dialogs/ )
 such as feed or apprequest.

 @param session Represents the session to use for the dialog. May be nil, which uses
 the active session if present, or returns NO, if not.

 @param dialog Represents the dialog or method name, such as @"feed"

 @param parameters A dictionary of parameters to be passed to the dialog

 @param handler An optional handler that will be called when the dialog is dismissed. Note,
 that if the method returns NO, the handler is not called. May be nil.

 @param delegate An optional delegate to allow for advanced processing of web based
 dialogs. See 'FBWebDialogsDelegate' for more details.
 */
+ (void)presentDialogModallyWithSession:(FBSession *)session
                                 dialog:(NSString *)dialog
                             parameters:(NSDictionary *)parameters
                                handler:(FBWebDialogHandler)handler
                               delegate:(id<FBWebDialogsDelegate>)delegate;

/*!
 @abstract
 Presents a Facebook apprequest dialog.

 @param session Represents the session to use for the dialog. May be nil, which uses
 the active session if present.

 @param message The required message for the dialog.

 @param title An optional title for the dialog.

 @param parameters A dictionary of additional parameters to be passed to the dialog. May be nil

 @param handler An optional handler that will be called when the dialog is dismissed. May be nil.
 */
+ (void)presentRequestsDialogModallyWithSession:(FBSession *)session
                                        message:(NSString *)message
                                          title:(NSString *)title
                                     parameters:(NSDictionary *)parameters
                                        handler:(FBWebDialogHandler)handler;

/*!
 @abstract
 Presents a Facebook apprequest dialog.

 @param session Represents the session to use for the dialog. May be nil, which uses
 the active session if present.

 @param message The required message for the dialog.

 @param title An optional title for the dialog.

 @param parameters A dictionary of additional parameters to be passed to the dialog. May be nil

 @param handler An optional handler that will be called when the dialog is dism