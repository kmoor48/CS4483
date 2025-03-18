using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    private TextMeshProUGUI itemInstructionsText;
    private GameObject spotLight;
    private bool isOn = false;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            Debug.Log("O key was pressed");
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
    }
}
