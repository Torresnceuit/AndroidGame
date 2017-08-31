using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class NativeXAndroid {

	public string appId;
	//private string packageName;
	//private string displayName;
	public string publisherUserId;
	public int actionId;
	public bool disableLegacyIDs = true;
	public bool enableLogging = false;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public NativeXAndroid(int applicationId, string packName, string disName, string pubName)
	{
		appId = applicationId.ToString();
		//packageName = packName;
		//displayName = disName;
		publisherUserId = pubName;
	}

	public NativeXAndroid(int applicationId, str