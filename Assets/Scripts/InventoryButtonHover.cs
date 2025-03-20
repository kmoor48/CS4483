using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryButtonHover : MonoBehaviour
{
    private GameObject itemLabelText;
    private GameObject universalLogicHandler;
    private InventoryBar inventoryBarScript;

    void Start()
    {
        Transform itemLabelTransform = transform.GetChild(1);
        itemLabelText = itemLabelTransform.gameObject;

        if (itemLabelText == null)
        {
            Debug.LogError("Problem trying to retrieve the inventory slot's text component");
        }

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        if (universalLogicHandler == null)
        {
            Debug.LogError("UniversalLogicHandler not found");
        }

        inventoryBarScript = universalLogicHandler.GetComponent<InventoryBar>();
        if (inventoryBarScript == null)
        {
            Debug.LogError("UniversalLogicHandler Inventory Bar script is missing");
        }
    }

    public void OnHoverEnter()
    {
        itemLabelText.SetActive(true);

        // For tracking what's hovered over for 2D puzzles
        inventoryBarScript.SetHoverState(itemLabelText);
    }

    public void OnHoverExit()
    {
        itemLabelText.SetActive(false);

        // For tracking what's hovered over for 2D puzzles
        inventoryBarScript.UnsetHoverState();
    }
}

