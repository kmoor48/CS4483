using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryBar; // Reference to the inventory bar UI
    public GameObject itemDisplayPanel; // Reference to the panel for displaying the selected item
    public Image itemDisplayImage; // Reference to the Image component for displaying the item's sprite
    public TextMeshProUGUI itemDisplayText; // Reference to the TextMeshProUGUI for displaying the item's name

    private bool isInventoryOpen = false; // Track whether the inventory is open

    private void Start()
    {
        // Ensure the display panel is hidden at the start
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for number key presses (1-9)
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Alpha1 is the key for '1'
            {
                DisplayItem(i);
            }
        }

        // Check for Esc key to hide the display
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
        // Check if the slot index is valid
        if (inventoryBar != null && slotIndex >= 0 && slotIndex < inventoryBar.transform.childCount)
        {
            // Get the slot at the specified index
            Transform slot = inventoryBar.transform.GetChild(slotIndex);

            // Check if the slot has an item (has at least one child)
            if (slot.childCount > 0)
            {
                // Get the item's sprite and name from the slot
                Transform imageTransform = slot.GetChild(0); // Assuming the image is the first child
                Transform textTransform = slot.GetChild(1); // Assuming the text is the second child

                if (imageTransform != null && textTransform != null)
                {
                    // Get the Image and TextMeshProUGUI components
                    Image itemImage = imageTransform.GetComponent<Image>();
                    TextMeshProUGUI itemText = textTransform.GetComponentInChildren<TextMeshProUGUI>();

                    if (itemImage != null && itemText != null)
                    {
                        // Update the display panel with the item's sprite and name
                        if (itemDisplayImage != null)
                        {
                            itemDisplayImage.sprite = itemImage.sprite; // Set the sprite
                            itemDisplayImage.enabled = true; // Ensure the image is visible
                        }

                        if (itemDisplayText != null)
                        {
                            itemDisplayText.text = itemText.text;
                        }

                        // Show the display panel
                        if (itemDisplayPanel != null)
                        {
                            itemDisplayPanel.SetActive(true);
                        }

                        // Set inventory state to open
                        isInventoryOpen = true;
                    }
                }
            }
        }
    }

    // Function to hide the item display
    private void HideItemDisplay()
    {
        // Hide the display panel
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
        }

        // Optionally, clear the image and text
        if (itemDisplayImage != null)
        {
            itemDisplayImage.enabled = false;
        }

        if (itemDisplayText != null)
        {
            itemDisplayText.text = "";
        }

        // Set inventory state to closed
        isInventoryOpen = false;
    }

    // Function to freeze the game
    private void FreezeGame()
    {
        // Stop time (freeze animations, physics, etc.)
        Time.timeScale = 0;

        // Disable player movement (assuming you have a PlayerController script)
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    // Function to unfreeze the game
    private void UnfreezeGame()
    {
        // Resume time
        Time.timeScale = 1;

        // Enable player movement
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}