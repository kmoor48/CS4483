using TMPro;
using UnityEngine;

public class ShowJimNote : MonoBehaviour
{
    public TextMeshProUGUI noteText;
    public GameObject pannel;

    private Transform playerHand;
    private Transform player;
    private bool isPlayerInTrigger = false;
    private bool isHoldingNote = false;
    private GameObject universalLogicHandler;

    private void Start()
    {
        pannel.SetActive(false);
        noteText.gameObject.SetActive(false);

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        player = GameObject.FindWithTag("Player")?.transform;

        GameObject handObj = GameObject.FindWithTag("PlayerRightHandTarget");
        if (handObj != null)
        {
            playerHand = handObj.transform;
        }
        else
        {
            Debug.LogError("PlayerRightHandTarget tag not found in scene.");
        }
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

            if (universalLogicHandler != null)
            {
                LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
                clueScript?.IncrementPuzzleCounter();
            }
        }
    }

    void CheckIfHoldingNote()
    {
        if (playerHand != null && playerHand.childCount > 0)
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


