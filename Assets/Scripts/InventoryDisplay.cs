using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryBar;
    public GameObject itemDisplayPanel; 
    public Image itemDisplayImage; 
    public TextMeshProUGUI itemDisplayText; 
    public Transform playerHand;

    private bool isInventoryOpen = false;
    private int selectedSlotIndex = -1;

    private void Start()
    {
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                DisplayItem(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            RemoveItemFromInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideItemDisplay();
        }

        if (isInventoryOpen)
        {
            FreezeGame();
        }
        else
        {
            UnfreezeGame();
        }
    }

    private void DisplayItem(int slotIndex)
    {
        if (inventoryBar != null && slotIndex >= 0 && slotIndex < inventoryBar.transform.childCount)
        {
            Transform slot = inventoryBar.transform.GetChild(slotIndex);

            if (slot.childCount > 0)
            {
                Transform imageTransform = slot.GetChild(0); 
                Transform textTransform = slot.GetChild(1); 

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

                        selectedSlotIndex = slotIndex;
                        isInventoryOpen = true;
                    }
                }
            }
        }
    }


    private void RemoveItemFromInventory()
    {
        if (selectedSlotIndex == -1) return; 

        Transform selectedSlot = inventoryBar.transform.GetChild(selectedSlotIndex);

        if (selectedSlot.childCount > 0)
        {
            Transform imageTransform = selectedSlot.GetChild(0);
            Transform textTransform = selectedSlot.GetChild(1);

            TextMeshProUGUI temp = textTransform.GetComponentInChildren<TextMeshProUGUI>();
            string itemName = textTransform.GetComponentInChildren<TextMeshProUGUI>().text;

            Sprite itemSprite = imageTransform.GetComponent<Image>().sprite;

            // Clearing the inventory bar of the item
            GameObject universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
            InventoryBar inventoryBarScript = universalLogicHandler.GetComponent<InventoryBar>();
            inventoryBarScript.RemoveItem(itemName);

            GameObject itemPrefab = Resources.Load<GameObject>("Items/" + itemName);
            if (itemPrefab != null)
            {
                GameObject newItem = Instantiate(itemPrefab, playerHand.position, Quaternion.identity);
                newItem.transform.SetParent(playerHand);
                
                // Placing the object in the hand
                if (itemName == "Flashlight"){
                    newItem.transform.localScale = playerHand.localScale * 6f; 
                    Vector3 offset = new Vector3(0.005f, 0.6f, 0.2f); 
                    newItem.transform.localPosition += offset;
                }
                else {
                    newItem.transform.localScale = playerHand.localScale * 0.2f; 
                    Vector3 offset = new Vector3(0.005f, 0.2f, 0.2f); 
                    newItem.transform.localPosition += offset;
                }

                // Removing the pickup item script from the item in the hand
                Destroy(newItem.GetComponent<PickupItem>());
            }
            else
            {
                Debug.LogError("Item prefab not found in Resources/Items: " + itemName);
            }

            selectedSlotIndex = -1;
            HideItemDisplay();
        }
    }


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

    private void FreezeGame()
    {
        Time.timeScale = 0;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

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

