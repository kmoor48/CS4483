using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName; 
    public GameObject openText; 

    private bool playerInRange = false; 

    void Start()
    {
        openText.SetActive(false); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText.SetActive(true); // Show "Pick up object?" when near
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText.SetActive(false); // Hide the text when player moves away
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Pickup when pressing "E"
        {
            Debug.Log(itemName + " picked up!");

            // Add the item to the inventory
            InventoryManager.Instance.AddItem(itemName);

            // Hide the text and remove the object
            openText.SetActive(false);
            Destroy(gameObject);
        }
    }
}



