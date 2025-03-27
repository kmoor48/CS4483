//using UnityEngine;

//public class PickupItem : MonoBehaviour
//{
//    public string itemName;
//    public Sprite image; // The associated png image of the object that will be displayed in the inventory bar

//    private InventoryBar inventoryScript;
//    private bool playerInRange = false;
//    private GameObject universalLogicHandler;
//    private GameObject openText; // Display text for "Press [E] to PickUp"

//    //void Start()
//    //{
//    //    GameObject HUD = GameObject.FindWithTag("HUD");
//    //    openText = HUD.transform.GetChild(0).gameObject;
//    //    if (openText == null){
//    //        Debug.LogError("OpenText not found in hierarchy");
//    //    }
//    //    openText.SetActive(false);

//    //    universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

//    //    if (universalLogicHandler == null)
//    //    {
//    //        Debug.LogError("No GameObject with tag of UniversalLogicHandler");
//    //    }

//    //    // Get the inventory bar script from the logic handler game object 
//    //    inventoryScript = universalLogicHandler.GetComponent<InventoryBar>();
//    //}

//    void Start()
//    {
//        GameObject HUD = GameObject.FindWithTag("HUD");

//        if (HUD != null)
//        {
//            openText = HUD.transform.GetChild(0).gameObject;
//        }
//        else
//        {
//            Debug.LogError("HUD object not found! Check that your HUD has the correct tag.");
//        }

//        if (openText == null)
//        {
//            Debug.LogError("OpenText is not assigned in PickupItem!");
//        }
//        else
//        {
//            openText.SetActive(false);
//        }

//        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

//        if (universalLogicHandler == null)
//        {
//            Debug.LogError("No GameObject with tag UniversalLogicHandler found.");
//        }
//        else
//        {
//            inventoryScript = universalLogicHandler.GetComponent<InventoryBar>();
//        }
//    }


//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            openText.SetActive(true); // Show "Pick up object?" when near
//            playerInRange = true;
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            openText.SetActive(false); // Hide the text when player moves away
//            playerInRange = false;
//        }
//    }

//    void Update()
//    {
//        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Pickup when pressing "E"
//        {
//            Debug.Log(itemName + " picked up!");

//            if (inventoryScript != null)
//            {
//                // Add the item to the inventory
//                inventoryScript.AddItem(gameObject, itemName, image);

//                // Hide the text and remove the object
//                openText.SetActive(false);
//                Destroy(gameObject);
//            }
//            else
//            {
//                Debug.LogError("No Inventory Bar script is attached to the main logic handler");
//            }
//        }
//    }
//}

using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    public Sprite image; // The associated png image of the object that will be displayed in the inventory bar

    private InventoryBar inventoryScript;
    private bool playerInRange = false;
    private GameObject universalLogicHandler;
    private GameObject openText; // Display text for "Press [E] to PickUp"

    void Start()
    {
        // Initialize openText reference
        GameObject HUD = GameObject.FindWithTag("HUD");

        if (HUD != null)
        {
            openText = HUD.transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogError("HUD object not found! Check that your HUD has the correct tag.");
        }

        if (openText != null)
        {
            openText.SetActive(false); // Make sure it's initially hidden
        }
        else
        {
            Debug.LogError("OpenText is not assigned in PickupItem!");
        }

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

        if (universalLogicHandler == null)
        {
            Debug.LogError("No GameObject with tag UniversalLogicHandler found.");
        }
        else
        {
            inventoryScript = universalLogicHandler.GetComponent<InventoryBar>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && openText != null)
        {
            openText.SetActive(true); // Show "Pick up object?" when near
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && openText != null)
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
                if (openText != null)
                {
                    openText.SetActive(false);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No Inventory Bar script is attached to the main logic handler");
            }
        }
    }

    // You can now use this function to set openText to false when an item is removed from inventory
    public void SetOpenTextFalse()
    {
        if (openText != null)
        {
            openText.SetActive(false); // Set openText to false
        }
    }
}



//using UnityEngine;

//public class PickupItem : MonoBehaviour
//{
//    public string itemName;
//    public Sprite image; // The associated png image of the object that will be displayed in the inventory bar

//    private InventoryBar inventoryScript;
//    private bool playerInRange = false;
//    private bool isPickedUp = false; // Track whether the item has been picked up
//    private GameObject universalLogicHandler;
//    private GameObject openText; // Display text for "Press [E] to PickUp"

//    // Called when the script is enabled or re-enabled
//    void OnEnable()
//    {
//        // Ensure that the references are reset when the script is enabled
//        GameObject HUD = GameObject.FindWithTag("HUD");

//        if (HUD != null)
//        {
//            openText = HUD.transform.GetChild(0).gameObject;
//        }
//        else
//        {
//            Debug.LogError("HUD object not found! Check that your HUD has the correct tag.");
//        }

//        if (openText == null)
//        {
//            Debug.LogError("OpenText is not assigned in PickupItem!");
//        }
//        else
//        {
//            openText.SetActive(false); // Ensure it's hidden initially
//        }

//        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

//        if (universalLogicHandler == null)
//        {
//            Debug.LogError("No GameObject with tag UniversalLogicHandler found.");
//        }
//        else
//        {
//            inventoryScript = universalLogicHandler.GetComponent<InventoryBar>();
//        }
//    }

//    // Start is now left for any other initialization if needed
//    void Start()
//    {
//        // This check is a fallback in case OnEnable isn't triggered first
//        if (openText == null)
//        {
//            Debug.LogError("OpenText is not assigned in PickupItem!");
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player") && !isPickedUp) // Only show text if the item isn't picked up
//        {
//            openText.SetActive(true); // Show "Pick up object?" when near
//            playerInRange = true;
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            openText.SetActive(false); // Hide the text when player moves away
//            playerInRange = false;
//        }
//    }

//    void Update()
//    {
//        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isPickedUp) // Pickup when pressing "E"
//        {
//            Debug.Log(itemName + " picked up!");

//            if (inventoryScript != null)
//            {
//                // Add the item to the inventory
//                inventoryScript.AddItem(gameObject, itemName, image);

//                // Hide the text and mark the item as picked up
//                openText.SetActive(false);
//                isPickedUp = true; // Item is now picked up
//                Destroy(gameObject); // Remove the item from the world
//            }
//            else
//            {
//                Debug.LogError("No Inventory Bar script is attached to the main logic handler");
//            }
//        }
//    }
//}

