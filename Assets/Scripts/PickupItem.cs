<<<<<<< HEAD
=======
//using UnityEngine;

//public class ItemPickup : MonoBehaviour
//{
//    public string itemName;

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Picked up " + itemName);
//            InventoryManager.Instance.AddItem(itemName);
//            Destroy(gameObject);
//        }
//    }
//}

>>>>>>> 8275135 (add puzzle with foundation and text)
using UnityEngine;

public class PickupItem : MonoBehaviour
{
<<<<<<< HEAD
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
            openText.SetActive(true); 
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText.SetActive(false); 
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log(itemName + " picked up!");

            InventoryManager.Instance.AddItem(itemName);

            openText.SetActive(false);
=======
    public string itemName; // Set this to "Foundation Powder" in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player picks up the item
        {
            Debug.Log(itemName + " picked up!");

            // Add the item to the inventory
            InventoryManager.Instance.AddItem(itemName);

            // Destroy the item from the scene after pickup
>>>>>>> 8275135 (add puzzle with foundation and text)
            Destroy(gameObject);
        }
    }
}



