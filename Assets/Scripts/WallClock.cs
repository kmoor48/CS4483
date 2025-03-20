using UnityEngine;

public class WallClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;

    public float timeMultiplier = 1f; // Speed up or slow down time
    private float timer = 0f;

    private int hours = 12;
    private int minutes = 0;

    void Update()
    {
        timer += Time.deltaTime * timeMultiplier;
        if (timer >= 1f)
        {
            timer -= 1f;
            UpdateTime();
            UpdateClockHands();
        }
    }

    void UpdateTime()
    {
        minutes++;
        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        if (hours >= 12) // 12-hour format
        {
            hours = 0;
        }
    }

    void UpdateClockHands()
    {
        // Adjust minute hand rotation
        if (minuteHand != null)
            minuteHand.localRotation = Quaternion.Euler(0, 0, minutes * 6f); // 6 degrees per minute

        // Adjust hour hand rotation, considering both hours and minutes
        if (hourHand != null)
            hourHand.localRotation = Quaternion.Euler(0, 0, hours * 30f + (minutes / 2f)); // 30 degrees per hour, plus adjustment for minutes
    }
}
