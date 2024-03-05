using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    public GameObject fan; // Reference to the fan GameObject
    public Light winLight; // Reference to the light component for the win condition
    public AudioClip fanActivationSound; // Sound to play when the fan is activated
    public AudioClip winSound; // Sound to play when the win condition is triggered

    private Animator fanAnimator;
    private bool isFanActivated = false;

    private void Start()
    {
        fanAnimator = fan.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Remote"))
        {
            ActivateFan();
            TurnOnWinLight();
            TriggerWinCondition();
            Destroy(other.gameObject);
        }
    }

    private void ActivateFan()
    {
        if (!isFanActivated)
        {
            isFanActivated = true;
            // Play animation
            fanAnimator.SetTrigger("Activate");
            // Play sound
            AudioSource.PlayClipAtPoint(fanActivationSound, fan.transform.position);
        }
    }

    private void TurnOnWinLight()
    {
        winLight.enabled = true;
    }

    private void TriggerWinCondition()
    {
        Debug.Log("You win!");
        // Implement your win condition logic here, such as showing a win screen
        // Play win sound
        AudioSource.PlayClipAtPoint(winSound, transform.position);
    }
}