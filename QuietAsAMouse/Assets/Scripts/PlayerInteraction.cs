using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float speedBoostDuration = 5f; // Duration of speed boost
    public float speedBoostMultiplier = 2f; // Multiplier for speed boost
    public float trapDuration = 5f; // Duration of speed decrease from trap
    public float trapSpeedDecrease = 0.5f; // Multiplier for speed decrease from trap

    private bool isSpeedBoosted = false;
    private bool isTrapped = false;
    private float originalMoveSpeed;
    private float timer;

    private void Start()
    {
        originalMoveSpeed = GetComponent<PlayerMovement>().moveSpeed;
    }

    private void Update()
    {
        // Update timers for speed boost and trap effects
        if (isSpeedBoosted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndSpeedBoost();
            }
        }
        if (isTrapped)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndTrap();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
        else if (other.CompareTag("PowerUp"))
        {
            GainSpeedBoost();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            GetTrapped();
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        // Handle player death
        Debug.Log("Player died!");
        // Implement your death logic here, such as showing a game over screen
    }

    private void GainSpeedBoost()
    {
        // Apply speed boost effect
        isSpeedBoosted = true;
        GetComponent<PlayerMovement>().moveSpeed = speedBoostMultiplier;
        timer = speedBoostDuration;
    }

    private void EndSpeedBoost()
    {
        // End speed boost effect
        isSpeedBoosted = false;
        GetComponent<PlayerMovement>().moveSpeed = originalMoveSpeed;
    }

    private void GetTrapped()
    {
        // Apply trap effect
        isTrapped = true;
        GetComponent<PlayerMovement>().moveSpeed= trapSpeedDecrease;
        timer = trapDuration;
    }

    private void EndTrap()
    {
        // End trap effect
        isTrapped = false;
        GetComponent<PlayerMovement>().moveSpeed = originalMoveSpeed;
    }
}
