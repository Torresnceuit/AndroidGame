INDX( 	 `=X�            (   h  �       �    A               M�    � �     L�    l��p���猨v���nL�^�l��p���                        4 7 2 6 9 8 5 5 2 c 0 c d 4 e a 1 8 9 a 5 b 3 0 3 c 9 6 6 4 1 5       N�    � �     L�    �p���=P�v����L�^��p����      �              %4 7 2 6 9 8 5 5 2 c 0 c d 4 e a 1 8 9 a 5 b 3 0 3 c 9 6 6 4 1 5 . i n f o     M�    h R     L�    l��p���猨v���nL�^�l��p���                       4 7 2 6 9 8 ~ 1       N�    p Z     L�    �p�� =P�v����L�^��p����      �              4 7 2 6 9 8 ~ 1 . I N F       O�    � �     L�    �p����/q����L�^��p��� `      }R               4 7 4 5 c 9 9 0 b 0 1 d 6 4 0 0 6 b a 6 8 1 6 a b 1 f e c 7 0 2       P�    � �     L�    0�p����`0q����L�^�0�p����      �              %4 7 4 5 c 9 9 0 b 0 1 d 6 4 0 0 6 b a 6 8 1 6 a b 1 f e c 7 0 2 . i n f o     O�    h R     L�    �p����/q����L�^��p��� `      }R              4 7 4 5 C 9 ~ 1       P�    p Z    L�    0�p����`0q����L�^�0�p����      �              4 7 4 5 C 9 ~ 1 . I N F       S�    � �     L�    ��p�������x��L�^���p��� �      w               4 7 a 0 c 7 f 6 0 5 0 c e 4 1 6 e 8 8 f 1 a 9 c a 1 1 e b e 3 7       T�    � �     L�    G��p���O�$��x��L�^�G��p���        �              %4 7 a 0 c 7 f 6 0 5 0 c e 4 1 6 e 8 8 f 1 a 9 c a 1 1 e b e 3 7 . i n f o     S�    h R     L�    ��p�������x��L�^���p��� �      w              4 7 A 0 C 7 ~ 1      T�    p Z     L�    G��p���O�$��x��L�^�G��p���        �              4 7 A 0 C 7 ~ 1 . I N F                             4 7 A 0 C 7 ~ 1       T�    p Z     L�    G��p���O�$��x����"l��G��p���        �              4 7 A 0 C 7 ~ 1 . I N F                     L�    G��p���O�$��x����"l��G��p���        �              4 7 A 0 C 7 ~ 1 . I N F                     4 7 A 0 C 7 ~ 1       T�    p Z     L�    G��p���O�$��x����"l��G��p���        �              4 7  0 C 7 ~ 1 . I N F                     G��p���O�$��x����"l��G��p���        �              4 7 A 0 C 7 ~ 1 . I N F                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Facebook
{
    class IOSFacebook : AbstractFacebook, IFacebook
    {
        private const string CancelledResponse = "{\"cancelled\":true}";
#if UNITY_IOS
        [DllImport ("__Internal")] private static extern void iosInit(string appId, bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix);
        [DllImport ("__Internal")] private static extern void iosLogin(string scope);
        [DllImport ("__Internal")] private static extern void iosLogout();

        [DllImport ("__Internal")] private static extern void iosSetShareDialogMode(int mode);

        [DllImport ("__Internal")]
        private static extern void iosFeedRequest(
            int requestId,
            string toId,
            string link,
            string linkName,
            string linkCaption,
            string linkDescription,
            string picture,
            string mediaSource,
            string actionName,
            string actionLink,
            string reference);

        [DllImport ("__Internal")]
        private static extern void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "");

        [DllImport ("__Internal")]
        private static extern void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy);

        [DllImport ("__Internal")]
        private static extern void iosJoinGameGroup(int requestId, string id);

        [DllImport ("__Internal")]
        private static extern void iosFBSettingsPublishInstall(int requestId, string appId);

        [DllImport ("__Internal")]
        private static extern void iosFBSettingsActivateApp(string appId);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals);

        [DllImport ("__Internal")]
        private static extern void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage);

        [DllImport ("__Internal")]
        private static extern void iosGetDeepLink();
#else
        void iosInit(string appId, bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix) { }
        void iosLogin(string scope) { }
        void iosLogout() { }

        void iosSetShareDialogMode(int mode) { }

        void iosFeedRequest(
            int requestId,
            string toId,
            string link,
            string linkName,
            string linkCaption,
            string linkDescription,
            string picture,
            string mediaSource,
            string actionName,
            string actionLink,
            string reference) { }

        void iosAppRequest(
            int requestId,
            string message,
            string actionType,
            string objectId,
            string[] to = null,
            int toLength = 0,
            string filters = "",
            string[] excludeIds = null,
            int excludeIdsLength = 0,
            bool hasMaxRecipients = false,
            int maxRecipients = 0,
            string data = "",
            string title = "") { }

        void iosCreateGameGroup(
            int requestId,
            string name,
            string description,
            string privacy) { }

        void iosJoinGameGroup(int requestId, string id) {}

        void iosFBSettingsPublishInstall(int requestId, string appId) { }

        void iosFBSettingsActivateApp(string appId) { }

        void iosFBAppEventsLogEvent(
            string logEvent,
            double valueToSum,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsLogPurchase(
            double logPurchase,
            string currency,
            int numParams,
            string[] paramKeys,
            string[] paramVals) { }

        void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage) { }

        void iosGetDeepLink() { }
#endif

        private class NativeDict
        {
            public NativeDict()
            {
                numEntries = 0;
                keys = null;
                vals = null;
            }

            public int numEntries;
            public string[] keys;
            public string[] vals;
        };

        public enum FBInsightsFlushBehavior
        {
            FBInsightsFlushBehaviorAuto,
            FBInsightsFlushBehaviorExplicitOnly,
        };

        private int dialogMode = (int)NativeDialogModes.eModes.FAST_APP_SWITCH_SHARE_DIALOG;

        private InitDelegate externalInitDelegate;

        public override int DialogMode
        {
            get { return dialogMode; }
            set { dialogMode = value; iosSetShareDialogMode(dialogMode); }
        }

        public override bool LimitEventUsage
        {
            get
            {
                return limitEventUsage;
            }
            set
            {
                limitEventUsage = value;
                iosFBAppEventsSetLimitEventUsage(value);
            }
        }

        private FacebookDelegate deepLinkDelegate;

        #region Init
        protected override void OnAwake()
        {
            accessToken = "NOT_USED_ON_IOS_FACEBOOK";
        }

        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            iosInit(appId, cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
            externalInitDelegate = onInitComplete;
        }
        #endregion

        #region FB public interface
        public override void Login(string scope = "", FacebookDelegate callback = null)
        {
            AddAuthDelegate(callback);
            iosLogin(scope);
        }

        public override void Logout()
        {
            iosLogout();
            isLoggedIn = false;
        }

        public override void AppRequest(
            string message,
            OGActionType actionType,
            string objectId,
            string[] to = null,
            List<object> filters = null,
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message", "message cannot be null or empty!");
            }

            if (actionType != null && string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("objectId", "You cannot provide an actionType without an objectId");
            }

            if (actionType == null && !string.IsNullOrEmpty(objectId))
            {
                throw new ArgumentNullException("actionType", "You cannot provide an objectId without an actionType");
            }

            string mobileFilter = null;
            if(filters != null && filters.Count > 0) {
                mobileFilter = filters[0] as string;
            }

            iosAppRequest(
                Convert.ToInt32(AddFacebookDelegate(callback)),
                message,
                (actionType != null) ? actionType.ToString() : null,
                objectId,
                to,
                to != null ? to.Length : 0,
                mobileFilter != null ? mobileFilter : "",
                excludeIds,
                excludeIds != null ? excludeIds.Length : 0,
                maxRecipients.HasValue,
                maxRecipients.HasValue ? maxRecipients.Value : 0,
                data,
                title);
        }

        public override void FeedRequest(
            string toId = "",
            string link = "",
            string linkName = "",
            string linkCaption = "",
            string linkDescription = "",
            string picture = "",
            string mediaSource = "",
            string actionName = "",
            string actionLink = "",
            string reference = "",
            Dictionary<string, string[]> properties = null,
            FacebookDelegate callback = null)
        {
            iosFeedRequest(System.Convert.ToInt32(AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
        }

        public override void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
        }

        public override void GameGroupCreate(
            string name,
            string description,
            string privacy = "CLOSED",
            FacebookDelegate callback = null)
        {
            iosCreateGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), name, description, privacy);
        }

        public override void GameGroupJoin(
            string id,
            FacebookDelegate callback = null)
        {
            iosJoinGameGroup(System.Convert.ToInt32(AddFacebookDelegate(callback)), id);
        }

        public override void GetDeepLink(FacebookDelegate callback)
        {
            if (callback == null)
            {
                return;
            }
            deepLinkDelegate = callback;
            iosGetDeepLink();
        }

        public void OnGetDeepLinkComplete(string message)
        {
            var rawResult = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            if (deepLinkDelegate == null)
            {
                return;
            }
            object deepLink = "";
            rawResult.TryGetValue("deep_link", out deepLink);
            deepLinkDelegate(new FBResult(deepLink.ToString()));
        }

        public override void AppEventsLogEvent(
            string logEvent,
            float? valueToSum = null,
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (valueToSum.HasValue)
            {
                iosFBAppEventsLogEvent(logEvent, valueToSum.Value, dict.numEntries, dict.keys, dict.vals);
            }
            else
            {
                iosFBAppEventsLogEvent(logEvent, 0.0, dict.numEntries, dict.keys, dict.vals);
            }
        }

        public override void AppEventsLogPurchase(
            float logPurchase,
            string currency = "USD",
            Dictionary<string, object> parameters = null)
        {
            NativeDict dict = MarshallDict(parameters);
            if (string.IsNullOrEmpty(currency))
            {
                currency = "USD";
            }
            iosFBAppEventsLogPurchase(logPurchase, currency, dict.numEntries, dict.keys, dict.vals);
        }

        public override void PublishInstall(string appId, FacebookDelegate callback = null)
        {
            iosFBSettingsPublishInstall(System.Convert.ToInt32(AddFacebookDelegate(callback)), appId);
        }

        public override void ActivateApp(string appId = null)
        {
            iosFBSettingsActivateApp(appId);
        }
        #endregion

        #region Interal stuff
        pr