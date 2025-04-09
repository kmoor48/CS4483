using UnityEngine;

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
        if (puzzleSolved) return; // Ignore all interactions after solving the puzzle

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            ToggleClockEditing();
        }

        if (isEditing)
        {
            HandleClockAdjustment();
            CheckPuzzleSolved(); // Check if the puzzle is solved after each adjustment
        }
    }

    public bool IsEditingClock()
    {
        return isEditing;  // This just returns the value of isEditing
    }


    void ToggleClockEditing()
    {
        if (puzzleSolved) return; // Prevent interaction after solving the puzzle

        isEditing = !isEditing;

        if (isEditing)
        {
            clockCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            if (player != null) player.SetActive(false);
        }
        else
        {
            ReturnToPlayerView();
        }
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
