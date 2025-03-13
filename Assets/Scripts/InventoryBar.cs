using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryBar : MonoBehaviour
{
    public GameObject inventoryBar;

    private GameObject[] inventorySlots; // Array referencing the gameobjects for each slot on the inventory bar
    private string[] itemNames;
    private int[] itemCounts; // Tracks what items slots are currently full (=1) and empty (=0)

    private int selectedSlot = -1; // Tracks the currently selected slot

    void Start()
    {
        int childCount = inventoryBar.transform.childCount;
        inventorySlots = new GameObject[childCount];
        itemNames = new string[childCount];
        itemCounts = new int[childCount];

        for (int i = 0; i < childCount; i++)
        {
            inventorySlots[i] = inventoryBar.transform.GetChild(i).gameObject;
            itemNames[i] = null;
            itemCounts[i] = 0;
        }
    }

    public void AddItem(GameObject item, string passedInItemName, Sprite image)
    {
        int index = Array.IndexOf(itemCounts, 0);
        if (index == -1)
        {
            Debug.LogError("Inventory is Full!");
            return;
        }

        GameObject openSlot = inventorySlots[index];
        itemNames[index] = passedInItemName;
        itemCounts[index] = 1; // Marking the item as full

        if (openSlot.transform.childCount > 0)
        {
            Transform imageTransform = openSlot.transform.GetChild(0);
            Image imageComponent = imageTransform.GetComponent<Image>();
            imageComponent.sprite = image;
            imageComponent.color = new Color(1, 1, 1, 1);

            Transform textTransform = openSlot.transform.GetChild(1).GetChild(0);
            TextMeshProUGUI itemText = textTransform.GetComponent<TextMeshProUGUI>();
            itemText.text = passedInItemName;
        }
    }

    public bool HasItem(string itemName)
    {
        return Array.Exists(itemNames, item => item == itemName);
    }

    public void RemoveItem(string itemName)
    {
        int index = Array.IndexOf(itemNames, itemName);
        if (index != -1)
        {
            itemNames[index] = null;
            itemCounts[index] = 0;

            GameObject slot = inventorySlots[index];
            Transform imageTransform = slot.transform.GetChild(0);
            Image imageComponent = imageTransform.GetComponent<Image>();
            imageComponent.sprite = null;
            imageComponent.color = new Color(1, 1, 1, 0);

            Transform textTransform = slot.transform.GetChild(1).GetChild(0);
            TextMeshProUGUI itemText = textTransform.GetComponent<TextMeshProUGUI>();
            itemText.text = "";

            Debug.Log(itemName + " removed from inventory.");
        }
    }

    void Update()
    {
        // Detect number key press (1-9) to select an inventory slot
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlot = i;
                Debug.Log("Selected Slot: " + (selectedSlot + 1));
            }
        }

        // Remove item only if a slot is selected and 'T' is pressed
        /*if (selectedSlot != -1 && Input.GetKeyDown(KeyCode.T))
        {
            if (itemNames[selectedSlot] != null)
            {
                Debug.Log("clearing inventory");
                RemoveItem(itemNames[selectedSlot]);
                itemCounts[selectedSlot] = 0; // Mark the inventory slot as empty again
                selectedSlot = -1; // Reset selection after removal
            }
            else
            {
                Debug.Log("No item in the selected slot.");
            }
        }*/
    }
}

