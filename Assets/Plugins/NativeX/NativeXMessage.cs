using UnityEngine;

public class NativeXMessage {

	public string DisplayName;
	public string DisplayText;
	public string ReferenceName;

	public NativeXMessage()
	{
	}

	public NativeXMessage(string disName, string disText, string refName)
	{
		DisplayName = disName;
		DisplayText = disText;
		ReferenceName = refName;
	}

	public override string ToString ()
	{
		return "DisplayName:"+DisplayName+" - DisplayText:"+DisplayText+" - ReferenceName:"+ReferenceName;
	}


}
