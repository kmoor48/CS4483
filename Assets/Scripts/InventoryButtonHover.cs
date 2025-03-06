using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtonHover : MonoBehaviour
{
    private GameObject itemLabelText;

    void Start()
    {
        // Retrieve the child object (assumed to be at index 1, for example)
        Transform itemLabelTransform = transform.GetChild(1);
        itemLabelText = itemLabelTransform.gameObject;

        if (itemLabelText == null)
        {
            Debug.LogError("Problem trying to retrieve the inventory slot's text component");
        }
    }

    public void OnHoverEnter()
    {
        itemLabelText.SetActive(true);
    }

    public void OnHoverExit()
    {
        itemLabelText.SetActive(false);
    }
}
