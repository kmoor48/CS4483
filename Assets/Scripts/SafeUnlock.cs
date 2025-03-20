using UnityEngine;
using UnityEngine.UI;

public class SafeUnlock : MonoBehaviour
{
    public Camera playerCamera;
    public Camera safeCamera;
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
     
            CheckCombination();
        });
        slider2.onValueChanged.AddListener(delegate {
       
            CheckCombination();
        });
        slider3.onValueChanged.AddListener(delegate {
        
            CheckCombination();
        });
        slider4.onValueChanged.AddListener(delegate {
          
            CheckCombination();
        });
        slider5.onValueChanged.AddListener(delegate {
         
            CheckCombination();
        });

        successMessage.SetActive(false);
    }
  
    void Update()
    {
     
        CheckCombination();
    }

    void CheckCombination()
    {
       

        // Read integer values from sliders (round to nearest integer)
        int s1 = Mathf.RoundToInt(slider1.value);
        int s2 = Mathf.RoundToInt(slider2.value);
        int s3 = Mathf.RoundToInt(slider3.value);
        int s4 = Mathf.RoundToInt(slider4.value);
        int s5 = Mathf.RoundToInt(slider5.value);


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

        // Switch cameras back to the player view
        playerCamera.gameObject.SetActive(true);
        safeCamera.gameObject.SetActive(false);

        Debug.Log("Safe Unlocked! Returning to player camera.");
    }


}

