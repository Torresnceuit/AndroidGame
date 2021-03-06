INDX( 	 +u[�           (   �  �         c c                 \s    � �     Ls    �v�;���т4<��`��^��v�;���       y               $6 9 a 7 a e c 1 b 6 e 2 5 3 3 5 e 6 5 f 7 b f 2 1 b 1 8 6 0 c 2 . b i n       \s    p Z     Ls    �v�;���т4<��`��^��v�;���       y               6 9 A 7 A E ~ 1 . B I N       ]s    � �     Ls    .�v�;����@�bv�����^�.�v�;��       �              $6 a 4 9 e 2 c 6 2 8 8 4 3 9 a 8 d 0 0 b f 5 e 7 7 2 b f b 8 f 6 . b i n       ]s    p Z    Ls    .�v�;����@�bv�����^�.�v�;��       �              6 A 4 9 E 2 ~ 1 . B I N       ^s    � �     Ls    I�v�;��ž")<�� ��^�I�v�;��                     $6 a 8 b 4 a 2 3 6 7 8 9 9 8 b 4 7 a 8 0 0 b 0 6 5 1 a 6 6 e 1 5 . b i n       ^s    p Z     Ls    I�v�;��ž")<�� ��^�I�v�;��                     6 A 8 B 4 A ~ 1 . B I N       _s    � �     Ls    W�v�;���h�(<���3��^�W�v�;��       `              $6 a 9 3 a 2 d 6 b c 0 b b 1 0 e a f 0 b 2 e 6 f f 4 b 6 1 8  5 . b i n       _s    p Z     Ls    W�v�;���h�(<���3��^�W�v�;��       `              6 A 9 3 A 2 ~ 1 . B I N       `s    � �     Ls    w%w�;����,<���Z��^�w%w�;��        �              $6 c 4 0 9 0 f 0 6 8 b d 3 3 f b 2 8 3 a 1 4 0 a b 8 f 6 7 b 7 8 . b i n       `s    p Z     Ls    w%w�;����,<���Z��^�w%w�;��        �              6 C 4 0 9 0 ~ 1 . B I N       as    � �     Ls    �Lw�;�����,<������^��Lw�;��`       ]               $6 c d 8 b f 3 1 2 3 4 2 7 4  b 5 4 5 3 8 c 3 4 1 3 1 d 3 5 8 3 . b i n       as    p Z     Ls    �Lw�;�����,<������^��Lw�;��`       ]               6 C D 8 B F ~ 1 . B I N       bs    � �     Ls    �sw�;�����(<��C���^��sw�;��       �              $6 d f 8 8 9 2 0 6 4 8 c 5 a 3 1 4 c b a 6 c b 8 e c c d 7 d 6 5 . b i n       bs    p Z     Ls    �sw�;�����(<��C���^��sw�;��       �              6 D F 8 8 9 ~ 1 . B I N       cs    � �     Ls    ��w�;�������w�Q���^���w�;�� 0      �!             $6 f 3 8 4 f 1 a 5 b 1 4 7 a 4 3 2 4 1 8 6 e b 7 7 1 f 5 3 3 c 4 . b i n       cs    p Z     Ls    ��w�;�������w�Q���^���w�;�� 0      �!              6 F 3 8 4 F ~ 1 . B I N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(FBSettings))]
public class FacebookSettingsEditor : Editor
{
    bool showFacebookInitSettings = false;
    bool showAndroidUtils = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
	// Unity renamed build target from iPhone to iOS in Unity 5, this keeps both versions happy
    bool showIOSSettings = (EditorUserBuildSettings.activeBuildTarget.ToString() == "iPhone"
	                        || EditorUserBuildSettings.activeBuildTarget.ToString() == "iOS");

    GUIContent appNameLabel = new GUIContent("App Name [?]:", "For your own use and organization.\n(ex. 'dev', 'qa', 'prod')");
    GUIContent appIdLabel = new GUIContent("App Id [?]:", "Facebook App Ids can be found at https://developers.facebook.com/apps");

    GUIContent urlSuffixLabel = new GUIContent ("URL Scheme Suffix [?]", "Use this to share Facebook APP ID's across multiple iOS apps.  https://developers.facebook.com/docs/ios/share-appid-across-multiple-apps-ios-sdk/");
    
    GUIContent cookieLabel = new GUIContent("Cookie [?]", "Sets a cookie which your server-side code can use to validate a user's Facebook session");
    GUIContent loggingLabel = new GUIContent("Logging [?]", "(Web Player only) If true, outputs a verbose log to the Javascript console to facilitate debugging.");
    GUIContent statusLabel = new GUIContent("Status [?]", "If 'true', attempts to initialize the Facebook object with valid session data.");
    GUIContent xfbmlLabel = new GUIContent("Xfbml [?]", "(Web Player only If true) Facebook will immediately parse any XFBML elements on the Facebook Canvas page hosting the app");
    GUIContent frictionlessLabel = new GUIContent("Frictionless Requests [?]", "Use frictionless app requests, as described in their own documentation.");

    GUIContent packageNameLabel = new GUIContent("Package Name [?]", "aka: the bundle identifier");
    GUIContent classNameLabel = new GUIContent("Class Name [?]", "aka: the activity name");
    GUIContent debugAndroidKeyLabel = new GUIContent("Debug Android Key Hash [?]", "Copy this key to the Facebook Settings in order to test a Facebook Android app");

    GUIContent sdkVersion = new GUIContent("SDK Version [?]", "This Unity Facebook SDK version.  If you have problems or compliments please include this so we know exactly what version to look out for.");
    GUIContent buildVersion = new GUIContent("Build Version [?]", "This Unity Facebook SDK version.  If you have problems or compliments please include this so we know exactly what version to look out for.");

    private FBSettings instance;

    public override void OnInspectorGUI()
    {
        instance = (FBSettings)target;

        AppIdGUI();
        FBParamsInitGUI();
        AndroidUtilGUI();
        IOSUtilGUI();
        AboutGUI();
    }

    private void AppIdGUI()
    {
        EditorGUILayout.HelpBox("1) Add the Facebook App Id(s) associated with this game", MessageType.None);
        if (instance.AppIds.Length == 0 || instance.AppIds[instance.SelectedAppIndex] == "0")
        {
            EditorGUILayout.HelpBox("Invalid App Id", MessageType.Error);
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(appNameLabel);
        EditorGUILayout.LabelField(appIdLabel);
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < instance.AppIds.Length; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            instance.SetAppLabel(i, EditorGUILayout.TextField(instance.AppLabels[i]));
            GUI.changed = false;
            instance.SetAppId(i, EditorGUILayout.TextField(instance.AppIds[i]));
            if (GUI.changed)
                ManifestMod.GenerateManifest();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Another App Id"))
        {
            var appLabels = new List<string>(instance.AppLabels);
            appLabels.Add("New App");
            instance.AppLabels = appLabels.ToArray();

            var appIds = new List<string>(instance.AppIds);
            appIds.Add("0");
            instance.AppIds = appIds.ToArray();
        }
        if (instance.AppLabels.Length > 1)
        {
            if (GUILayout.Button("Remove Last App Id"))
            {
                var appLabels = new List<string>(instance.AppLabels);
                appLabels.RemoveAt(appLabels.Count - 1);
                instance.AppLabels = appLabels.ToArray();

                var appIds = new List<string>(instance.AppIds);
                appIds.RemoveAt(appIds.Count - 1);
                instance.AppIds = appIds.ToArray();
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        if (instance.AppIds.Length > 1)
        {
            EditorGUILayout.HelpBox("2) Select Facebook App Id to be compiled with this game", MessageType.None);
            GUI.changed = false;
            instance.SetAppIndex(EditorGUILayout.Popup("Selected App Id", instance.SelectedAppIndex, instance.AppLabels));
            if (GUI.changed)
                ManifestMod.GenerateManifest();
            EditorGUILayout.Space();
        }
        else
        {
            instance.SetAppIndex(0);
        }
    }

    private void FBParamsInitGUI()
    {
        EditorGUILayout.HelpBox("(Optional) Edit the FB.Init() parameters", MessageType.None);
        showFacebookInitSettings = EditorGUILayout.Foldout(showFacebookInitSettings, "FB.Init() Parameters");
        if (showFacebookInitSettings)
        {
            FBSettings.Cookie = EditorGUILayout.Toggle(cookieLabel, FBSettings.Cookie);
            FBSettings.Logging = EditorGUILayout.Toggle(loggingLabel, FBSettings.Logging);
            FBSettings.Status = EditorGUILayout.