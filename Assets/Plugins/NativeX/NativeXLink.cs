using UnityEngine;

public class NativeXLink : MonoBehaviour {

	public void moveScene(string scene)
	{
		Application.LoadLevel(scene);
	}
}
