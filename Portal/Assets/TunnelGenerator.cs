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

    public SteamVR_Input_Sources leftHandType; // 1
    public SteamVR_Input_Sources rightHandType; // 1
    public SteamVR_Action_Boolean interactUIAction; // 2

    // Start is called before the first frame update
    void Start()
    {
        targetPortal.SetActive(false);
        sourcePortal.SetActive(false);

        tunnelWorld.rotation = sourcePortal.transform.rotation;
    }

    public bool getInteractUIActionLeftHand() // 1
    {
        return interactUIAction.GetStateDown(leftHandType);
    }

    public bool getInteractUIActionRightHand() // 1
    {
        return interactUIAction.GetStateDown(rightHandType);
    }


    // Update is called once per frame
    void Update()
    {
        if (getInteractUIActionLeftHand() && player.position.sqrMagnitude < 60000)
        {
            targetPortal.SetActive(true);
            sourcePortal.SetActive(true);

            Vector3 viewport = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
            Vector3 viewportTarget = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 50.0f));

            sourcePortal.transform.position = new Vector3(viewport.x, 2.0f, viewport.z);
            targetPortal.transform.position = new Vector3(viewportTarget.x, 2.0f, viewportTarget.z);

            sourcePortal.transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 180, 0);
            targetPortal.transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y - 180, 0);

            tunnelWorld.rotation = sourcePortal.transform.rotation;
        }
        else if (getInteractUIActionRightHand())
        {
            targetPortal.SetActive(false);
            sourcePortal.SetActive(false);
        }
    }
}
