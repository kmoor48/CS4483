using TMPro;
using UnityEngine;

public class ShowJimNote : MonoBehaviour
{
    public TextMeshProUGUI noteText;   
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
            noteText.gameObject.SetActive(false);
            pannel.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInTrigger)
        {
            CheckIfHoldingNote(); 
        }

        if (isPlayerInTrigger && isHoldingNote)
        {
            noteText.gameObject.SetActive(true);
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
                return;
            }
        }

        isHoldingNote = false;
    }
}


