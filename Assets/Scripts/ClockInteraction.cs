using UnityEngine;
using TMPro;


public class ClockInteraction : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Camera clockCamera;
    public Camera playerCamera;
    public GameObject player;
    public Transform playerTransform;
    public float interactionDistance = 3f;
    public GameObject[] batteries;
    public ExitDoor exitDoor; // Reference to the ExitDoor script
    public GameObject interactionText;


    private bool isEditing = false;
    private bool puzzleSolved = false;
    private int selectedHand = 1; // 1 = Hour, 2 = Minute
    private float hourRotation, minuteRotation;
    private GameObject universalLogicHandler;

    // Target rotations for puzzle completion
    private float targetHourRotation = 57f;
    private float targetMinuteRotation = -152.7f;
    private float marginOfError = 5f; // Allow some error to make it easier

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        hourRotation = hourHand.localRotation.eulerAngles.z;
        minuteRotation = minuteHand.localRotation.eulerAngles.z;

        clockCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        foreach (GameObject battery in batteries)
        {
            battery.SetActive(false);
        }
    }

    void Update()
    {
        if (puzzleSolved)
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        if (isEditing)
        {
            interactionText.gameObject.SetActive(false); // Hide text during clock interaction

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleClockEditing(); // Exit clock view with E or Esc
                return;
            }

            HandleClockAdjustment();
            CheckPuzzleSolved();
            return;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactionDistance)
        {
            interactionText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleClockEditing(); // Enter clock view
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }


    public bool IsEditingClock()
    {
        return isEditing;  // This just returns the value of isEditing
    }


    void ToggleClockEditing()
    {
        if (puzzleSolved) return;

        isEditing = !isEditing;

        Debug.Log("Toggling Clock Editing. Editing: " + isEditing); // ← Check this in Console

        if (isEditing)
        {
            interactionText.gameObject.SetActive(false); // ← Make sure this runs
            clockCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            if (player != null) player.SetActive(false);
        }
        else
        {
            ReturnToPlayerView();
        }
    }


    private void OnPuzzleSolved()
    {
        // Call the PuzzleSolved method from ExitDoor when the puzzle is completed
        exitDoor.PuzzleSolved();
    }

    void HandleClockAdjustment()
    {
        if (puzzleSolved) return; // Prevent hand adjustments after solving the puzzle

        // Use 'H' for Hour pointer and 'M' for Minute pointer
        if (Input.GetKeyDown(KeyCode.H)) selectedHand = 1; // Hour
        if (Input.GetKeyDown(KeyCode.M)) selectedHand = 2; // Minute

        float scroll = Input.GetAxis("Mouse ScrollWheel") * 10f;

        switch (selectedHand)
        {
            case 1:
                hourRotation += scroll * 30f;
                hourHand.localRotation = Quaternion.Euler(0, 0, hourRotation);
                break;
            case 2:
                minuteRotation += scroll * 6f;
                minuteHand.localRotation = Quaternion.Euler(0, 0, minuteRotation);
                break;
        }
    }

    void CheckPuzzleSolved()
    {
        float normalizedHourRotation = NormalizeAngle(hourHand.localRotation.eulerAngles.z);
        float normalizedMinuteRotation = NormalizeAngle(minuteHand.localRotation.eulerAngles.z);

        if (Mathf.Abs(normalizedHourRotation - targetHourRotation) <= marginOfError &&
            Mathf.Abs(normalizedMinuteRotation - targetMinuteRotation) <= marginOfError)
        {
            puzzleSolved = true;
            ReturnToPlayerView();

            foreach (GameObject battery in batteries)
            {
                battery.SetActive(true);
            }

            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();

            OnPuzzleSolved();
        }

    }

    // Normalize angles to be between -180° and 180°
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }


    void ReturnToPlayerView()
    {
        isEditing = false;
        clockCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        if (player != null) player.SetActive(true);
    }

    
}
