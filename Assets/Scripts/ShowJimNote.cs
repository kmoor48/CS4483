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
//            Debug.Log("Player entered the trigger!");
//            isPlayerInTrigger = true;
//            CheckIfHoldingNote();
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player")) // If the player leaves the trigger
//        {
//            Debug.Log("Player exited the trigger!");
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
//            Debug.Log("made it to canvas");
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
//            noteTextPrompt.SetActive(true);  // Make sure the prompt is activated when inside the trigger
//            CheckIfHoldingNote();
//        }

//        // Show the note canvas when "R" is pressed and the player is holding the note
//        if (Input.GetKeyDown(KeyCode.R) && isPlayerInTrigger && isHoldingNote)
//        {
//            Debug.Log("R pressed with note in hand");
//            noteTextPrompt.SetActive(false); // Hide prompt
//            noteCanvas.SetActive(true); // Show the note canvas
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
//                Debug.Log("holding note");
//                noteTextPrompt.SetActive(true); // Show "Press R" prompt
//                return;
//            }
//        }

//        isHoldingNote = false;
//        noteTextPrompt.SetActive(false); // Hide "Press R" prompt if no note
//    }
//}

using TMPro;
using UnityEngine;

public class ShowJimNote : MonoBehaviour
{
    public TextMeshProUGUI noteText;   // The UI Canvas to display the note
    public GameObject noteTextPrompt; // "Press R" UI prompt
    public Transform playerHand;  // The player's hand where the note is held

    private bool isPlayerInTrigger = false; // Tracks if player is in the trigger
    private bool isHoldingNote = false; // Tracks if player has the note

    private void Start()
    {
        noteText.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If the player enters the trigger
        {
            isPlayerInTrigger = true;
            CheckIfHoldingNote();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // If the player leaves the trigger
        {
            isPlayerInTrigger = false;
            isHoldingNote = false;
            noteTextPrompt.SetActive(false); // Hide the "Press R" prompt
        }
    }

    void Update()
    {
        // Only check if the player is in trigger if they are in the trigger
        if (isPlayerInTrigger)
        {
            CheckIfHoldingNote(); // Recheck if player is holding the note
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed"); // Check if this appears in Console
        }

        // Show the note canvas when "R" is pressed and the player is holding the note
        if (isPlayerInTrigger && isHoldingNote)
        {
            Debug.Log("R pressed with note in hand");
            noteTextPrompt.SetActive(false); // Hide prompt
            noteText.gameObject.SetActive(true); // Show the note canvas
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


