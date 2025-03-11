//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class InventoryDisplay : MonoBehaviour
//{
//    public GameObject inventoryBar; // Reference to the inventory bar UI
//    public GameObject itemDisplayPanel; // Reference to the panel for displaying the selected item
//    public Image itemDisplayImage; // Reference to the Image component for displaying the item's sprite
//    public TextMeshProUGUI itemDisplayText; // Reference to the TextMeshProUGUI for displaying the item's name

//    private bool isInventoryOpen = false; // Track whether the inventory is open

//    private void Start()
//    {
//        if (itemDisplayPanel != null)
//        {
//            itemDisplayPanel.SetActive(false); // Ensure the display panel is hidden at the start
//        }
//    }

//    private void Update()
//    {
//        // Check for number key presses (1-9) to select an inventory slot
//        for (int i = 0; i < 9; i++)
//        {
//            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
//            {
//                DisplayItem(i);
//            }
//        }

//        // Hide the display when 'Esc' or 'T' is pressed
//        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T))
//        {
//            HideItemDisplay();
//        }

//        // Freeze/unfreeze the game based on inventory state
//        if (isInventoryOpen)
//        {
//            FreezeGame();
//        }
//        else
//        {
//            UnfreezeGame();
//        }
//    }

//    // Function to display the selected item
//    private void DisplayItem(int slotIndex)
//    {
//        if (inventoryBar != null && slotIndex >= 0 && slotIndex < inventoryBar.transform.childCount)
//        {
//            Transform slot = inventoryBar.transform.GetChild(slotIndex);

//            if (slot.childCount > 0)
//            {
//                Transform imageTransform = slot.GetChild(0); // Get item image
//                Transform textTransform = slot.GetChild(1); // Get item name

//                if (imageTransform != null && textTransform != null)
//                {
//                    Image itemImage = imageTransform.GetComponent<Image>();
//                    TextMeshProUGUI itemText = textTransform.GetComponentInChildren<TextMeshProUGUI>();

//                    if (itemImage != null && itemText != null)
//                    {
//                        if (itemDisplayImage != null)
//                        {
//                            itemDisplayImage.sprite = itemImage.sprite;
//                            itemDisplayImage.enabled = true;
//                        }

//                        if (itemDisplayText != null)
//                        {
//                            itemDisplayText.text = itemText.text;
//                        }

//                        if (itemDisplayPanel != null)
//                        {
//                            itemDisplayPanel.SetActive(true);
//                        }

//                        isInventoryOpen = true;
//                    }
//                }
//            }
//        }
//    }

//    // Function to hide the item display
//    private void HideItemDisplay()
//    {
//        if (itemDisplayPanel != null)
//        {
//            itemDisplayPanel.SetActive(false);
//        }

//        if (itemDisplayImage != null)
//        {
//            itemDisplayImage.enabled = false;
//        }

//        if (itemDisplayText != null)
//        {
//            itemDisplayText.text = "";
//        }

//        isInventoryOpen = false;
//    }

//    // Function to freeze the game
//    private void FreezeGame()
//    {
//        Time.timeScale = 0;
//        PlayerController playerController = FindObjectOfType<PlayerController>();
//        if (playerController != null)
//        {
//            playerController.enabled = false;
//        }
//    }

//    // Function to unfreeze the game
//    private void UnfreezeGame()
//    {
//        Time.timeScale = 1;
//        PlayerController playerController = FindObjectOfType<PlayerController>();
//        if (playerController != null)
//        {
//            playerController.enabled = true;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryBar; // Reference to the inventory bar UI
    public GameObject itemDisplayPanel; // Reference to the panel for displaying the selected item
    public Image itemDisplayImage; // Reference to the Image component for displaying the item's sprite
    public TextMeshProUGUI itemDisplayText; // Reference to the TextMeshProUGUI for displaying the item's name
    public Transform playerHand; // Reference to the player's hand transform where the item will be placed

    private bool isInventoryOpen = false; // Track whether the inventory is open
    private int selectedSlotIndex = -1; // Track which slot is selected

    private void Start()
    {
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false); // Ensure the display panel is hidden at the start
        }
    }

    private void Update()
    {
        // Check for number key presses (1-9) to select an inventory slot
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                DisplayItem(i);
            }
        }

        // Remove item and add it to player's hand when 'T' is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            RemoveItemFromInventory();
        }

        // Hide the display when 'Esc' is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideItemDisplay();
        }

        // Freeze/unfreeze the game based on inventory state
        if (isInventoryOpen)
        {
            FreezeGame();
        }
        else
        {
            UnfreezeGame();
        }
    }

    // Function to display the selected item
    private void DisplayItem(int slotIndex)
    {
        if (inventoryBar != null && slotIndex >= 0 && slotIndex < inventoryBar.transform.childCount)
        {
            Transform slot = inventoryBar.transform.GetChild(slotIndex);

            if (slot.childCount > 0)
            {
                Transform imageTransform = slot.GetChild(0); // Get item image
                Transform textTransform = slot.GetChild(1); // Get item name

                if (imageTransform != null && textTransform != null)
                {
                    Image itemImage = imageTransform.GetComponent<Image>();
                    TextMeshProUGUI itemText = textTransform.GetComponentInChildren<TextMeshProUGUI>();

                    if (itemImage != null && itemText != null)
                    {
                        if (itemDisplayImage != null)
                        {
                            itemDisplayImage.sprite = itemImage.sprite;
                            itemDisplayImage.enabled = true;
                        }

                        if (itemDisplayText != null)
                        {
                            itemDisplayText.text = itemText.text;
                        }

                        if (itemDisplayPanel != null)
                        {
                            itemDisplayPanel.SetActive(true);
                        }

                        selectedSlotIndex = slotIndex; // Store selected slot index
                        isInventoryOpen = true;
                    }
                }
            }
        }
    }

    // Function to remove an item from inventory and add it to the player's hand
    //private void RemoveItemFromInventory()
    //{
    //    if (selectedSlotIndex == -1) return; // No item selected

    //    Transform selectedSlot = inventoryBar.transform.GetChild(selectedSlotIndex);

    //    if (selectedSlot.childCount > 0)
    //    {
    //        Transform imageTransform = selectedSlot.GetChild(0); // Get item image
    //        Transform textTransform = selectedSlot.GetChild(1); // Get item name

    //        string itemName = textTransform.GetComponentInChildren<TextMeshProUGUI>().text;
    //        Sprite itemSprite = imageTransform.GetComponent<Image>().sprite;

    //        // Remove the item from the inventory UI
    //        Destroy(imageTransform.gameObject);
    //        Destroy(textTransform.gameObject);

    //        // Spawn item in player's hand
    //        GameObject itemPrefab = Resources.Load<GameObject>("Items/" + itemName); // Ensure prefab exists in Resources/Items folder
    //        if (itemPrefab != null)
    //        {
    //            GameObject newItem = Instantiate(itemPrefab, playerHand.position, Quaternion.identity);
    //            newItem.transform.SetParent(playerHand); // Attach to player's hand
    //        }
    //        else
    //        {
    //            Debug.LogError("Item prefab not found in Resources/Items: " + itemName);
    //        }

    //        selectedSlotIndex = -1; // Reset selected slot
    //        HideItemDisplay(); // Close inventory display
    //    }
    //}

    private void RemoveItemFromInventory()
    {
        if (selectedSlotIndex == -1) return; // No item selected

        Transform selectedSlot = inventoryBar.transform.GetChild(selectedSlotIndex);

        if (selectedSlot.childCount > 0)
        {
            Transform imageTransform = selectedSlot.GetChild(0); // Get item image
            Transform textTransform = selectedSlot.GetChild(1); // Get item name

            string itemName = textTransform.GetComponentInChildren<TextMeshProUGUI>().text;
            Sprite itemSprite = imageTransform.GetComponent<Image>().sprite;

            // Remove the item from the inventory UI
            Destroy(imageTransform.gameObject);
            Destroy(textTransform.gameObject);

            // Load item prefab from Resources/Items folder
            GameObject itemPrefab = Resources.Load<GameObject>("Items/" + itemName); // Ensure prefab exists in Resources/Items folder
            if (itemPrefab != null)
            {
                // Instantiate the item at the player's hand position
                GameObject newItem = Instantiate(itemPrefab, playerHand.position, Quaternion.identity);

                // Attach to the player's hand
                newItem.transform.SetParent(playerHand);

                // Adjust the scale based on the hand's scale or set a fixed scale
                newItem.transform.localScale = playerHand.localScale * 0.2f;  // Scale down if hand is larger
                Vector3 offset = new Vector3(0.05f, 0.2f, 0.2f);  // Adjust the offset values as needed
                newItem.transform.localPosition += offset;

            }
            else
            {
                Debug.LogError("Item prefab not found in Resources/Items: " + itemName);
            }

            selectedSlotIndex = -1; // Reset selected slot
            HideItemDisplay(); // Close inventory display
        }
    }


    // Function to hide the item display
    private void HideItemDisplay()
    {
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
        }

        if (itemDisplayImage != null)
        {
            itemDisplayImage.enabled = false;
        }

        if (itemDisplayText != null)
        {
            itemDisplayText.text = "";
        }

        selectedSlotIndex = -1;
        isInventoryOpen = false;
    }

    // Function to freeze the game
    private void FreezeGame()
    {
        Time.timeScale = 0;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    // Function to unfreeze the game
    private void UnfreezeGame()
    {
        Time.timeScale = 1;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}

