using UnityEngine;

public class BangSoundTrigger : MonoBehaviour
{
    public AudioSource bangSound;  // Assign in Inspector
    public Transform playerHand;  // Assign the player's hand object

    private bool hasPlayed = false;  // Ensure it plays only once

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player") && IsHoldingNote())
        {
            bangSound.Play();
            hasPlayed = true;
        }
    }

    private bool IsHoldingNote()
    {
        if (playerHand.childCount > 0)
        {
            GameObject heldItem = playerHand.GetChild(0).gameObject;
            return heldItem.CompareTag("jim_note");
        }
        return false;
    }
}

