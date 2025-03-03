using UnityEngine;

public class InventoryBar : MonoBehaviour
{
    public GameObject inventoryBar;

    private GameObject[] inventorySlots; // References to the item slots themselves
    private int[] itemCounts; // Item counts corresponding to slots

    void Start()
    {
        inventorySlots = GameObject.FindGameObjectsWithTag("InventoryBox");
        
        itemCounts = new int[inventorySlots.Length];
        for (int i = 0; i < itemCounts.Length; i++)
        {
            itemCounts[i] = 0; // Initialize all slots as empty
        }
    }

    public void AddItem()
    {
        Debug.Log("Add an item to the inventory");
    }

    public void RemoveItem()
    {
        Debug.Log("Remove an item from the inventory");
    }

    public void ClearInventory()
    {

    }
}
