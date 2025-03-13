using UnityEngine;

public class ClockInteraction : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Camera clockCamera; // Camera for looking at the clock
    public Camera playerCamera; // Main player camera
    public GameObject player; // Reference to the player (to disable movement)

    private bool isEditing = false;
    private int selectedHand = 1; // 1 = Hour, 2 = Minute
    private float hourRotation, minuteRotation;

    void Start()
    {
        // Store initial hand positions
        hourRotation = hourHand.localRotation.eulerAngles.z;
        minuteRotation = minuteHand.localRotation.eulerAngles.z;

        // Ensure the player starts with their main camera
        clockCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleClockEditing();
        }

        if (isEditing)
        {
            HandleClockAdjustment();
        }
    }

    void ToggleClockEditing()
    {
        isEditing = !isEditing;

        if (isEditing)
        {
            // Switch to clock camera
            clockCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);

            // Disable player movement
            if (player != null)
                player.SetActive(false);
        }
        else
        {
            // Switch back to player camera
            clockCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);

            // Re-enable player movement
            if (player != null)
                player.SetActive(true);
        }
    }

    void HandleClockAdjustment()
    {
        // Select hand to adjust
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedHand = 1; // Hour
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedHand = 2; // Minute

        // Adjust selected hand with mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel") * 10f;

        switch (selectedHand)
        {
            case 1: // Adjust Hour Hand
                hourRotation += scroll * 30f; // 30° per hour
                hourHand.localRotation = Quaternion.Euler(0, 0, hourRotation);
                break;
            case 2: // Adjust Minute Hand
                minuteRotation += scroll * 6f; // 6° per minute
                minuteHand.localRotation = Quaternion.Euler(0, 0, minuteRotation);
                break;
        }
    }
}
