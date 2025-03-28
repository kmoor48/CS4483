using UnityEngine;

public class BangSoundTrigger : MonoBehaviour
{
    public AudioSource bangSound; 
    public Transform playerHand;  

    private bool hasPlayed = false;  

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

