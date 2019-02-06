using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {

	public Camera cameraB;
    public Camera cameraA_Tunnel;
    public Camera cameraB_Tunnel;


	public Material cameraMatA;
	public Material cameraMatB;

    public Material cameraMatA_Tunnel;
    public Material cameraMatB_Tunnel;

    // Use this for initialization
    void Start () {
		if (cameraB.targetTexture != null)
		{
			cameraB.targetTexture.Release();
		}
		cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatB.mainTexture = cameraB.targetTexture;

        if (cameraB_Tunnel.targetTexture != null)
        {
            cameraB_Tunnel.targetTexture.Release();
        }
        cameraB_Tunnel.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB_Tunnel.mainTexture = cameraB_Tunnel.targetTexture;
    }
	
}
