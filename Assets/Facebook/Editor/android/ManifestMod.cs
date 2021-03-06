    CCB                                                                                                                                                                                                                                                                                                                                                                                              d��!                                                                                                                      !�R!�              43                                                        WU                                        3�    �1            XU!"                                                          hf33                                      !�   �1            XUdfDD  VU    VU#"    33ED  !"43""    CDDD33  ""WU         ���t   1�     S�2  ����!  0�   XUxw{wywgf��  FDFD43hf  ��gfUUXU!"��gf33��gfzw    zwxw��33zw        ��SB�eA,�Q�  d�S� !  0�S%L-��� XU!"43UUzw  ifhfhfVUywXU    XU!"ED43  XU  yw    ywVU33WU��      c�2    �e)�r  Qa	��  T� ��  :r+�      XU!"33WUyw  hfhfhfxwif""��jfGDXU!"FD{wjfVUWU  yw    yw  ED43ywgf""    ��    0�u,�A  1��S  C
����u:)�a      XU!"53EDzw  ifywWUywgf33yw13ywXU!"EDUUTUgfhf  yw    ywWU33��43ED    �T    e�v-�   Q*��B  T��1 �:*�)�Q    XUxw��ywgf��""XUEDXUED33��hfFDXUzw23��zw""��gf{w    zwxw��CDhfif    �2          *�   r�
�C  ��� 1�:(�A�'�  ""#"  VUVU  ""33DD  ""UUyw    ywTU33  $"    �!           r%r ,�Qu�  �T�1��:a%rA=�                                                  yw    yw                �B            &�8�&�    ���   ���4:  &�9�b                                                  ww    gf                �e                                     $:                                                                                  1�      �                             $:                                                                                    ��C��1                            G[                                                                                      2UT                               !                                                              ee2      33                            43                    v  u      gf43VU""4343#"ED#"4343VU#"#"WU43  2��"rAd�!�e�0DDED43EDhfxwFD$"DDGD43$"4343#"FDED  �  dHS a�!&U�#)DA  VUEDVUEDWUgfVUVUWUVUVUVU5353WUVUED  �  C7b ��2kg h:Tr          ""4343""    �     %rbd�3"�v&%�Q                        ""        �  2                                                      ! ev                                           T"  ED43VU4343434343eU"Qdf#5A#"33434343434343D!RuU#G$R                 C2        """"""!#""""!"#"#""                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;

namespace UnityEditor.FacebookEditor
{
    public class ManifestMod
    {
        public const string DeepLinkingActivityName = "com.facebook.unity.FBUnityDeepLinkingActivity";

        public const string LoginActivityName = "com.facebook.LoginActivity";

        public const string UnityLoginActivityName = "com.facebook.unity.FBUnityLoginActivity";

        public const string UnityDialogsActivityName = "com.facebook.unity.FBUnityDialogsActivity";

        public const string ApplicationIdMetaDataName = "com.facebook.sdk.ApplicationId";

        public static void GenerateManifest()
        {
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

            // only copy over a fresh copy of the AndroidManifest if one does not exist
            if (!File.Exists(outputFile))
            {
                var inputFile = Path.Combine(EditorApplication.applicationContentsPath, "PlaybackEngines/androidplayer/AndroidManifest.xml");
                File.Copy(inputFile, outputFile);
            }
            UpdateManifest(outputFile);
        }

        public static bool CheckManifest()
        {
            bool result = true;
            var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
            if (!File.Exists(outputFile))
            {
                Debug.LogError("An android manifest must be generated for the Facebook SDK to work.  Go to Facebook->Edit Settings and press \"Regenerate Android Manifest\"");
                return false;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(outputFile);

            if (doc == null)
            {
                Debug.LogError("Couldn't load " + outputFile);
                return false;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            XmlNode dict = FindChildNode(manNode, "application");

            if (dict == null)
            {
                Debug.LogError("Error parsing " + outputFile);
                return false;
            }

            string ns = dict.GetNamespaceOfPrefix("android");

            XmlElement loginElement = FindElementWithAndroidName("activity", "name", ns, UnityLoginActivityName, dict);
            if (loginElement == null)
            {
                Debug.LogError(string.Format("{0} is missing from your android manifest.  Go to Facebook->Edit Settings and press \"Regenerate Android Manifest\"", LoginActivityName));
                result = false;
            }

            var deprecatedMainActivityName = "com.facebook.unity.FBUnityPlayerActivity";
            XmlElement deprecatedElement = FindElementWithAndroidName("activity", "name", ns, deprecatedMainActivityName, dict);
            if (deprecatedElement != null)
            {
                Debug.LogWarning(string.Format("{0} is deprecated and no longer needed for the Facebook SDK.  Feel free to use your own main activity or use the default \"com.unity3d.player.UnityPlayerNativeActivity\"", deprecatedMainActivityName));
            }

            return result;
        }

        private static XmlNode FindChildNode(XmlNode parent, string name)
        {
            XmlNode curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name))
                {
                    return curr;
                }
                curr = curr.NextSibling;
            }
            return null;
        }

        private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
        {
            var curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
                {
                    return curr as XmlElement;
                }
                curr = curr.NextSibling;
            }
            return null;
        }

        public static void UpdateManifest(string fullPath)
        {
            string appId = FBSettings.AppId;

            if (!FBSettings.IsValidAppId)
            {
                Debug.LogError("You didn't specify a Facebook app ID.  Please add one using the Facebook menu in the main Unity editor.");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fullPath);

            if (doc == null)
            {
                Debug.LogError("Couldn't load " + fullPath);
                return;
            }

            XmlNode manNode = FindChildNode(doc, "manifest");
            XmlNode dict = FindChildNode(manNode, "application");

            if (dict == null)
            {
                Debug.LogError("Error parsing " + fullPath);
                return;
            }

            string ns = dict.GetNamespaceOfPrefix("android");

            //add the unity login activity
            XmlElement unityLoginElement = FindElementWithAndroidName("activity", "name", ns, UnityLoginActivityName, dict);
            if (unityLoginElement == null)
            {
                unityLoginElement = CreateUnityOverlayElement(doc, ns, UnityLoginActivityName);
                dict.AppendChild(unityLoginEl