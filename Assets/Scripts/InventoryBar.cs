using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryBar : MonoBehaviour
{
    private GameObject inventoryBar;

    private GameObject[] inventorySlots; // Array referencing the gameobjects for each slot on the inventory bar
    private string[] itemNames;
    private int[] itemCounts; // Tracks what items slots are currently full (=1) and empty (=0)
    private string[] itemTags; // Tracks items tag names for between level clear out inventory purposes

    private int selectedSlot = -1; // Tracks the currently selected slot

    private GameObject hoveredInventoryItem = null; // Tracks what button is being hovered over for 2D puzzles

    void Start()
    {
        inventoryBar = GameObject.FindWithTag("InventoryBar");
        if (inventoryBar == null)
        {
            Debug.LogError("Cannot find inventory bar");
        }

        int childCount = inventoryBar.transform.childCount;
        inventorySlots = new GameObject[childCount];
        itemNames = new string[childCount];
        itemTags = new string[childCount];
        itemCounts = new int[childCount];

        for (int i = 0; i < childCount; i++)
        {
            inventorySlots[i] = inventoryBar.transform.GetChild(i).gameObject;
            itemNames[i] = null;
            itemTags[i] = null;
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
        itemTags[index] = item.tag;
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
            itemTags[index] = null;
            itemCounts[index] = 0;

            GameObject slot = inventorySlots[index];
            Transform imageTransform = slot.transform.GetChild(0);
            Image imageComponent = imageTransform.GetComponent<Image>();
            imageComponent.sprite = null;
            imageComponent.color = new Color(1, 1, 1, 0);

            Transform textTransform = slot.transform.GetChild(1).GetChild(0);
            TextMeshProUGUI itemText = textTransform.GetComponent<TextMeshProUGUI>();
            itemText.text = "";
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

    public void SetHoverState(GameObject itemLabelText)
    {
        hoveredInventoryItem = itemLabelText;
    }

    public void UnsetHoverState()
    {
        hoveredInventoryItem = null;
    }

    public GameObject CheckHoverState()
    {
        return hoveredInventoryItem;
    }
    
    public void ClearInventoryBetweenLevels(int levelJustFinishedIndex)
    {
        string levelName = "Level" + (levelJustFinishedIndex + 1).ToString();
        
        for (int i = 0; i < itemTags.Length ; i++)
        {
            if (itemTags[i] == levelName)
            {
                RemoveItem(itemNames[i]); // remove if the item is only needed in the previous level
            }
        }
    }
}

