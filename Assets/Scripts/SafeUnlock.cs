using UnityEngine;
using UnityEngine.UI;

public class SafeUnlock : MonoBehaviour
{
    public Slider slider1, slider2, slider3, slider4, slider5; // UI sliders
    public GameObject successMessage;
    public GameObject safeDoor;
    public GameObject safeCanvas;// Safe door to unlock

    private int[] correctCombination = { 2, 1, 3, 5, 4 }; // Correct slider positionss


    void Start()
    {

        // Ensure sliders move correctly & check combination on change
        slider1.wholeNumbers = true;
        slider2.wholeNumbers = true;
        slider3.wholeNumbers = true;
        slider4.wholeNumbers = true;
        slider5.wholeNumbers = true;

        slider1.minValue = 1;
        slider1.maxValue = 5;
        slider2.minValue = 1;
        slider2.maxValue = 5;
        slider3.minValue = 1;
        slider3.maxValue = 5;
        slider4.minValue = 1;
        slider4.maxValue = 5;
        slider5.minValue = 1;
        slider5.maxValue = 5;

        // Listen for slider movement
        slider1.onValueChanged.AddListener(delegate {
            Debug.Log("Slider 1 Value Changed");
            CheckCombination();
        });
        slider2.onValueChanged.AddListener(delegate {
            Debug.Log("Slider 2 Value Changed");
            CheckCombination();
        });
        slider3.onValueChanged.AddListener(delegate {
            Debug.Log("Slider 3 Value Changed");
            CheckCombination();
        });
        slider4.onValueChanged.AddListener(delegate {
            Debug.Log("Slider 4 Value Changed");
            CheckCombination();
        });
        slider5.onValueChanged.AddListener(delegate {
            Debug.Log("Slider 5 Value Changed");
            CheckCombination();
        });

        successMessage.SetActive(false);
    }
  
    void Update()
    {
        Debug.Log("Update called");
        CheckCombination();
    }

    void CheckCombination()
    {
        // Debugging: Print raw slider values (before rounding)
        Debug.Log($"Raw Slider Values: {slider1.value}, {slider2.value}, {slider3.value}, {slider4.value}, {slider5.value}");

        // Read integer values from sliders (round to nearest integer)
        int s1 = Mathf.RoundToInt(slider1.value);
        int s2 = Mathf.RoundToInt(slider2.value);
        int s3 = Mathf.RoundToInt(slider3.value);
        int s4 = Mathf.RoundToInt(slider4.value);
        int s5 = Mathf.RoundToInt(slider5.value);

        // Debugging: Print the integer slider values
        Debug.Log($"Sliders: {s1}, {s2}, {s3}, {s4}, {s5}");

        // Check if the combination is correct
        if (s1 == correctCombination[0] &&
            s2 == correctCombination[1] &&
            s3 == correctCombination[2] &&
            s4 == correctCombination[3] &&
            s5 == correctCombination[4])
        {
            successMessage.SetActive(true);
            Invoke("UnlockSafe", 2f);
        }
    }


    void UnlockSafe()
    {
        safeCanvas.SetActive(false);
        successMessage.SetActive(false);
        safeDoor.SetActive(false); // Open the safe
        Debug.Log("Safe Unlocked!");
    }
}

