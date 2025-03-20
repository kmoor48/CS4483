using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    private TextMeshProUGUI itemInstructionsText;
    private GameObject spotLight;
    private bool isOn = false;
    private int layerMask;

    void Start()
    {
        GameObject itemInstructionsTextContainer = GameObject.FindWithTag("ItemInstructionsText");
        itemInstructionsText = itemInstructionsTextContainer.GetComponent<TextMeshProUGUI>();

        if (itemInstructionsText == null)
        {
            Debug.LogError("Error finding instructions text");
        }
        else{
            itemInstructionsText.text = "Press [O] to turn the Flashlight on/off";
        }

        spotLight = gameObject.transform.GetChild(0).gameObject;
        if (spotLight == null)
        {
            Debug.LogError("No SpotLight object found on flashlight");
        }

        layerMask = LayerMask.GetMask("Interactable Elements"); // Convert layer name to LayerMask
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            if (isOn)
            {
                spotLight.SetActive(false);
                isOn = false;
            }
            else
            {
                spotLight.SetActive(true);
                isOn = true;
            }
        }

        if (spotLight.activeSelf)  // Only check when flashlight is on
        {
            RaycastHit hit;
            if (Physics.Raycast(spotLight.transform.position, spotLight.transform.forward, out hit, 2, layerMask))
            {
                // Checking to see if light hit the Poster
                if (hit.collider.gameObject.name == "Poster")
                {
                    Debug.Log("Light hit the target object!");
                    // Call any function here (e.g., activate something)
                }
            }
        }
    }
}
