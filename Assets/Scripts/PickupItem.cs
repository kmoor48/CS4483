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

using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName; // Set this to "Foundation Powder" in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player picks up the item
        {
            Debug.Log(itemName + " picked up!");

            // Add the item to the inventory
            InventoryManager.Instance.AddItem(itemName);

            // Destroy the item from the scene after pickup
            Destroy(gameObject);
        }
    }
}



