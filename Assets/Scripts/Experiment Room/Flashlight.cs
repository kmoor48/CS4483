using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    private TextMeshProUGUI itemInstructionsText;
    private GameObject spotLight;
    private bool isOn = false;
    private int layerMask;
    private GameObject hiddenPosterPlane;
    private RevealPosterGradually revealPosterScript;
    private bool hasHitPosterYet = false;

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

        hiddenPosterPlane = GameObject.FindWithTag("PosterPanel");
        revealPosterScript = hiddenPosterPlane.GetComponent<RevealPosterGradually>();

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
                    if (!hasHitPosterYet)
                    {
                        revealPosterScript.StartFadeIn();
                        hasHitPosterYet = true;
                    }
                    else {
                        revealPosterScript.UnPauseFadeIn();
                    }
                }
            }
            else
            {
                revealPosterScript.PauseFadeIn();
            }
        }
    }
}
