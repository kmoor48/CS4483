using UnityEngine;

public class CollisionTriggerPuzzleSwitch : MonoBehaviour
{
    public GameObject roomLogicHandler;
    public GameObject enterPuzzleText;
    public string puzzleToSwitchTo;

    private bool playerInRange = false;
    private CameraSwitcherPuzzleView cameraSwitcherScript;

    void Start()
    {
        cameraSwitcherScript = roomLogicHandler.GetComponent<CameraSwitcherPuzzleView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterPuzzleText.SetActive(true); // Show "Pick up object?" when near
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterPuzzleText.SetActive(false); // Hide the text when player moves away
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Pickup when pressing "E"
        {
            if (cameraSwitcherScript != null)
            {
                cameraSwitcherScript.SwitchToPuzzleCamera(puzzleToSwitchTo);

                // Hide the text
                enterPuzzleText.SetActive(false);
            }
            else
            {
                Debug.LogError("No Camera Switcher Puzzle Script!");
            }
        }
    }
}
