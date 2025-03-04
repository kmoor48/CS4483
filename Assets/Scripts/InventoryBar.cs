using UnityEngine;

public class InventoryBar : MonoBehaviour
{
    public GameObject inventoryBar;

    private GameObject[] inventorySlots;
    private int[] itemCounts; // Item counts corresponding to slots

    void Start()
    {
        int childCount = inventoryBar.transform.childCount; // Get the number of children
        inventorySlots = new GameObject[childCount];
        int i = 0;

        foreach (Transform slot in inventoryBar.transform)
        {
            inventorySlots[i] = slot.gameObject;
            i++;
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
