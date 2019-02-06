using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera_A : MonoBehaviour {

    public Transform playerCamera;
    public Transform localPortal;
    public Transform remotePortal;

    // Update is called once per frame
    void Update () {
        Vector3 playerOffsetFromPortal = playerCamera.position - remotePortal.position;
        transform.position = localPortal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(localPortal.rotation, remotePortal.rotation);


        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
