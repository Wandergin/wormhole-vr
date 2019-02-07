using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

	public Transform playerCamera;
    public Transform sourcePortal;
    public Transform sourceTunnelPortal;
    public Transform targetTunnelPortal;
    public Transform targetPortal;

    // Update is called once per frame
    void Update () {
		

            //if (playercamera.position)
            Vector3 playerOffsetFromSourcePortal = playerCamera.position - sourcePortal.position;
            Vector3 playerOffsetFromSourceTunnelPortal = playerCamera.position - sourceTunnelPortal.position;
            Vector3 playerOffsetFromTargetTunnelPortal = playerCamera.position - targetTunnelPortal.position;
            Vector3 playerOffsetFromTargetPortal = playerCamera.position - targetPortal.position;

            if (System.Math.Abs(playerOffsetFromSourcePortal.x) < System.Math.Abs(playerOffsetFromSourceTunnelPortal.x))
            {
                print("Player is in the real world");

                if (this.transform.name == "Camera_Forward")
                {
                    transform.position = sourceTunnelPortal.position + playerOffsetFromSourcePortal;
                }
                else if (this.transform.name == "Camera_Backward")
                {
                    transform.position = targetTunnelPortal.position + playerOffsetFromTargetPortal;
                }
            }
            else
            {
                print("Player is in the tunnel");

                if (this.transform.name == "Camera_Forward")
                {
                    transform.position = targetPortal.position + playerOffsetFromTargetTunnelPortal;
                }
                else if (this.transform.name == "Camera_Backward")
                {
                    transform.position = sourcePortal.position + playerOffsetFromSourceTunnelPortal;
                }
            }
            //print(this.transform.name);
            //print(playerOffsetFromLocalPortal);
            //print(playerOffsetFromRemotePortal);
            //transform.position = remotePortal.position + playerOffsetFromRemotePortal;
        //else if (this.transform.name == "Camera_Remote")
        //{
        //    Vector3 playerOffsetFromPortal = playerCamera.position - targetPortal.position;
        //    print(this.transform.name);
        //    print(playerOffsetFromPortal);
        //    transform.position = targetPortal.position + playerOffsetFromPortal;
        //}

        //float angularDifferenceBetweenPortalRotations = Quaternion.Angle(localPortal.rotation, remotePortal.rotation)+180;


        //Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(0, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}
