//using UnityEngine;
//using UnityEngine.UI;

//public class SafeLock : MonoBehaviour
//{
//    public Slider[] sliders; // Assign 5 sliders in the Inspector
//    public int[] correctValues = { 4, 3, 2, 1, 0 }; // Correct sequence for unlocking
//    public GameObject safeDoor; // Assign the safe door GameObject
//    //public GameObject letterInside; // Assign the letter GameObject

//    private bool isUnlocked = false;

//    void Start()
//    {
//        //letterInside.SetActive(false); // Hide letter at the start
//    }

//    void Update()
//    {
//        if (!isUnlocked && CheckSafeCombination())
//        {
//            UnlockSafe();
//        }
//    }

//    bool CheckSafeCombination()
//    {
//        for (int i = 0; i < sliders.Length; i++)
//        {
//            if ((int)sliders[i].value != correctValues[i]) // Convert slider value to int
//            {
//                return false; // If any slider is incorrect, return false
//            }
//        }
//        return true; // All sliders are correct
//    }

//    void UnlockSafe()
//    {
//        isUnlocked = true;
//        Debug.Log("Safe Unlocked!");

//        // Play safe opening animation
//        safeDoor.SetActive(false);

//        // Show the letter inside
//        //letterInside.SetActive(true);
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;

//public class SafeUnlock : MonoBehaviour
//{
//    public Slider slider1;
//    public Slider slider2;
//    public Slider slider3;
//    public Slider slider4;
//    public Slider slider5;
//    public GameObject safeDoor;  // Reference to the safe door GameObject
//    private bool isUnlocked = false; // Track if safe is unlocked

//    void Start()
//    {
//        // Add event listeners to detect when sliders change
//        slider1.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider2.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider3.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider4.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider5.onValueChanged.AddListener(delegate { CheckCombination(); });
//    }

//    void CheckCombination()
//    {
//        // Check if all sliders are set to the correct values
//        if (slider1.value == 2 &&
//            slider2.value == 1 &&
//            slider3.value == 3 &&
//            slider4.value == 5 &&
//            slider5.value == 4)
//        {
//            if (!isUnlocked) // Only unlock once
//            {
//                UnlockSafe();
//            }
//        }
//    }

//    void UnlockSafe()
//    {
//        isUnlocked = true; // Prevent multiple unlocks
//        Debug.Log("Safe Unlocked!");

//        // Example: Move the door or deactivate it
//        safeDoor.SetActive(false);
//        // OR disable door
//        // safeDoor.SetActive(false);

//        // Optional: Play a sound effect when unlocking
//        // AudioSource.PlayClipAtPoint(unlockSound, transform.position);
//    }
//}


//using UnityEngine;
//using UnityEngine.UI;

//public class SafeUnlock : MonoBehaviour
//{
//    public Slider slider1, slider2, slider3, slider4, slider5;
//    public GameObject safeDoor;  // The door GameObject to unlock
//    private bool isUnlocked = false;

//    void Start()
//    {
//        // Listen for changes in any of the sliders
//        slider1.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider2.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider3.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider4.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider5.onValueChanged.AddListener(delegate { CheckCombination(); });

//    }

//    void CheckCombination()
//    {
//        // Ensure the correct values are set
//        if (slider1.value == 2 && slider2.value == 1 &&
//            slider3.value == 3 && slider4.value == 5 && slider5.value == 4)
//        {
//            if (!isUnlocked) // Only unlock once
//            {
//                UnlockSafe();
//            }
//        }
//    }

//    void UnlockSafe()
//    {
//        isUnlocked = true;
//        Debug.Log("Safe Unlocked!");

//        // Move the door to open it
//        safeDoor.SetActive(false);// Adjust as needed
//    }
//}


//using UnityEngine;
//using UnityEngine.UI; // Required for UI elements
//using TMPro;

//public class SafeUnlock : MonoBehaviour
//{
//    public Slider slider1, slider2, slider3, slider4, slider5;
//    public GameObject successMessage; // Display message when unlocked
//    public GameObject safeDoor; // The safe door object to open

//    private int[] correctCombination = { 2, 1, 3, 5, 4 }; // Correct values

//    void Start()
//    {
//        // Listen for slider value changes
//        slider1.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider2.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider3.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider4.onValueChanged.AddListener(delegate { CheckCombination(); });
//        slider5.onValueChanged.AddListener(delegate { CheckCombination(); });

//        successMessage.SetActive(false); // Hide success message initially
//    }

//    void CheckCombination()
//    {
//        // Get current slider values
//        int s1 = Mathf.RoundToInt(slider1.value);
//        int s2 = Mathf.RoundToInt(slider2.value);
//        int s3 = Mathf.RoundToInt(slider3.value);
//        int s4 = Mathf.RoundToInt(slider4.value);
//        int s5 = Mathf.RoundToInt(slider5.value);

//        if (s1 == correctCombination[0] &&
//            s2 == correctCombination[1] &&
//            s3 == correctCombination[2] &&
//            s4 == correctCombination[3] &&
//            s5 == correctCombination[4])
//        {
//            successMessage.SetActive(true);
//            Invoke("UnlockSafe", 2f); // Delay unlocking the safe
//        }
//    }

//    void UnlockSafe()
//    {
//        successMessage.SetActive(false);
//        safeDoor.SetActive(false); // "Unlock" the safe by hiding the door
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class SafeUnlock : MonoBehaviour
{
    public Slider slider1, slider2, slider3, slider4, slider5; // UI sliders
    public GameObject safeDoor; // Safe door to unlock

    private int[] correctCombination = { 2, 1, 3, 5, 4 }; // Correct slider positions

    void Start()
    {
        // Ensure sliders move correctly & check combination on change
        slider1.wholeNumbers = true;
        slider2.wholeNumbers = true;
        slider3.wholeNumbers = true;
        slider4.wholeNumbers = true;
        slider5.wholeNumbers = true;

        // Listen for slider movement
        slider1.onValueChanged.AddListener(delegate { CheckCombination(); });
        slider2.onValueChanged.AddListener(delegate { CheckCombination(); });
        slider3.onValueChanged.AddListener(delegate { CheckCombination(); });
        slider4.onValueChanged.AddListener(delegate { CheckCombination(); });
        slider5.onValueChanged.AddListener(delegate { CheckCombination(); });

    }

    void CheckCombination()
    {
        // Read integer values from sliders
        int s1 = Mathf.RoundToInt(slider1.value);
        int s2 = Mathf.RoundToInt(slider2.value);
        int s3 = Mathf.RoundToInt(slider3.value);
        int s4 = Mathf.RoundToInt(slider4.value);
        int s5 = Mathf.RoundToInt(slider5.value);

        // Debugging: Print current slider values
        Debug.Log($"Sliders: {s1}, {s2}, {s3}, {s4}, {s5}");

        // Check if the combination is correct
        if (s1 == correctCombination[0] &&
            s2 == correctCombination[1] &&
            s3 == correctCombination[2] &&
            s4 == correctCombination[3] &&
            s5 == correctCombination[4])
        {
            Invoke("UnlockSafe", 2f);
        }
    }

    void UnlockSafe()
    {
        safeDoor.SetActive(false); // Open the safe
        Debug.Log("Safe Unlocked!");
    }
}

//using UnityEngine;
//using UnityEngine.UI;

//public class SafeUnlock : MonoBehaviour
//{
//    public Slider slider1, slider2, slider3, slider4, slider5; // UI sliders
//    public Transform handle1, handle2, handle3, handle4, handle5; // Slider handles
//    public GameObject safeDoor; // Safe door to unlock

//    private int[] correctCombination = { 2, 1, 3, 5, 4 }; // Correct slider positions
//    private float handleStartZ = 0f; // Default Z position

//    void Start()
//    {
//        // Store the initial Z position of the handles
//        handleStartZ = handle1.position.z;

//        // Ensure sliders move correctly & update handle position
//        slider1.onValueChanged.AddListener(delegate { MoveHandle(slider1, handle1); CheckCombination(); });
//        slider2.onValueChanged.AddListener(delegate { MoveHandle(slider2, handle2); CheckCombination(); });
//        slider3.onValueChanged.AddListener(delegate { MoveHandle(slider3, handle3); CheckCombination(); });
//        slider4.onValueChanged.AddListener(delegate { MoveHandle(slider4, handle4); CheckCombination(); });
//        slider5.onValueChanged.AddListener(delegate { MoveHandle(slider5, handle5); CheckCombination(); });
//    }

//    void MoveHandle(Slider slider, Transform handle)
//    {
//        // Move the handle on the Z-axis while keeping X and Y the same
//        Vector3 newPosition = handle.position;
//        newPosition.z = handleStartZ + slider.value * 0.1f; // Adjust sensitivity if needed
//        handle.position = newPosition;
//    }

//    void CheckCombination()
//    {
//        // Read integer values from sliders
//        int s1 = Mathf.RoundToInt(slider1.value);
//        int s2 = Mathf.RoundToInt(slider2.value);
//        int s3 = Mathf.RoundToInt(slider3.value);
//        int s4 = Mathf.RoundToInt(slider4.value);
//        int s5 = Mathf.RoundToInt(slider5.value);

//        // Debugging: Print current slider values
//        Debug.Log($"Sliders: {s1}, {s2}, {s3}, {s4}, {s5}");

//        // Check if the combination is correct
//        if (s1 == correctCombination[0] &&
//            s2 == correctCombination[1] &&
//            s3 == correctCombination[2] &&
//            s4 == correctCombination[3] &&
//            s5 == correctCombination[4])
//        {
//            Invoke("UnlockSafe", 2f);
//        }
//    }

//    void UnlockSafe()
//    {
//        safeDoor.SetActive(false); // Open the safe
//        Debug.Log("Safe Unlocked!");
//    }
//}

