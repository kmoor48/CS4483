using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectionLine : MonoBehaviour
{
    public RectTransform lineRect;
    public TextMeshProUGUI numberLabel;
    private ProvinceButton startProvince;
    private ProvinceButton endProvince;

    // Optional: Add a public variable to control line thickness
    public float lineThickness = 5f; // Adjust this value to control line width

    public void Initialize(ProvinceButton start, ProvinceButton end, string number)
    {
        // Store references to the provinces
        startProvince = start;
        endProvince = end;

        // Get positions
        Vector3 startPos = start.transform.position;
        Vector3 endPos = end.transform.position;

        // Calculate position, rotation and scale for the line
        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;

        // Position the line midway between start and end
        transform.position = startPos + direction / 2;

        // Rotate the line to point from start to end
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        // Scale the line to cover the distance
        // Set the line width to a smaller value (lineThickness)
        lineRect.sizeDelta = new Vector2(lineThickness, distance);

        // Set the number label
        if (numberLabel != null)
        {
            numberLabel.text = number;
            numberLabel.transform.position = startPos + direction / 2;

            // Make sure label faces the right way
            numberLabel.transform.rotation = Quaternion.identity;
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