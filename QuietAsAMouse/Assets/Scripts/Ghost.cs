using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public float raycastDistance = 1f; // Distance to cast rays to detect obstacles

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Check if there's an obstacle in front of the enemy
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, raycastDistance))
            {
                // If there's an obstacle, adjust direction away from it
                if (hit.collider.gameObject != player.gameObject)
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                }
            }

            // Move the enemy along the adjusted direction
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}