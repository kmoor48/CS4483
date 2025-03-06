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
        // Check to find the first open slot
        int index = Array.IndexOf(itemCounts, 0);

        // Check to see if inventory is full
        if (index == -1)
        {
            Debug.LogError("Inventory is Full!");
        }
        else
        {
            GameObject openSlot = inventorySlots[index];

            // Ensure the slot is empty (no image or label component)
            if (openSlot.transform.childCount == 0)
            {
                // Create a new GameObject for the image (child of the slot)
                GameObject imageObject = new GameObject("ItemImage");
                imageObject.transform.SetParent(openSlot.transform);

                // Add Image component
                Image imageComponent = imageObject.AddComponent<Image>();

                // Set the sprite for the new image
                imageComponent.sprite = image;

                // Set transparency
                Color currentColor = imageComponent.color;
                currentColor.a = 1.0f; // Full opacity
                imageComponent.color = currentColor;

                // Create a new label for the item (child of the slot)
                GameObject itemLabel = new GameObject("ItemLabel");
                itemLabel.transform.SetParent(openSlot.transform);

                // Add TextMeshProUGUI for the item's name
                TextMeshProUGUI itemText = itemLabel.AddComponent<TextMeshProUGUI>();
                itemText.text = passedInItemName;

                // Optionally, set font size, color, alignment, etc.
            }
            else
            {
                // If there is already a child (i.e., item exists in the slot), update the existing image and label
                Transform imageTransform = openSlot.transform.GetChild(0);
                GameObject imageOfInventorySlot = imageTransform.gameObject;
                Image imageComponent = imageOfInventorySlot.GetComponent<Image>();

                // Update the sprite for the existing image
                imageComponent.sprite = image;

                // Set transparency
                Color currentColor = imageComponent.color;
                currentColor.a = 1.0f; // Full opacity
                imageComponent.color = currentColor;

                // Update the item's label (name)
                Transform itemLabelContainer = openSlot.transform.GetChild(1);
                Transform itemLabelText = itemLabelContainer.transform.GetChild(0);
                TextMeshProUGUI itemText = itemLabelText.GetComponent<TextMeshProUGUI>();

                // Change the text
                if (itemText != null)
                {
                    itemText.text = passedInItemName;
                }
                else
                {
                    Debug.LogError("TextMeshProUGUI component not found on the item label!");
                }
            }

            // Optionally, update item counts or handle other inventory logic
            itemCounts[index]++;
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
