using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {

	public Camera camera_Local;
    public Camera camera_Remote;


	public Material cameraMat_Backward;
	public Material cameraMat_Forward;

    // Use this for initialization
    void Start () {
		if (camera_Remote.targetTexture != null)
		{
            camera_Remote.targetTexture.Release();
		}
        camera_Remote.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat_Backward.mainTexture = camera_Remote.targetTexture;

        if (camera_Local.targetTexture != null)
        {
            camera_Local.targetTexture.Release();
        }
        camera_Local.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat_Forward.mainTexture = camera_Local.targetTexture;
    }
	
}
