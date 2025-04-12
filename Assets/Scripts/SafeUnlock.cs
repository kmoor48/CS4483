using UnityEngine;
using UnityEngine.UI;

public class SafeUnlock : MonoBehaviour
{
    public Camera playerCamera;
    public Camera safeCamera;
    public Slider slider1, slider2, slider3, slider4, slider5;
    public GameObject successMessage;
    public GameObject safeDoor;
    public GameObject safeCanvas;

    private int[] correctCombination = { 2, 1, 3, 5, 4 };
    private GameObject universalLogicHandler;
    private bool hasUnlocked = false;

    void Start()
    {
        // Dynamically find the player's camera (child named "Camera" under Player)
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Transform camTransform = player.transform.Find("Camera");
            if (camTransform != null)
            {
                playerCamera = camTransform.GetComponent<Camera>();
            }
            else
            {
                Debug.LogError("Camera child not found under Player.");
            }
        }
        else
        {
            Debug.LogError("Player object not found.");
        }

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

        // Set slider limits and whole numbers
        Slider[] sliders = { slider1, slider2, slider3, slider4, slider5 };
        foreach (Slider s in sliders)
        {
            s.wholeNumbers = true;
            s.minValue = 1;
            s.maxValue = 5;
            s.onValueChanged.AddListener(delegate { CheckCombination(); });
        }

        successMessage.SetActive(false);
    }

    void Update()
    {
        if (!hasUnlocked)
            CheckCombination();
    }

    void CheckCombination()
    {
        int s1 = Mathf.RoundToInt(slider1.value);
        int s2 = Mathf.RoundToInt(slider2.value);
        int s3 = Mathf.RoundToInt(slider3.value);
        int s4 = Mathf.RoundToInt(slider4.value);
        int s5 = Mathf.RoundToInt(slider5.value);

        if (s1 == correctCombination[0] &&
            s2 == correctCombination[1] &&
            s3 == correctCombination[2] &&
            s4 == correctCombination[3] &&
            s5 == correctCombination[4] &&
            !hasUnlocked)
        {
            hasUnlocked = true;
            successMessage.SetActive(true);
            Invoke("UnlockSafe", 2f);
        }
    }

    void UnlockSafe()
    {
        safeCanvas.SetActive(false);
        successMessage.SetActive(false);
        safeDoor.SetActive(false);

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(true);

        if (safeCamera != null)
            safeCamera.gameObject.SetActive(false);

        LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        if (clueScript != null)
            clueScript.IncrementPuzzleCounter();
    }
}
