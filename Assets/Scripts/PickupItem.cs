using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    public GameObject openText;

    private bool playerInRange = false;

    void Start()
    {
        if (openText != null)
        {
            openText.SetActive(false);
        }
        else
        {
            Debug.LogWarning("openText is not assigned in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openText != null)
            {
                openText.SetActive(true);
            }
            playerInRange = true;
            Debug.Log("Player entered pickup range of " + itemName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openText != null)
            {
                openText.SetActive(false);
            }
            playerInRange = false;
            Debug.Log("Player left pickup range of " + itemName);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed! Attempting to pick up: " + itemName);

            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(itemName);
            }
            else
            {
                Debug.LogError("InventoryManager.Instance is NULL! Ensure the InventoryManager is in the scene.");
            }

            if (openText != null)
            {
                openText.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}
