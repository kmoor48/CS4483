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

        // Hide the display when 'Esc' or 'T' is pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T))
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

                        isInventoryOpen = true;
                    }
                }
            }
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
