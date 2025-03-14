using UnityEngine;
using UnityEngine.UI;

public class SafeLock : MonoBehaviour
{
    public Slider[] sliders; // Assign 5 sliders in the Inspector
    public int[] correctValues = { 4, 3, 2, 1, 0 }; // Correct sequence for unlocking
    public GameObject safeDoor; // Assign the safe door GameObject
    //public GameObject letterInside; // Assign the letter GameObject

    private bool isUnlocked = false;

    void Start()
    {
        //letterInside.SetActive(false); // Hide letter at the start
    }

    void Update()
    {
        if (!isUnlocked && CheckSafeCombination())
        {
            UnlockSafe();
        }
    }

    bool CheckSafeCombination()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            if ((int)sliders[i].value != correctValues[i]) // Convert slider value to int
            {
                return false; // If any slider is incorrect, return false
            }
        }
        return true; // All sliders are correct
    }

    void UnlockSafe()
    {
        isUnlocked = true;
        Debug.Log("Safe Unlocked!");

        // Play safe opening animation
        safeDoor.SetActive(false);

        // Show the letter inside
        //letterInside.SetActive(true);
    }
}

