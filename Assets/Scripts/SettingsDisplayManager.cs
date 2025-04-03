using UnityEngine;
using UnityEngine.UI;

public class SettingsDisplayManager : MonoBehaviour
{
    private Button buttonComponent;
    private GameObject settingsDisplay;
    private ClueDisplayManager clueManagerScript;

    void Start()
    {
        clueManagerScript = GameObject.FindWithTag("ClueDisplayPanel").GetComponent<ClueDisplayManager>();
        // Get the Button component attached to this GameObject
        buttonComponent = GetComponent<Button>();

        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OpenSettingsDisplay);
        }

        settingsDisplay = GameObject.FindWithTag("SettingsDisplayPanel");

        // Adding the close settings display function to the close button dynamically 
        Button closeSettingsDisplayBtn = settingsDisplay.transform.GetChild(0).GetComponent<Button>();
        if (closeSettingsDisplayBtn  != null)
        {
            closeSettingsDisplayBtn.onClick.AddListener(CloseSettingsDisplay);
        }

        settingsDisplay.SetActive(false);
    }

    public void OpenSettingsDisplay()
    {
        clueManagerScript.OnCloseClueDisplayButtonClick();
        settingsDisplay.SetActive(true);
    }

    public void CloseSettingsDisplay()
    {
        settingsDisplay.SetActive(false);
    }
}
