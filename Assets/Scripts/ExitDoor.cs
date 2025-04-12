using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public int puzzlesRequiredForLevel = 1; // Number of puzzles required for the level
    private int puzzlesSolved = 0;
    private bool isUnlocked = false;

    public GameObject exitDoor; // The door GameObject to unlock
    private BoxCollider doorCollider; // The BoxCollider on the door

    public int doorIndex; // Index or identifier for where the door leads

    private void Start()
    {
        // Initially, we make sure the collider is disabled until the puzzles are solved
        doorCollider = exitDoor.GetComponent<BoxCollider>();
        if (doorCollider != null)
        {
            doorCollider.enabled = false; // Disable the collider initially
        }
        else
        {
            Debug.LogWarning("No BoxCollider attached to the exit door.");
        }

        // Optional: log the destination index for debugging
        Debug.Log("ExitDoor initialized with destination index: " + doorIndex);
    }

    // This method should be called when a puzzle is solved
    public void PuzzleSolved()
    {
        puzzlesSolved++;
        CheckDoorUnlock();
    }

    private void CheckDoorUnlock()
    {
        // If the number of puzzles solved is equal to the required puzzles for the level
        if (puzzlesSolved >= puzzlesRequiredForLevel && !isUnlocked)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        // Enable the BoxCollider to allow the player to interact with the door
        if (doorCollider != null)
        {
            doorCollider.enabled = true; // Enable the collider to interact with the door
        }

        // Optionally, you can change the appearance of the door to indicate it's unlocked
        exitDoor.SetActive(true); // Keep this active or change appearance to indicate it's ready to interact with
        Debug.Log("Exit Door Unlocked! Leads to index: " + doorIndex);
    }
}
