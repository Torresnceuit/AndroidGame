INDX( 	 ��<�           (   �  �       i o _ b n            �4    � �     w4    '�A#��4�Re���b�^�'�A#���       �               &c o m _ f a c e b o o k _ p i c k e r _ m a g n i f i e r . p n g . m e t a ��4    � �     w4    M�A#���"�M�v��b�^�M�A#�� @      8=              +c o m _ f a c e b o o k _ t o o l t i p _ b l a c k _ b a c k g r o u n d . 9 . p n g �4    � �     w4    ]B#��.�b��fb�^�]B#���       �               0c o m _ f a c e b o o k _ t o o l t  p _ b l a c k _ b a c k g r o u n d . 9 . p n g . m e t a       �4    � �     w4    w;B#���"�M�v�fb�^�w;B#�� @      3:              (c o m _ f a c e b o o k _ t o o l t i p _ b l a c k _ b o t t o m n u b . p n g ?#���4    � �     w4    ��B#����id���*b�^���B#���       �               -c o m _ f a c e b o o k _ t o o l t i p _ b l a c k _ b o t t o m n u b . p n g . m e t a     �4    � �     w4    ѰB#���"�M�v��*b�^�ѰB#�� @      �9              %c o m _ f a c e b o  k _ t o o l t i p _ b l a c k _ t o p n u b . p n g ���4    � �     w4    ��B#����g��z>b�^���B#���       �               *c o m _ f a c e b o o k _ t o o l t i p _ b l a c k _ t o p n u b . p n g . m e t a ��4    � �     w4    &C#���"�M�v�z>b�^�&C#���      �              #c o m _ f a c e b o o k _ t o o l t i p _ b l a c k _ x o u t . p n g �4    � �     w4    .MC#����c��Rb�^�.MC#���       �               (c o m _ f a c e b o o k _ t o o l t i p _ b l a c k  x o u t . p n g . m e t a ?#���4    � �     w4    BtC#���"�M�v�Rb�^�BtC#�� @      >              *c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ b a c k g r o u n d . 9 . p n g ��4    � �     w4    p�C#��yj-g���eb�^�p�C#���       �               /c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ b a c k g r o u n d . 9 . p n g . m e t a �4    � �     w4    �D#���"�M�v��eb�^��D#�� @      u:              'c o m _ f a c e b o o k _ t o o l t i p _ b l u e _  o t t o m n u b . p n g �4    � �     w4    �^D#����a��yb�^��^D#���       �               ,c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ b o t t o m n u b . p n g . m e t a       �4    � �     w4    υD#���"�M�v�yb�^�υD#�� @      :              $c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ t o p n u b . p n g ?#���4    � �     w4    �D#��xPb��yb�^��D#���       �               )c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ t o p n u b . p  g . m e t a ���4    � �     w4    �D#���"�M�v�yb�^��D#���      �              "c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ x o u t . p n g ��4    � �     w4    +"E#��P�Md����b�^�+"E#���       �               'c o m _ f a c e b o o k _ t o o l t i p _ b l u e _ x o u t . p n g . m e t a y4    p Z     w4    �?#��a�4h��8�a�^��?#���       �               C O M _ F A ~ 1 . M E T       x4    p Z     w4    �{?#���"�M�v���a�^��{?#���                   C O M _ F A ~ 1 . P N G       {4    p Z     w4    F@#����d����a�^�F@#���       �               C O M _ F A ~ 2 . M E T       z4    p Z     w4    ,�?#���"�M�v�8�a�^�,�?#���      |              C O M _ F A ~ 2 . P N G       }4    p Z     w4    v�@#���m�g����a�^�v�@#���       �               C O M _ F A ~ 3 . M E T       |4    p Z     w4    hf@#���"�M�v���a�^�hf@#��H      C              C O M _ F A ~ 3 . P N G       4    p Z     w4    �)A#���~f����a�^��)A#���       �               C O M _ F A ~ 4 . M E T       ~4    p Z     w4    ��@#���"�M�v���a�^���@#��       ^              C O M _ F A ~ 4 . P N G                                                                                                                                                                                                                                                                                                                            using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class InteractiveConsole : ConsoleBase
{
    #region FB.ActivateApp() example

    private void CallFBActivateApp()
    {
        FB.ActivateApp();
        Callback(new FBResult("Check Insights section for your app in the App Dashboard under \"Mobile App Installs\""));
    }

    #endregion

    #region FB.AppRequest() Friend Selector

    public string FriendSelectorTitle = "";
    public string FriendSelectorMessage = "Derp";
    private string[] FriendFilterTypes = new string[] { "None (default)", "app_users", "app_non_users" };
    private int FriendFilterSelection = 0;
    private List<string> FriendFilterGroupNames = new List<string>();
    private List<string> FriendFilterGroupIDs = new List<string>();
    private int NumFriendFilterGroups = 0;
    public string FriendSelectorData = "{}";
    public string FriendSelectorExcludeIds = "";
    public string FriendSelectorMax = "";

    private void CallAppRequestAsFriendSelector()
    {
        // If there's a Max Recipients specified, include it
        int? maxRecipients = null;
        if (FriendSelectorMax != "")
        {
            try
            {
                maxRecipients = Int32.Parse(FriendSelectorMax);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }

        // include the exclude ids
        string[] excludeIds = (FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(',');

        // Filter groups
        List<object> FriendSelectorFilters = new List<object>();
        if (FriendFilterSelection > 0)
        {
            FriendSelectorFilters.Add(FriendFilterTypes[FriendFilterSelection]);
        }
        if (NumFriendFilterGroups > 0)
        {
            for (int i = 0; i < NumFriendFilterGroups; i++)
            {
                FriendSelectorFilters.Add(
                    new FBAppRequestsFilterGroup(
                        FriendFilterGroupNames[i],
                        FriendFilterGroupIDs[i].Split(',').ToList()
                    )
                );
            }
        }

        FB.AppRequest(
            FriendSelectorMessage,
            null,
            (FriendSelectorFilters.Count > 0) ? FriendSelectorFilters : null,
            excludeIds,
            maxRecipients,
            FriendSelectorData,
            FriendSelectorTitle,
            Callback
        );
    }
    #endregion

    #region FB.AppRequest() Direct Request

    public string DirectRequestTitle = "";
    public string DirectRequestMessage = "Herp";
    private string DirectRequestTo = "";

    private void CallAppRequestAsDirectRequest()
    {
        if (DirectRequestTo == "")
        {
            throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
        }
        FB.AppRequest(
            DirectRequestMessage,
            DirectRequestTo.Split(','),
            null,
            null,
            null,
            "",
            DirectRequestTitle,
            Callback
        );
    }

    #endregion

    #region FB.Feed() example

    public string FeedToId = "";
    public string FeedLink = "";
    public string FeedLinkName = "";
    public string FeedLinkCaption = "";
    public string FeedLinkDescription = "";
    public string FeedPicture = "";
    public string FeedMediaSource = "";
    public string FeedActionName = "";
    public string FeedActionLink = "";
    public string FeedReference = "";
    public bool IncludeFeedProperties = false;
    private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

    private void CallFBFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        if (IncludeFeedProperties)
        {
            feedProperties = FeedProperties;
        }
        FB.Feed(
            toId: FeedToId,
            link: FeedLink,
            linkName: FeedLinkName,
            linkCaption: FeedLinkCaption,
            linkDescription: FeedLinkDescription,
            picture: FeedPicture,
            mediaSource: FeedMediaSource,
            actionName: FeedActionName,
            actionLink: FeedActionLink,
            reference: FeedReference,
            properties: feedProperties,
            callback: Callback
        );
    }

    #endregion

    #region FB.Canvas.Pay() example

    public string PayProduct = "";

    private void CallFBPay()
    {
        FB.Canvas.Pay(PayProduct);
    }

    #endregion

    #region FB.API() example

    public string ApiQuery = "";

    private void CallFBAPI()
    {
        FB.API(ApiQuery, Facebook.HttpMethod.GET, Callback);
    }

    #endregion

    #region FB.GetDeepLink() example

    private void CallFBGetDeepLink()
    {
        FB.GetDeepLink(Callback);
    }

    #endregion

    #region FB.AppEvent.LogEvent example

    public void CallAppEventLogEvent()
    {
        FB.AppEvents.LogEvent(
            Facebook.FBAppEventName.UnlockedAchievement,
            null,
            new Dictionary<string,object>() {
                { Facebook.FBAppEventParameterName.Description, "Clicked 'Log AppEvent' button" }
            }
        );
        Callback(new FBResult(
                "You may see results showing up at https://www.facebook.com/insights/" +
                FB.AppId +
                "?section=AppEvents"
            )
        );
    }

    #endregion

    #region FB.Canvas.SetResolution example

    public string Width = "800";
    public string Height = "600";
    public bool CenterHorizontal = true;
    public bool CenterVertical = false;
    public string Top = "10";
    public string Left = "10";

    public void CallCanvasSetResolution()
    {
        int width;
        if (!Int32.TryParse(Width, out width))
        {
            width = 800;
        }
        int height;
        if (!Int32.TryParse(Height, out height))
        {
            height = 600;
        }
        float top;
        if (!float.TryParse(Top, out top))
        {
            top = 0.0f;
        }
        float left;
        if (!float.TryParse(Left, out left))
        {
            left = 0.0f;
        }
        if (CenterHorizontal && CenterVertical)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.CenterHorizontal());
        }
        else if (CenterHorizontal)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.CenterHorizontal());
        }
        else if (CenterVertical)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.Left(left));
        }
        else
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.Left(left));
        }
    }

    #endregion

    #region GUI

    override protected void Awake()
    {
        base.Awake();

        FeedProperties.Add("key1", new[] { "valueString1" });
        FeedProperties.Add("key2", new[] { "valueString2", "http://www.facebook.com" });
    }

    private void FriendFilterArea() {
#if UNITY_WEBPLAYER
        GUILayout.BeginHorizontal();
#endif
        GUILayout.Label("Filters:");
        FriendFilterSelection =
            GUILayout.SelectionGrid(FriendFilterSelection,
                                    FriendFilterTypes,
                                    3,
                                    GUILayout.MinHeight(buttonHeight));
#if UNITY_WEBPLAYER
        GUILayout.EndHorizontal();

        // Filter groups are not supported on mobile so no need to display
        // these buttons if not in web player.
        if (NumFriendFilterGroups > 0)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Filter group name:", GUILayout.MaxWidth(150));
            GUILayout.Label("IDs (comma separated):");
            GUILayout.EndHorizontal();

            int deleteGroup = -1;
            for (int i = 0; i < FriendFilterGroupNames.Count(); i++)
            {
                GUILayout.BeginHorizontal();
                FriendFilterGroupNames[i] = GUILayout.TextField(FriendFilterGroupNames[i], GUILayout.MaxWidth(150));
                FriendFilterGroupIDs[i] = GUILayout.TextField(FriendFilterGroupIDs[i]);
                if (GUILayout.Button("del", GUILayout.ExpandWidth(false)))
                {
                    deleteGroup = i;
                }
                GUILayout.EndHorizontal();
            }
            if (deleteGroup >= 0)
            {
                NumFriendFilterGroups--;
                FriendFilterGroupNames.RemoveAt(deleteGroup);
                FriendFilterGroupIDs.RemoveAt(deleteGroup);
            }
            GUILayout.EndVertical();
        }

        if (Button("Add filter group..."))
        {
            FriendFilterGroupNames.Add("");
            FriendFilterGroupIDs.Add("");
            NumFriendFilterGroups++;
        }
#endif
    }

    void OnGUI()
    {
        AddCommonHeader ();

        if (Button("Publish Install"))
        {
            CallFBActivateApp();
            status = "Install Published";
        }

        GUI.enabled = FB.IsLoggedIn;
        GUILayout.Space(10);
        LabelAndTextField("Title (optional): ", ref FriendSelectorTitle);
        LabelAndTextField("Message: ", ref FriendSelectorMessage);
        LabelAndTextField("Exclude Ids (optional): ", ref FriendSelectorExcludeIds);
        FriendFilterArea();
        LabelAndTextField("Max Recipients (optional): ", ref FriendSelectorMax);
        LabelAndTextField("Data (optional): ", ref FriendSelectorData);
        if (Button("Open Friend Selector"))
        {
            try
            {
                CallAppRequestAsFriendSelector();
                status = "Friend Selector called";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }
        GUILayout.Space(10);
        LabelAndTextField("Title (optional): ", ref DirectRequestTitle);
        LabelAndTextField("Message: ", ref DirectRequestMessage);
        LabelAndTextField("To Comma Ids: ", ref DirectRequestTo);
        if (Button("Open Direct Request"))
        {
            try
            {
                CallAppRequestAsDirectRequest();
                status = "Direct Request called";
 