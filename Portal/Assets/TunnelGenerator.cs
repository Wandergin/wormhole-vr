using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class TunnelGenerator : MonoBehaviour
{
    public GameObject sourcePortal;
    public GameObject targetPortal;
    public Transform tunnelWorld;
    public Transform player;
    public Transform leftController;
    public Transform headCamera;
    public GameObject groundZero;
    public GameObject groundOne;

    private Boolean placingPortal;

    public SteamVR_Input_Sources leftHandType; // 1
    public SteamVR_Input_Sources rightHandType; // 1
    public SteamVR_Action_Boolean interactUIAction; // 2
    public SteamVR_Action_Boolean grabPinch; // 3

    // Start is called before the first frame update
    void Start()
    {
        targetPortal.SetActive(false);
        sourcePortal.SetActive(false);

        tunnelWorld.rotation = sourcePortal.transform.rotation;
    }

    public bool getInteractUIActionLeftHand() // 1
    {
        if (interactUIAction.GetStateDown(leftHandType))
        {
        }
        return interactUIAction.GetStateDown(leftHandType);
    }

    public bool getLeftHandGrabPinchDown() // 1
    {
        if (grabPinch.GetStateDown(leftHandType))
        {
            placingPortal = true;
        }
        return grabPinch.GetStateDown(leftHandType);
    }

    public bool getLeftHandGrabPinchUp() // 1
    {
        if (grabPinch.GetStateUp(leftHandType))
        {
            placingPortal = false;
        }
        return grabPinch.GetStateUp(leftHandType);
    }

    public bool getInteractUIActionRightHand() // 1
    {
        return interactUIAction.GetStateDown(rightHandType);
    }


    // Update is called once per frame
    void Update()
    {
        getLeftHandGrabPinchUp();
        if ((getLeftHandGrabPinchDown() || placingPortal) && player.position.sqrMagnitude < 60000)
        {
            targetPortal.SetActive(true);
            sourcePortal.SetActive(true);

            Vector3 controllerOffsetFromCamera = -headCamera.position + leftController.position;

            Vector3 viewport = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.25f));
            Vector3 viewportTarget = controllerOffsetFromCamera * 100 + new Vector3(0, 25, 0);

            Bounds groundZeroBounds = new Bounds(groundZero.transform.position, groundZero.transform.localScale);
            Bounds groundOneBounds = new Bounds(groundOne.transform.position, groundOne.transform.localScale);


            float viewportY = viewport.y;
            float viewportTargetY = viewportTarget.y;

            if (
                targetPortal.transform.position.x < groundZeroBounds.max.x &&
                targetPortal.transform.position.z < groundZeroBounds.max.z &&
                targetPortal.transform.position.x > groundZeroBounds.min.x &&
                targetPortal.transform.position.z > groundZeroBounds.min.z
            )
            {
                viewportTargetY = groundZero.transform.position.y +2f;
                viewportY = player.transform.position.y;
            }
            else if (
                targetPortal.transform.position.x < groundOneBounds.max.x &&
                targetPortal.transform.position.z < groundOneBounds.max.z &&
                targetPortal.transform.position.x > groundOneBounds.min.x &&
                targetPortal.transform.position.z > groundOneBounds.min.z
            )
            {
                viewportTargetY = groundOne.transform.position.y + 2f;
                viewportY = player.transform.position.y;
            }

            sourcePortal.transform.position = new Vector3(viewport.x, viewportY, viewport.z);
            targetPortal.transform.position = new Vector3(viewportTarget.x, viewportTargetY, viewportTarget.z);

            sourcePortal.transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 180, 0);
            targetPortal.transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 180, 0);

            tunnelWorld.rotation = sourcePortal.transform.rotation;

            Debug.Log("Player placed a portal, source portal pos:" + sourcePortal.transform.position + " target portal pos:" + targetPortal.transform.position);

        }
        else if (getInteractUIActionRightHand())
        {
            targetPortal.SetActive(false);
            sourcePortal.SetActive(false);

            tunnelWorld.rotation = sourcePortal.transform.rotation;

        }
    }
}
