using UnityEngine;
using TMPro;

public class ClockInteraction : MonoBehaviour
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Camera clockCamera;
    [SerializeField] private Camera playerCamera;
    private GameObject player;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private GameObject[] batteries;
    [SerializeField] private ExitDoor exitDoor;
    [SerializeField] private GameObject interactionText;

    private bool isEditing = false;
    private bool puzzleSolved = false;
    private int selectedHand = 1;
    private float hourRotation, minuteRotation;
    private GameObject universalLogicHandler;

    private float targetHourRotation = 57f;
    private float targetMinuteRotation = -152.7f;
    private float marginOfError = 5f;

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        player = GameObject.FindWithTag("Player");

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
            interactionText.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleClockEditing();
                return;
            }

            HandleClockAdjustment();
            CheckPuzzleSolved();
            return;
        }

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= interactionDistance)
            {
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleClockEditing();
                }
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    public bool IsEditingClock()
    {
        return isEditing;
    }

    void ToggleClockEditing()
    {
        if (puzzleSolved) return;

        isEditing = !isEditing;

        Debug.Log("Toggling Clock Editing. Editing: " + isEditing);

        if (isEditing)
        {
            interactionText.gameObject.SetActive(false);
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
        exitDoor.PuzzleSolved();
    }

    void HandleClockAdjustment()
    {
        if (puzzleSolved) return;

        if (Input.GetKeyDown(KeyCode.H)) selectedHand = 1;
        if (Input.GetKeyDown(KeyCode.M)) selectedHand = 2;

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

            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();

            OnPuzzleSolved();
        }
    }

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
