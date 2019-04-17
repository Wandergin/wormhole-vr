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

    void Start()
    {
        initialPosition = player.position;
    }

    // Update is called once per frame
    void Update () {

        Debug.Log("Pos: " + headCamera.position + " Rot: " + headCamera.rotation.eulerAngles);

		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
            Vector3 portalToHead = headCamera.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToHead);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f && dotProduct > -5)
			{
                Debug.Log("TELEPORTING FROM: " + headCamera.position + " TO: " + reciever.position);

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

                playerIsOverlapping = false;
            }
        }

        if (player.position.y < - 50)
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
