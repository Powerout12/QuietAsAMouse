using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    public GameObject enemyToDisappear;
    public GameObject wallToDespawn;
    public Light lightToTurnOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the enemy object
            if (enemyToDisappear != null)
            {
                enemyToDisappear.SetActive(false);
            }

            // Despawn the wall
            if (wallToDespawn != null)
            {
                Destroy(wallToDespawn);
            }

            // Turn on the light
            if (lightToTurnOn != null)
            {
                lightToTurnOn.enabled = true;
            }
        }
    }
}
