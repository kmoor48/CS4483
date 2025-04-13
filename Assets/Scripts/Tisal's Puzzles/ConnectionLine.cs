using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectionLine : MonoBehaviour
{
    public RectTransform lineRect;
    public TextMeshProUGUI numberLabel;
    private ProvinceButton startProvince;
    private ProvinceButton endProvince;

    // Customization options
    public float lineThickness = 5f;

    // Offset controls - helps make lines not extend all the way to province borders
    [Range(0f, 0.4f)]
    public float startEndOffset = 0.2f; // Percentage of total distance to offset from ends

    public void Initialize(ProvinceButton start, ProvinceButton end, string number)
    {
        // Store references to the provinces
        startProvince = start;
        endProvince = end;

        // Use the RectTransform positions for UI elements
        RectTransform startRect = start.GetComponent<RectTransform>();
        RectTransform endRect = end.GetComponent<RectTransform>();

        // Get positions in canvas space
        Vector3 startPos = startRect.position;
        Vector3 endPos = endRect.position;

        // Calculate direction and distance
        Vector3 direction = endPos - startPos;
        float totalDistance = direction.magnitude;

        // Apply offset to start and end points so lines don't extend all the way to province borders
        Vector3 offsetDirection = direction.normalized;
        float offsetAmount = totalDistance * startEndOffset;

        Vector3 adjustedStartPos = startPos + (offsetDirection * offsetAmount);
        Vector3 adjustedEndPos = endPos - (offsetDirection * offsetAmount);

        // Recalculate with adjusted positions
        direction = adjustedEndPos - adjustedStartPos;
        float distance = direction.magnitude;

        // Position the line midway between adjusted points
        transform.position = adjustedStartPos + direction / 2;

        // Rotate the line to point from start to end
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Scale the line to cover the adjusted distance
        lineRect.sizeDelta = new Vector2(distance, lineThickness);

        // Set the number label
        if (numberLabel != null)
        {
            numberLabel.text = number;
            // Place the number in the middle of the line
            numberLabel.transform.position = adjustedStartPos + direction / 2;
            // Make sure label faces the right way
            numberLabel.transform.rotation = Quaternion.identity;

            // Optional: Add a small offset to make sure the number doesn't overlap the line
            numberLabel.transform.position += new Vector3(0, 0, -1);
        }
    }

    public ProvinceButton GetStartProvince()
    {
        return startProvince;
    }

    public ProvinceButton GetEndProvince()
    {
        return endProvince;
    }
}