using UnityEngine;
using UnityEngine.UI;

public class BookshelfInteraction : MonoBehaviour
{
    public Camera puzzleCamera; // Assign a separate puzzle camera in the Inspector
    public Camera playerCamera;
    public GameObject puzzleUI; // UI with book dragging elements
    private bool isNearBookshelf = false;

    void Update()
    {
        if (isNearBookshelf && Input.GetKeyDown(KeyCode.E))
        {
            EnterPuzzleMode();
        }
    }

    void EnterPuzzleMode()
    {
        playerCamera.gameObject.SetActive(false);
        puzzleCamera.gameObject.SetActive(true);
        puzzleUI.SetActive(true);
    }

    void ExitPuzzleMode()
    {
        playerCamera.gameObject.SetActive(true);
        puzzleCamera.gameObject.SetActive(false);
        puzzleUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBookshelf = true;
            // Show UI prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBookshelf = false;
            // Hide UI prompt
        }
    }
}
