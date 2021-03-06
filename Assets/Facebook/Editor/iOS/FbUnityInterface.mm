   additional arguments are ignored.
     * @return the formatted string.
     * @throws NullPointerException if {@code format == null}
     * @throws java.util.IllegalFormatException
     *             if the format is invalid.
     * @since 1.5
     */
    public static String format(String format, Object... args) {
        return format(Locale.getDefault(), format, args);
    }

    /**
     * Returns a formatted string, using the supplied format and arguments,
     * localized to the given locale.
     *
     * @param locale
     *            the locale to apply; {@code null} value means no localization.
     * @param format the format string (see {@link java.util.Formatter#format})
     * @param args
     *            the list of arguments passed to the formatter. If there are
     *            more arguments than required by {@code format},
     *            additional arguments are ignored.
     * @return the formatted string.
     * @throws NullPointerException if {@code format == null}
     * @throws java.util.IllegalFormatException
     *             if the format is invalid.
     * @since 1.5
     */
    public static String format(Locale locale, String format, Object... args) {
        if (format == null) {
            throw new NullPointerException("format == null");
        }
        int bufferSize = format.length() + (args == null ? 0 : args.length * 10);
        Formatter f = new Formatter(new StringBuilder(bufferSize), locale);
        return f.format(format, args).toString();
    }

    /*
     * An implementation of a String.indexOf that is supposed to perform
     * substantially better than the default algorithm if the "needle" (the
     * subString being searched for) is a constant string.
     *
     * For example, a JIT, upon encountering a call to String.indexOf(String),
     * where the needle is a constant string, may compute the values cache, md2
     * and lastChar, and change the call to the following method.
     */
    @FindBugsSuppressWarnings("UPM_UNCALLED_PRIVATE_METHOD")
    @SuppressWarnings("unused")
    private static int indexOf(String haystackString, String needleString,
            int cache, int md2, char lastChar) {
        char[] haystack = haystackString.value;
        int haystackOffset = haystackString.offset;
        int haystackLength = haystackString.count;
        char[] needle = needleString.value;
        int needleOffset = needleString.offset;
        int needleLength = needleString.count;
        int needleLengthMinus1 = needleLength - 1;
        int haystackEnd = haystackOffset + haystackLength;
        outer_loop: for (int i = haystackOffset + needleLengthMinus1; i < haystackEnd;) {
            if (lastChar == haystack[i]) {
                for (int j = 0; j < needleLengthMinus1; ++j) {
                    if (needle[j + needleOffset] != haystack[i + j
                            - needleLengthMinus1]) {
                        int skip = 1;
                        if ((cache & (1 << haystack[i])) == 0) {
                            skip += j;
                        }
                        i += Math.max(md2, skip);
                        continue outer_loop;
                    }
                }
                return i - needleLengthMinus1 - haystackOffset;
            }

            if ((cache & (1 << haystack[i])) == 0) {
                i += needleLengthMinus1;
            }
            i++;
        }
        return -1;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              //
//  FbUnityInterface.mm
//  Unity-iPhone
//
//  Created by Benjamin Padget on 6/4/13.
//
//

#include "FbUnityInterface.h"
#import <FacebookSDK/FacebookSDK.h>
#import <Foundation/NSJSONSerialization.h>
#include <string>




static FbUnityInterface *_instance = [FbUnityInterface sharedInstance];
const char *g_fbObjName = "UnityFacebookSDKPlugin";

extern "C" void iosGetDeepLink();

@implementation FbUnityInterface

+(FbUnityInterface *)sharedInstance {
  return _instance;
}

+ (void)initialize {
  if(!_instance) {
    _instance = [[FbUnityInterface alloc] init];
  }
}

- (id)init {
  if(_instance != nil) {
    return _instance;
  }

  self = [super init];
  if(!self)
    return nil;

  _instance = self;

  self.isInitializing = YES;
  self.dialogMode = NativeDialogModes::FAST_APP_SWITCH_SHARE_DIALOG;

  [[NSNotificationCenter defaultCenter]
   addObserver:self
   selector:@selector(didBecomeActive:)
   name:UIApplicationDidBecomeActiveNotification
   object:nil];

  [[NSNotificationCenter defaultCenter]
   addObserver:self
   selector:@selector(willTerminate:)
   name:UIApplicationWillTerminateNotification
   object:nil];

  [[NSNotificationCenter defaultCenter]
   addObserver:self
   selector:@selector(didFinishLaunching:)
   name:UIApplicationDidFinishLaunchingNotification
   object:nil];

#if UNITY_VERSION >= 430
  UnityRegisterAppDelegateListener(self);
#endif
  return self;
}

- (id)initWithAppId:(const char *)_appId
             cookie:(bool)_cookie
            logging:(bool)_logging
              status:(bool)_status
frictionlessRequests:(bool)_frictionlessRequests
           urlSuffix:(const char *)_urlSuffix {
  self = [self init];

  self.useFrictionlessRequests = _frictionlessRequests;

  if(_appId) {
    [FBSettings setDefaultAppID:[NSString stringWithUTF8String:_appId]];
  }

  if(_urlSuffix && strlen(_urlSuffix) > 0) {
    [FBSettings setDefaultUrlSchemeSuffix:[NSString stringWithUTF8String:_urlSuffix]];
  }

  //since this class is a singleton, I don't know how we would ever have an open session here, but handle anyway
  if (self.session.isOpen) {
    [self handleSessionChange:self.session state:self.session.state error:nil];
    return self;
  }

  // create a fresh session object
  _session = [[FBSession alloc] init];

  // if we don't have a cached token, a call to open here would cause UX for login to
  // occur; we don't want that to happen unless the user clicks the login button, and so
  // we check here to make sure we have a token before calling open
  if (self.session.state == FBSessionStateCreatedTokenLoaded) {
    // even though we had a cached token, we need to login to make the session usable
    [self.session openWithCompletionHandler:^(FBSession *session,
                                              FBSessionState state,
                                              NSError *error) {
      [self handleSessionChange:session state:state error:error];
    }];
  } else {
    self.isInitializing = NO;
    UnitySendMessage(g_fbObjName, "OnInitComplete", "");
  }
  return self;
}

+ (void)sendMessageToUnity:(const char *)unityMessage userData:(NSDictionary *)userData
{
  NSError *serializationError = nil;
  NSData *jsonData = nil;
  if(userData != nil) {
    jsonData = [NSJSONSerialization dataWithJSONObject:userData options:0 error:&serializationError];
  }

  const char *userDataString = nil;
  NSString *jsonString = nil;
  if (jsonData) {
    jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    userDataString = [jsonString cStringUsingEncoding:NSUTF8StringEncoding];
  }

  UnitySendMessage(g_fbObjName, unityMessage, userDataString==nil?"":userDataString);

}


- (void)handleSessionChange:(FBSession *)session
                      state:(FBSessionState) state
                      error:(NSError *)error
{
  NSMutableDictionary *msgData = [NSMutableDictionary dictionary];
  switch (state) {
    case FBSessionStateOpen: {
      [FBSession setActiveSession:session];

      if(self.useFrictionlessRequests) {
        self.friendCache = [[FBFrictionlessRecipientCache alloc] init];
        [self.friendCache prefetchAndCacheForSession:nil];
      } else {
        self.friendCache = nil;
      }


      //lets fire off another request while in the completion handler for the previous request
      //what can possibly go wrong?
      [FBRequestConnection startForMeWithCompletionHandler:
       ^(FBRequestConnection *connection, id result, NSError *error) {

         id<FBGraphUser> user = result;
         if(user && session != nil && session.accessTokenData != nil && session.accessTokenData.accessToken != nil) {
           [msgData setObject:[user objectForKey:@"id"] forKey:@"user_id"];
           [msgData setObject:session.accessTokenData.accessToken forKey:@"access_token"];
           [msgData setObject:[NSString stringWithFormat:@"%ld", (long)session.accessTokenData.expirationDate.timeIntervalSince1970] forKey:@"expiration_timestamp"];
         }

         const char *msgType = nil;
         if(self.isInitializing) {
           msgType = "OnInitComplete";
           self.isInitializing = NO;
         } else {
           msgType = "OnLogin";
         }
         [FbUnityInterface sendMessageToUnity:msgType userData:msgData];
      }];
    }
      break;

    case FBSessionStateOpenTokenExtended: {
      // this case is for both refreshing access token and requesting new permissions
      [msgData setObject:session.accessTokenData.accessToken forKey:@"access_token"];
      [msgData setObject:[NSString stringWithFormat:@"%ld", (long)session.accessTokenData.expirationDate.timeIntervalSince1970] forKey:@"expiration_timestamp"];
      [FbUnityInterface sendMessageToUnity:"OnAccessTokenRefresh" userData:msgData];
    }
      break;

    case FBSessionStateClosedLoginFailed:
      [FBSession.activeSession closeAndClearTokenInformation];
      UnitySendMessage(g_fbObjName, "OnLogin", "");
      break;
    default:
      break;
  }
}

-(void)login:(const char *)scope {
    NSString *scopeStr = [NSString stringWithUTF8String:scope];
    NSArray *permissions = nil;
    if(scope && strlen(scope) > 0) {
      permissions = [scopeStr componentsSeparatedByString:@","];
    }

    if (self.session == nil || ![self.session isOpen]) {
      self.session = [[FBSession alloc] initWithAppID:nil
                                        permissions:permissions
                                        defaultAudience:FBSessionDefaultAudienceFriends
                                        urlSchemeSuffix:nil
                                        tokenCacheStrategy:nil];
      [self.session openWithBehavior:FBSessionLoginBehaviorWithFallbackToWebView
                    completionHandler:^(FBSession *session,
                                        FBSessionState state,
                                        NSError *error) {
                        [self handleSessionChange:session state:state error:error];
                     }];
    } else {
      // this works correctly for publish permissions too
      [self.session requestNewReadPermissions:permissions completionHandler:^(FBSession *session, NSError *error) {
          [self handleSessionChange:session state:session.state error:error];
        }];
    }
}

-(void)logout {
  [self.session closeAndClearTokenInformation];
  UnitySendMessage(g_fbObjName, "OnLogout", "");
}

-(void)didBecomeActive: (NSNotification *)notification {
  [FBAppCall handleDidBecomeActiveWithSession:self.session];
}

-(void)willTerminate:(NSNotification *)notification {
  [self.session close];
}

-(void)didFinishLaunching:(NSNotification *)notification {
  NSDictionary *info = notification.userInfo;

  if(&UIApplicationLaunchOptionsURLKey && info && [info objectForKey:UIApplicationLaunchOptionsURLKey]) {
    [FbUnityInterface sharedInstance].launchURL = [[info objectForKey:UIApplicationLaunchOptionsURLKey] absoluteString];
  }
}


- (BOOL)openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication {
  __block bool hasDeepLink = false;
  bool fbhandled = [FBAppCall handleOpenURL:url
                        sourceApplication:sourceApplication
                        withSession:self.session
                        fallbackHandler:^(FBAppCall *call) {
    // Handler is called when FB SDK does consume a url, but only partially
    hasDeepLink = true;
  }];

  [FBSession setActiveSession:self.session];
  if (hasDeepLink || !fbhandled) {
    self.launchURL = [url absoluteString];
    iosGetDeepLink();
  }

  return fbhandled;
}


#if UNITY_VERSION >= 430
- (void)onOpenURL:(NSNotification*)notification {
  [self openURL:[notification.userInfo objectForKey:@"url"] sourceApplication:[notification.userInfo objectForKey:@"sourceApplication"]];
}
#endif

@end





//helper function for stuffing c strings into NSDictionarys
void addCStrToNsDict(NSMutableDictionary *dict, const char *key, const char *val) {
  if(dict && key && val && val[0] != 0 && key[0] != 0) {
    [dict setObject:[NSString stringWithUTF8String:val] forKey:[NSString stringWithUTF8String:key]];
  }
}

void HandleJSONResponse(int requestId, bool isError, const char *payload) {

  std::string temp = "";

  char idStr[16];
  sprintf(idStr, "%d", requestId);

  temp += idStr;
  temp += ":";
  if(payload) {
    temp += payload;
  }

  UnitySendMessage(g_fbObjName, "OnRequestComplete", temp.c_str());
}

void HandleDictionaryResponse(int requestId, bool isError, NSDictionary *srcDict) {

  NSMutableDictionary *dict = [srcDict mutableCopy];


  [srcDict enumerateKeysAndObjectsUsingBlock:
   ^(NSString *key, NSString *val, BOOL *stop) {
     //strip this out of response, we signal cancel and completion differently
     if([key isEqualToString:@"didComplete"]) {
       [dict removeObjectForKey:key];
     }

     if([key isEqualToString:@"completionGesture"]) {
       //if we cancelled this, clear out the dict and add our own cancel flag
       if([val isEqualToString:@"cancel"]) {
         [dict removeAllObjects];
         [dict setObject:[NSNumber numberWithBool:YES] forKey:@"cancelled"];
         *stop = true;
       } else { //otherwise c# land doesn't care about this key
         [dict removeObjectForKey:key];
         //add "posted", so the response not cancelled later in case we don't have a post ID
         [dict setObject:[NSNumber numberWithBool:YES] forKey:@"posted"];
       }
     }


     //canvas and android use "id" instead of "postId" here
     if([key isEqualToString:@"postId"]) {
       [dict removeObjectForKey:key];
       [dict setObject:val forKey:@"id"];
     }
  }];

  //if the dictionary is empty at this point we have a cancelled action
  if([dict count] == 0) {
    //yes, this is really the way to add a bool to a nsdictionary
    [dict setObject:[NSNumber numberWithBool:YES] forKey:@"cancelled"];
  }


  NSError *serError = nil;
  NSData *jsonData = nil;
  if(dict) {
    jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:&serError];
  }

  NSString *jsonString = nil;
  if (jsonData) {
    jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
  }

  HandleJSONResponse(requestId, isError, [jsonString cStringUsingEncoding:NSUTF8StringEncoding]);
}

void HandleURLResponse(FBWebDialogResult result, int requestId, bool isError, NSURL *url) {

  NSString *decodedUrl = [[url absoluteString] stringByReplacingPercentEscapesUsingEncoding:NSUTF8StringEncoding];
  NSArray *requestAndParams = [decodedUrl componentsSeparatedByString:@"?"];
  NSArray *params = nil;
  if(requestAndParams.count > 1)
    params = [requestAndParams[1] componentsSeparatedByString:@"&"];

  NSMutableDictionary *dict = [[NSMutableDictionary alloc] init];
  NSMutableArray *toArray = nil;

  if(params != nil && result != FBWebDialogResultDialogNotCompleted) {
    for(NSString *str in params) {
      NSArray *keyAndVal = [str componentsSeparatedByString:@"="];
      NSString *key = nil, *val = nil;
      if(keyAndVal.count > 0)
        key = keyAndVal[0];
      if(keyAndVal.count > 1)
        val = keyAndVal[1];

      //requests with multiple recipients come in as to[0] = , to[1] = etc and need to be joined into an array
      if([key hasPrefix:@"to["]) {
        if(toArray == nil) {
          toArray = [[NSMutableArray alloc] init];
          [dict setObject:toArray forKey:@"to"];
        }

        [toArray addObject:val];

      } else if(key && val) {
        [dict setObject:val forKey:key];
      }
    }
  }

  HandleDictionaryResponse(requestId, isError, dict);
}

NSDictionary *UnpackDict(int numVals, const char **keys, const char **vals)
{
  NSMutableDictionary *params = nil;
  if(numVals > 0 && keys && vals) {
    params = [NSMutableDictionary dictionaryWithCapacity:numVals];
    for(int i=0; i<numVals; i++) {
      [params setObject:[NSString stringWithUTF8String:vals[i]] forKey:[NSString stringWithUTF8String:keys[i]]];
    }
  }

  return params;
}

//everything in the extern "C" section is callable from C# unity
extern "C" {

void iosInit(const char *_appId, bool _cookie, bool _logging, bool _status, bool _frictionlessRequests, const char *_urlSuffix) {
  [[FbUnityInterface alloc] initWithAppId:_appId cookie:_cookie logging:_logging status:_status frictionlessRequests:_frictionlessRequests urlSuffix:_urlSuffix];
}

void iosLogin(const char *scope) {
  [[FbUnityInterface sharedInstance] login:scope];
}

void iosLogout() {
  [[FbUnityInterface sharedInstance] logout];
}

void iosSetShareDialogMode(NativeDialogModes::eModes mode) {
  [[FbUnityInterface sharedInstance] setDialogMode:mode];
}

void iosCreateGameGroup(int requestId,
                        const char *name,
                        const char *description,
                        const char *privacy) {
  NSMutableDictionary *params = [NSMutableDictionary dictionary];
  addCStrToNsDict(params, "name", name);
  addCStrToNsDict(params, "description", description);
  addCStrToNsDict(params, "privacy", privacy);

  [FBWebDialogs presentDialogModallyWithSession:FBSession.activeSession
                                         dialog:@"game_group_create"
                                     parameters:params
                                        handler:^(FBWebDialogResult result,
                                                  NSURL *resultURL,
                                                  NSError *error) {
                                          HandleURLResponse(result, requestId, error != nil, resultURL);
                                        }];
}

void iosJoinGameGroup(int requestId,
                        const char *groupId) {
  NSMutableDictionary *params = [NSMutableDictionary dictionary];
  addCStrToNsDict(params, "id", groupId);

  [FBWebDialogs presentDialogModallyWithSession:FBSession.activeSession
                                         dialog:@"game_group_join"
                                     parameters:params
                                        handler:^(FBWebDialogResult result,
                                                  NSURL *resultURL,
                                                  NSError *error) {
                                          HandleURLResponse(result, requestId, error != nil, resultURL);
                                        }];
}

void iosAppRequest(int requestId,
                   const char *message,
                   const char *actionType,
                   const char *objectId,
                   const char **to,
                   int toLength,
                   const char *filters,
                   const char **excludeIds,
                   int excludeIdsLength,
                   bool hasMaxRecipients, //not supported on mobile
                   int maxRecipients, //not supported on mobile
                   const char *data,
                   const char *title) {
  NSMutableDictionary *params = [NSMutableDictionary dictionary];
  addCStrToNsDict(params, "message", message);
  if (actionType != nil && objectId != nil) {
    addCStrToNsDict(params, "action_type", actionType);
    addCStrToNsDict(params, "object_id", objectId);
  }
  addCStrToNsDict(params, "filters", filters);
  addCStrToNsDict(params, "data", data);
  addCStrToNsDict(params, "title", title);

  if(to && toLength) {
    NSMutableArray *tempArray = [NSMutableArray array];
    for(int i=0; i<toLength; i++) {
      [tempArray addObject:[NSString stringWithUTF8String:to[i]]];
    }
    NSString *tempString = [tempArray componentsJoinedByString:@","];
    [params setObject:tempString forKey:@"to"];
  }

  FBFrictionlessRecipientCache *fc = [[FbUnityInterface sharedInstance] friendCache];

  [FBWebDialogs
   presentRequestsDialogModallyWithSession:nil
   message:[NSString stringWithUTF8String:message]
   title:[NSString stringWithUTF8String:title]
   parameters:params handler:
   ^(FBWebDialogResult result, NSURL *resultURL, NSError *error) {
     //requests send a url response instead of nice sensible json
     HandleURLResponse(result, requestId, error != nil, resultURL);
   }
   friendCache:fc];
}

void iosGetDeepLink() {
  NSString *url = [FbUnityInterface sharedInstance].launchURL;

  if(url == nil)
    url = @"";

  NSDictionary *dict = [NSDictionary dictionaryWithObject:url forKey:@"deep_link"];

  NSError *serError = nil;
  NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:&serError];
  NSString *jsonString = nil;
  if (jsonData) {
    jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
  }

  UnitySendMessage(g_fbObjName, "OnGetDeepLinkComplete", [jsonString cStringUsingEncoding:NSUTF8StringEncoding]);
}

void iosFeedRequest(int requestId,
                    const char *toId,
                    const char *link,
                    const char *linkName,
                    const char *linkCaption,
                    const char *linkDescription,
                    const char *picture,
                    const char *mediaSource,
                    const char *actionName,
                    const char *actionLink,
                    const char *reference) {

  NSMutableDictionary *params = [NSMutableDictionary dictionary];

  addCStrToNsDict(params, "to", toId);
  addCStrToNsDict(params, "link", link);
  addCStrToNsDict(params, "name", linkName);
  addCStrToNsDict(params, "caption", linkCaption);
  addCStrToNsDict(params, "description", linkDescription);
  addCStrToNsDict(params, "picture", picture);
  addCStrToNsDict(params, "source", mediaSource);
  addCStrToNsDict(params, "ref", reference);

  //json should look like this:
  //[{'name': '$actionName', 'link': '$actionLink'}]
  if(actionName && actionLink && actionName[0] != 0 && actionLink[0] != 0) {
    NSDictionary *tempDict =
    [NSDictionary dictionaryWithObjectsAndKeys:
     [NSString stringWithUTF8String:actionName],
     @"name",
     [NSString stringWithUTF8String:actionLink],
     @"link",
     nil];
    NSArray *tempArray = [NSArray arrayWithObject:tempDict];

    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:tempArray
                                                       options:0
                                                         error:&error];
    if (jsonData) {
      NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
      [params setObject:jsonString forKey:@"actions"];
    }
  }

  bool shouldDisplayNative = [FbUnityInterface sharedInstance].dialogMode == NativeDialogModes::FAST_APP_SWITCH_SHARE_DIALOG;
  // Native dialogs do not yet support To: fields, so fall back if we have one.
  shouldDisplayNative = shouldDisplayNative && !(toId && toId[0] != 0);
  if(shouldDisplayNative) {
    FBLinkShareParams *dialogParams = [[[FBLinkShareParams alloc] init] autorelease];

    NSString *strLink = [NSString stringWithUTF8String:link];
    NSURL *linkUrl = [NSURL URLWithString:strLink];
    if(linkUrl.scheme == nil)
    {
      NSString *prefixed = [NSString stringWithFormat:@"http://%@", strLink];
      linkUrl = [NSURL URLWithString:prefixed];
    }
    dialogParams.link = linkUrl;
    dialogParams.name = [NSString stringWithUTF8String:linkName];
    dialogParams.caption = [NSString stringWithUTF8String:linkCaption];
    dialogParams.linkDescription = [NSString stringWithUTF8String:linkDescription];
    dialogParams.picture = [NSURL URLWithString:[NSString stringWithUTF8String:picture]];

    bool canPresentNative = [FBDialogs canPresentShareDialogWithParams:dialogParams];
    if( canPresentNative ) {
      [FBDialogs presentShareDialogWithParams:dialogParams
                                  clientState:nil
                                      handler:
       ^(FBAppCall *call, NSDictionary *results, NSError *error) {
         HandleDictionaryResponse(requestId, error != nil, results);
       }];
      return;
    }
  }

  // Invoke the dialog
  [FBWebDialogs presentFeedDialogModallyWithSession:nil
                                         parameters:params
                                            handler:
   ^(FBWebDialogResult result, NSURL *resultURL, NSError *error) {
     HandleURLResponse(result, requestId, error != nil, resultURL);
   }];

}

NSString *ResponseHelper(id result, NSError *error) {
  NSError *serError = nil;
  if(result && [result isKindOfClass:[NSDictionary class]]) {
    NSDictionary *dict = (NSDictionary *)result;
    id nonJsonResponse = [dict objectForKey:FBNonJSONResponseProperty];

    NSData *jsonData;
    if(nonJsonResponse && [nonJsonResponse isKindOfClass:[NSString class]]) {
      return nonJsonResponse;
    }

    jsonDat