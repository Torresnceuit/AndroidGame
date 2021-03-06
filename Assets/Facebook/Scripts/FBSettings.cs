               �ɉ�8�v���  ��                                Q��a���%�5���V�9������>`�S>                ���<�<*�9  ��                                ,eA\ ��`�0���#�˥{:���yX(>��>                 �W���	��V7  ��                                ,eA����`�0��ח�    ���	� >�:�>                Jn�B �>�V�:  ��                                ,e�n�@�%�          ��lx�>� 0=                �[��
z�      ��                                ,e�n�@`�0>`C���F~?    ?(\�=                �F~�`C��      �?                                �� A�G�@�%�          ��sג=�\>>                �}W��3
�      ��                                N                       
 	 - -  
                               " !     # " & , . . ' & ) ( , , & ) ) + * * ( )     EHSM                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif
public class FBSettings : ScriptableObject
{

    const string facebookSettingsAssetName = "FacebookSettings";
    const string facebookSettingsPath = "Facebook/Resources";
    const string facebookSettingsAssetExtension = ".asset";

    private static FBSettings instance;

    static FBSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load(facebookSettingsAssetName) as FBSettings;
                if (instance == null)
                {
                    // If not found, autocreate the asset object.
                    instance = CreateInstance<FBSettings>();
#if UNITY_EDITOR
                    string properPath = Path.Combine(Application.dataPath, facebookSettingsPath);
                    if (!Directory.Exists(properPath))
                    {
                        AssetDatabase.CreateFolder("Assets/Facebook", "Resources");
                    }

                    string fullPath = Path.Combine(Path.Combine("Assets", facebookSettingsPath),
                                                   facebookSettingsAssetName + facebookSettingsAssetExtension
                                                  );
                    AssetDatabase.CreateAsset(instance, fullPath);
#endif
                }
            }
            return instance;
        }
    }

#if UNITY_EDITOR
    [MenuItem("Facebook/Edit Settings")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }

    [MenuItem("Facebook/Developers Page")]
    public static void OpenAppPage()
    {
        string url = "https://developers.facebook.com/apps/";
        if (Instance.AppIds[Instance.SelectedAppIndex] != "0")
            url += Instance.AppIds[Instance.SelectedAppIndex];
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/SDK Documentation")]
    public static void OpenDocumentation()
    {
        string url = "https://developers.facebook.com/games/unity/";
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/SDK Facebook Group")]
    public static void OpenFacebookGroup()
    {
        string url = "https://www.facebook.com/groups/491736600899443/";
        Application.OpenURL(url);
    }

    [MenuItem("Facebook/Report a SDK Bug")]
    public static void ReportABug()
    {
        string url = "https://developers.facebook.com/bugs/create";
        Application.OpenURL(url);
    }
#endif

    #region App Settings

    [SerializeField]
    private int selectedAppIndex = 0;
    [SerializeField]
    private string[] appIds = new[] { "0" };
    [SerializeField]
    private string[] appLabels = new[] { "App Name" };
    [SerializeField]
    private bool cookie = true;
    [SerializeField]
    private bool logging = true;
    [SerializeField]
    private bool status = true;
    [SerializeField]
    private bool xfbml = false;
    [SerializeField]
    private bool frictionlessRequests = true;
    [SerializeField]
    private string iosURLSuffix = "";

    public void SetAppIndex(int index)
    {
        if (selectedAppIndex != index)
        {
            selectedAppIndex = index;
            DirtyEditor();
        }
    }

    public int SelectedAppIndex
    {
        g