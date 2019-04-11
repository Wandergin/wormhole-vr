using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;
    public Transform headCamera;

    private bool playerIsOverlapping = false;
    private Vector3 initialPosition;

    // Using square magnitude in order to distinguish when the player is inside the tunnel
    private float portalSqrMagnitudeLowerBound = 60000f;
    private float portalSqrMagnitudeUpperBound = 68000f;

    private float portalCooldown = 0.25f;


    void Start()
    {
        initialPosition = player.position;
    }

    // Update is called once per frame
    void Update () {

		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
            Vector3 portalToHead = headCamera.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, portalToHead);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
			{

                //Debug.Log(portalToHead);
                //if (reciever.name == "ColliderPlane_B_Tunnel")
                //{

                    Debug.Log("Player position after teleportation: " + player.position);
                    Debug.Log("Position x: " + player.position.x);
                //}


                // Teleport him!
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				//Vector3 headPositionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToHead;
                Vector3 playerPositionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

                Vector3 positionToTeleport = reciever.position + playerPositionOffset;
                if (positionToTeleport.x > -10)
                {
                    player.position = reciever.position + playerPositionOffset;
                }

                //headCamera.position = reciever.position + headPositionOffset;


                playerIsOverlapping = false;


            }

   
        }

        if (player.position.y < - 100)
        {
            // Respawn
            player.position = initialPosition;
            playerIsOverlapping = false;
        }
	}

	void OnTriggerStay(Collider other)
	{
        if (other.tag == "MainCamera")
		{
            playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "MainCamera")
		{
            //print("Player exited collider!");
		}
	}
}
