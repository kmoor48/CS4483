using TMPro;
using UnityEngine;

public class ShowJimNote : MonoBehaviour
{
    public TextMeshProUGUI noteText;   
    public GameObject noteTextPrompt; 
    public Transform playerHand;  
    public GameObject pannel;

    private bool isPlayerInTrigger = false; 
    private bool isHoldingNote = false;

    private void Start()
    {
        pannel.SetActive(false);
        noteText.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInTrigger = true;
            CheckIfHoldingNote();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInTrigger = false;
            isHoldingNote = false;
            pannel.SetActive(false);
            noteTextPrompt.SetActive(false); 
        }
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            CheckIfHoldingNote(); 
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed"); 
        }
        if (isPlayerInTrigger && isHoldingNote)
        {
            Debug.Log("R pressed with note in hand");
            noteTextPrompt.SetActive(false); 
            pannel.SetActive(true);
        }
    }

    void CheckIfHoldingNote()
    {
        if (playerHand.childCount > 0) 
        {
            GameObject heldItem = playerHand.GetChild(0).gameObject;
            if (heldItem.CompareTag("jim_note")) 
            {
                isHoldingNote = true;
                noteTextPrompt.SetActive(true); 
                return;
            }
        }

        isHoldingNote = false;
        noteTextPrompt.SetActive(false); 
    }
}


