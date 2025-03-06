using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryBar : MonoBehaviour
{
    public GameObject inventoryBar;

    private GameObject[] inventorySlots;
    private int[] itemCounts; // Boolean array of whether the slots are filled or not

    void Start()
    {
        int childCount = inventoryBar.transform.childCount; // Get the number of children
        inventorySlots = new GameObject[childCount];
        itemCounts = new int[childCount];
        int i = 0;

        foreach (Transform slot in inventoryBar.transform)
        {
            inventorySlots[i] = slot.gameObject;
            itemCounts[i] = 0; // Setting it's slot to empty
            i++;
        }
    }

    public void AddItem(GameObject item, string passedInItemName, Sprite image)
    {
        // Check to find first open slot
        int index = Array.IndexOf(itemCounts, 0);

        // Check to see if inventory is full
        if (index == -1)
        {
            Debug.LogError("Inventory is Full!");
        }
        else
        {
            GameObject openSlot = inventorySlots[index];

            // Ensure the slot has a child (The image component)
            if (openSlot.transform.childCount > 0)
            {
                // Retrieve the image child object
                Transform imageTransform = openSlot.transform.GetChild(0);
                GameObject imageOfInvenotrySlot = imageTransform.gameObject;
                Image imageComponent = imageOfInvenotrySlot.GetComponent<Image>();

                // Add the image to the inventory
                imageComponent.sprite = image;

                // Update the transparency
                Color currentColor = imageComponent.color;
                currentColor.a = 1.0f; // Set the new alpha value while preserving the current RGB values
                imageComponent.color = currentColor; // Apply the new color with the modified alpha

                // Update the item's label aka text component 
                Transform itemLabelContainer = openSlot.transform.GetChild(1); // Retrieve the image child object
                Transform itemLabelText = itemLabelContainer.transform.GetChild(0);
                TextMeshProUGUI itemText = itemLabelText.GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
                if (itemText != null)
                {
                    itemText.text = passedInItemName; // Change the text
                }
                else
                {
                    Debug.LogError("TextMeshProUGUI component not found on the item label!");
                }
            }
            else
            {
                Debug.LogError("No image child found on the selected Inventory Slot.");
            }
        }

    }

    public void RemoveItem()
    {
        Debug.Log("Remove an item from the inventory");
    }

    public void ClearInventory()
    {

    }
}

