using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName; 
    public GameObject openText;
    public Sprite image; // The associated png image of the object that will be displayed in the inventory bar

    private InventoryBar inventoryScript;
    private bool playerInRange = false;
    private GameObject universalLogicHandler;

    void Start()
    {
        openText.SetActive(false);
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

        if (universalLogicHandler == null)
        {
            Debug.LogError("No GameObject with tag of UniversalLogicHandler");
        }

        // Get the inventory bar script from the logic handler game object 
        inventoryScript = universalLogicHandler.GetComponent<InventoryBar>();
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

            if (inventoryScript != null)
            {
                // Add the item to the inventory
                inventoryScript.AddItem(gameObject, itemName, image);

                // Hide the text and remove the object
                openText.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No Inventory Bar script is attached to the main logic handler");
            }
        }
    }
}



