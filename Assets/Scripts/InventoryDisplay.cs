using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryDisplay : MonoBehaviour
{
    private GameObject inventoryBar;
    private GameObject itemDisplayPanel; 
    private Image itemDisplayImage; 
    private TextMeshProUGUI itemDisplayText; 
    private Transform playerHand;
    private GameObject itemInstructionsText;

    private bool isInventoryOpen = false;
    private int selectedSlotIndex = -1;
    private bool isClockInteracting = false; // Track clock interaction state


    public ClockInteraction clockInteraction; // Reference to ClockInteraction script


    private void Start()
    {
        // Getting all objects through Tags
        inventoryBar = GameObject.FindWithTag("InventoryBar");
        itemInstructionsText = GameObject.FindWithTag("ItemUseInstructions");
        itemDisplayPanel = GameObject.FindWithTag("ItemDisplayPanel");
        playerHand = GameObject.FindWithTag("PlayerRightHandTarget").transform;
        GameObject itemDisplayImageGO = itemDisplayPanel.transform.GetChild(0).gameObject;
        GameObject itemDisplayTextGO = itemDisplayPanel.transform.GetChild(1).gameObject;
        itemDisplayImage = itemDisplayImageGO.GetComponent<Image>();
        itemDisplayText = itemDisplayTextGO.GetComponent<TextMeshProUGUI>();

        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
        }
        if (itemInstructionsText != null)
        {
            itemInstructionsText.SetActive(false);
        }
    }

    private void Update()
    {

        if (clockInteraction != null)
        {
            isClockInteracting = clockInteraction.IsEditingClock();
        }

        if (isClockInteracting)
        {
            return; // Block inventory actions if clock is being interacted with
        }

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReturnToInventory();
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

    private void ReturnToInventory()
    {
        if (playerHand.childCount == 0)
        {
            return;
        }

        Transform itemInHand = playerHand.GetChild(0);
        //itemInHand.GetComponent<PickupItem>().enabled = true;
        PickupItem pickupItem = itemInHand.GetComponent<PickupItem>();

        if (pickupItem == null)
        {
            Debug.LogError("Item in hand does not have a PickupItem component!");
            return;
        }
        pickupItem.enabled = true;
        string itemName = pickupItem.itemName; 
        Sprite itemSprite = pickupItem.image;
        

        if (itemSprite == null)
        {
            Debug.LogError("Item does not have a valid sprite!");
            return;
        }

        GameObject universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        InventoryBar inventoryBarScript = universalLogicHandler.GetComponent<InventoryBar>();

        inventoryBarScript.AddItem(itemInHand.gameObject, itemName, itemSprite);

        // Closing item instruction text
        itemInstructionsText.SetActive(false);

        Destroy(itemInHand.gameObject); 

        HideItemDisplay();
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

            GameObject itemPrefab = Resources.Load<GameObject>("Items/" + itemName);

            if (itemPrefab != null)
            {
                // Clearing the inventory bar of the item
                GameObject universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
                InventoryBar inventoryBarScript = universalLogicHandler.GetComponent<InventoryBar>();
                inventoryBarScript.RemoveItem(itemName);

                // Adding the object to the hand
                GameObject newItem = Instantiate(itemPrefab, playerHand.position, Quaternion.identity);
                newItem.transform.SetParent(playerHand);
                
                // Placing the object in the hand
                if (itemName == "Flashlight"){
                    newItem.transform.localScale = playerHand.localScale * 6f; 
                    Vector3 offset = new Vector3(-0.2f, 0.4f, 0.2f); 
                    newItem.transform.localPosition += offset;

                    // Set the rotation with a custom value (for example, 90 degrees on the Y-axis)
                    Quaternion customRotation = Quaternion.Euler(-20, 6, 44.2f);  // Rotate 90 degrees around the Y-axis
                    newItem.transform.localRotation = customRotation;

                    newItem.AddComponent<Flashlight>();
                    itemInstructionsText.SetActive(true);
                }

                else
                {
                    newItem.transform.localScale = playerHand.localScale * 0.2f; 
                    Vector3 offset = new Vector3(0.005f, 0.2f, 0.2f); 
                    newItem.transform.localPosition += offset;
                }

                // Removing the pickup item script from the item in the hand
                //Destroy(newItem.GetComponent<PickupItem>());
                PickupItem pickupItemScript = newItem.GetComponent<PickupItem>();
                if (pickupItemScript != null)
                {
                    pickupItemScript.enabled = false; // Disable so it doesn't interact again
                    pickupItemScript.SetOpenTextFalse();
                }
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

