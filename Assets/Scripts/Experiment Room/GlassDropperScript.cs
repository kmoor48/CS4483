using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    public Camera puzzleCamera;
    public Material glassMaterial;
    public Material bloodMaterial;

    private bool isHolding = false;
    private Vector3 originalPosition;
    private GameObject universalLogicHandler;
    private InventoryBar inventoryBarScript;

    // For material switching
    private MeshRenderer meshRenderer;
    private Material[] materials;
    private bool isHoveringOverSampleGlass = false;
    private bool isFilledWithBlood = false;

    void Start()
    {
        originalPosition = transform.position; // Store the starting position

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

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null && meshRenderer.materials.Length > 1)
        {
            materials = meshRenderer.materials; // Get the material array
            if (materials == null)
            {
                Debug.LogError("No materials found on Glass Dropper");
            }
        }
        else 
        {
            Debug.LogError("Incorrect mesh render found on glass dropper game object");
        }
    }

    void OnMouseDown()
    {
        // Check if it was a left mouse click (pick up or drop item)
        if (Input.GetMouseButton(0))
        {
            if (!isHolding)
            {
                PickUp();
            }
            else
            {
                Drop();
            }
        }
    }

    void OnMouseOver()
    {
        // Check for right mouse click while the mouse is over the object
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Debug.Log("Right Click");
            // Check to see if an inventory item is currently being hovered over
            GameObject hoverState = inventoryBarScript.CheckHoverState();
            if (hoverState != null) 
            {
                TextMeshProUGUI itemText = hoverState.GetComponentInChildren<TextMeshProUGUI>();

                // If Missing Poster is right clicked, change the dropper to material that mimics filled with blood
                if (itemText.text == "Missing Poster")
                {
                    materials[1] = bloodMaterial; // Change Element 1
                    meshRenderer.materials = materials; // Apply the updated array
                    isFilledWithBlood = true;
                }
            }
        }
    }

    void PickUp()
    {
        isHolding = true;
    }

    void Drop()
    {
        isHolding = false;
        transform.position = originalPosition; // Place it back on the table
    }

    void Update()
    {
        if (isHolding)
        {
            // Get the mouse position in screen space (relative to your custom camera)
            Vector3 mousePosition = Input.mousePosition;

            // Set the z-value to the dropper's original z (or a fixed value)
            mousePosition.z = puzzleCamera.WorldToScreenPoint(transform.position).z;

            // Convert screen space to world space using your custom camera
            Vector3 worldPosition = puzzleCamera.ScreenToWorldPoint(mousePosition);

            // Update only x and z position, keeping y fixed at original
            transform.position = new Vector3(worldPosition.x, originalPosition.y, worldPosition.z);
        }
    }

    public void OnGlassSampleHoverEnter()
    {
        Debug.Log("Hovering over glass sample");
        isHoveringOverSampleGlass = true;
    }

    public void OnGlassSampleHoverExit()
    {
        Debug.Log("Hovering not over glass sample");
        isHoveringOverSampleGlass = false;
    }
}
