using UnityEngine;
using UnityEngine.EventSystems;

public class SafeInteraction : MonoBehaviour
{
    public GameObject safeCanvas; 
    public Camera playerCamera;
    public Camera safeCamera;
    public GameObject safePrefab; 
    public float interactionDistance = 3f;

    public GameObject interactionPrompt; 
    private bool isInteracting = false;
    private bool isNearSafe = false;

    void Start()
    {
        safeCanvas.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        float distanceToSafe = Vector3.Distance(playerCamera.transform.position, transform.position);

        if (distanceToSafe <= interactionDistance)
        {
            if (!isNearSafe)
            {
                isNearSafe = true;
                interactionPrompt.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.P) && !isInteracting)
            {
                EnterSafe();
            }
        }
        else
        {
            if (isNearSafe)
            {
                isNearSafe = false;
                interactionPrompt.SetActive(false);
            }
        }

        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitSafe();
        }
    }

    public void EnterSafe()
    {
        isInteracting = true;
        UIInteractionManager.Instance.EnableUIInteraction();

        // Activate UI and switch camera
        playerCamera.gameObject.SetActive(false);
        safeCamera.gameObject.SetActive(true);
        safeCanvas.SetActive(true);
        interactionPrompt.SetActive(false);
    }

    public void ExitSafe()
    {
        isInteracting = false;
        UIInteractionManager.Instance.DisableUIInteraction();

        // Hide UI and switch back to the player camera
        playerCamera.gameObject.SetActive(true);
        safeCamera.gameObject.SetActive(false);
        safeCanvas.SetActive(false);
    }
}
