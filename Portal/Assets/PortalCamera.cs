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
        Vector3 playerOffsetFromSourcePortal = playerCamera.position - sourcePortal.position;
        Vector3 playerOffsetFromSourceTunnelPortal = playerCamera.position - sourceTunnelPortal.position;
        Vector3 playerOffsetFromTargetTunnelPortal = playerCamera.position - targetTunnelPortal.position;
        Vector3 playerOffsetFromTargetPortal = playerCamera.position - targetPortal.position;

        if (System.Math.Abs(playerOffsetFromSourcePortal.x) < System.Math.Abs(playerOffsetFromSourceTunnelPortal.x))
        {
            //print("Player is in the real world");

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
            //print("Player is in the tunnel");

            if (this.transform.name == "Camera_Forward")
            {
                transform.position = targetPortal.position + playerOffsetFromTargetTunnelPortal;
            }
            else if (this.transform.name == "Camera_Backward")
            {
                transform.position = sourcePortal.position + playerOffsetFromSourceTunnelPortal;
            }
        }

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(targetPortal.rotation, sourcePortal.rotation);
        //print("Angular difference between two portals: ");
        //print(angularDifferenceBetweenPortalRotations);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        //newCameraDirection = new Vector3(newCameraDirection.x, newCameraDirection.y, playerCamera.rotation.z);
        
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        transform.Rotate(0, 0, playerCamera.rotation.z);
    }
}
