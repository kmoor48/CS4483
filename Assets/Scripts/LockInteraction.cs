using UnityEngine;
using TMPro;

public class LockInteraction : MonoBehaviour
{
    public GameObject lockCanvas; 
    public Camera playerCamera;
    public Camera lockCamera;
    public GameObject lockPrefab;
    public GameObject successMessage; 
    public GameObject closetDoorRight;
    public GameObject closetDoorLeft;
    public TMP_Text displayText; 
    public string correctCode = "8401"; 
    public float interactionDistance = 3f; 

    public GameObject interactionPrompt; 

    private string enteredCode = ""; 
    private bool isInteracting = false;
    private bool isNearLock = false; 

    void Start()
    {
        lockCanvas.SetActive(false);
        successMessage.SetActive(false); 
        interactionPrompt.SetActive(false); 
    }

    void Update()
    {
        float distanceToLock = Vector3.Distance(playerCamera.transform.position, transform.position);
        if (distanceToLock <= interactionDistance)
        {
            if (!isNearLock)
            {
                isNearLock = true;
                interactionPrompt.SetActive(true); 
            }

            if (Input.GetKeyDown(KeyCode.P) && !isInteracting)
            {
                EnterLock(); 
            }
        }
        else
        {
            if (isNearLock)
            {
                isNearLock = false;
                interactionPrompt.SetActive(false); 
            }
        }

        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLock(); 
        }
    }

    public void EnterLock()
    {
        isInteracting = true;
        UIInteractionManager.Instance.EnableUIInteraction(); 

        playerCamera.gameObject.SetActive(false);
        lockCamera.gameObject.SetActive(true);
        lockCanvas.SetActive(true);
        interactionPrompt.SetActive(false);
    }

    public void ExitLock()
    {
        isInteracting = false;
        UIInteractionManager.Instance.DisableUIInteraction(); 
        playerCamera.gameObject.SetActive(true);
        lockCamera.gameObject.SetActive(false);
        lockCanvas.SetActive(false);
        successMessage.SetActive(false); 
        enteredCode = ""; 
        displayText.text = ""; 
    }

    public void AddDigit(string digit)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += digit;
            displayText.text = enteredCode;
        }
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            successMessage.SetActive(true); 
            Invoke("UnlockLock", 2f); 
        }
        else
        {
            enteredCode = ""; 
            displayText.text = "Incorrect"; 
            Invoke("ClearDisplay", 1.5f);
        }
    }

    void UnlockLock()
    {
        successMessage.SetActive(false); 
        ExitLock(); 
        lockPrefab.SetActive(false); 
        closetDoorLeft.SetActive(false);
        closetDoorRight.SetActive(false);
    }

    void ClearDisplay()
    {
        displayText.text = ""; 
    }
}
