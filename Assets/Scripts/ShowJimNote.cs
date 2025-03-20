//using UnityEngine;

//public class ShowJimNote : MonoBehaviour
//{
//    public GameObject noteCanvas;  // The UI Canvas to display the note
//    public GameObject noteTextPrompt; // "Press R" UI prompt
//    public Transform playerHand;  // The player's hand where the note is held

//    private bool isPlayerInTrigger = false; // Tracks if player is in the trigger
//    private bool isHoldingNote = false; // Tracks if player has the note

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player")) // If the player enters the trigger
//        {
//            isPlayerInTrigger = true;
//            CheckIfHoldingNote();
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player")) // If the player leaves the trigger
//        {
//            isPlayerInTrigger = false;
//            isHoldingNote = false;
//            noteTextPrompt.SetActive(false); // Hide the "Press R" prompt
//        }
//    }

//    void Update()
//    {
//        // Re-check if the player is holding the note
//        if (isPlayerInTrigger)
//        {
//            noteTextPrompt.SetActive(true);
//            CheckIfHoldingNote();
//        }

//        // Show the note canvas when "R" is pressed and the player is holding the note
//        if (Input.GetKeyDown(KeyCode.R) && isPlayerInTrigger && isHoldingNote)
//        {
//            noteTextPrompt.SetActive(false); // Hide prompt
//            noteCanvas.SetActive(true); // Show the note
//        }
//    }

//    void CheckIfHoldingNote()
//    {
//        if (playerHand.childCount > 0) // Check if something is in the player's hand
//        {
//            GameObject heldItem = playerHand.GetChild(0).gameObject;
//            if (heldItem.CompareTag("jim_note")) // Check if it's the note
//            {
//                isHoldingNote = true;
//                noteTextPrompt.SetActive(true); // Show "Press R" prompt
//                return;
//            }
//        }

//        isHoldingNote = false;
//        noteTextPrompt.SetActive(false); // Hide "Press R" prompt if no note
//    }
//}

using UnityEngine;

public class ShowJimNote : MonoBehaviour
{
    public GameObject noteCanvas;  // The UI Canvas to display the note
    public GameObject noteTextPrompt; // "Press R" UI prompt
    public Transform playerHand;  // The player's hand where the note is held

    private bool isPlayerInTrigger = false; // Tracks if player is in the trigger
    private bool isHoldingNote = false; // Tracks if player has the note

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If the player enters the trigger
        {
            Debug.Log("Player entered the trigger!");
            isPlayerInTrigger = true;
            CheckIfHoldingNote();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // If the player leaves the trigger
        {
            Debug.Log("Player exited the trigger!");
            isPlayerInTrigger = false;
            isHoldingNote = false;
            noteTextPrompt.SetActive(false); // Hide the "Press R" prompt
        }
    }

    void Update()
    {
        // Re-check if the player is holding the note
        if (isPlayerInTrigger)
        {
            noteTextPrompt.SetActive(true);
            CheckIfHoldingNote();
        }

        // Show the note canvas when "R" is pressed and the player is holding the note
        if (Input.GetKeyDown(KeyCode.R) && isPlayerInTrigger && isHoldingNote)
        {
            noteTextPrompt.SetActive(false); // Hide prompt
            noteCanvas.SetActive(true); // Show the note
        }
    }

    void CheckIfHoldingNote()
    {
        if (playerHand.childCount > 0) // Check if something is in the player's hand
        {
            GameObject heldItem = playerHand.GetChild(0).gameObject;
            if (heldItem.CompareTag("jim_note")) // Check if it's the note
            {
                isHoldingNote = true;
                noteTextPrompt.SetActive(true); // Show "Press R" prompt
                return;
            }
        }

        isHoldingNote = false;
        noteTextPrompt.SetActive(false); // Hide "Press R" prompt if no note
    }
}

