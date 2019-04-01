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
        //Debug.Log("hi");
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
            Vector3 portalToHead = headCamera.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, portalToHead);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
            }
        }

        if (player.position.y < - 10)
        {
            // Respawn
            player.position = initialPosition;
            playerIsOverlapping = false;
        }
	}

	void OnTriggerEnter (Collider other)
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
            print("Player exited collider!");
		}
	}
}
