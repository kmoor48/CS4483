using UnityEngine;
using UnityEngine.UI;

public class ClueDisplayManager : MonoBehaviour
{
    private GameObject universalLogicHandler;
    private Button closeClueDisplayButton;
    private Button requestNewClueButton;
    private LevelClueAndProgressionManager clueScript;

    // Bools for managing when to set inactive the clue display but only after all components have their references
    private bool startIsDone = false;
    private bool levelClueScriptIsDone = false;
    private bool settingsDisplayScriptIsDone = false;

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        
        closeClueDisplayButton = transform.GetChild(0).GetComponent<Button>();
        Image closeClueDisplayImage = transform.GetChild(0).GetComponent<Image>();

        requestNewClueButton = transform.GetChild(1).GetComponent<Button>();
        Image requestNewClueImage = transform.GetChild(1).GetComponent<Image>();

        closeClueDisplayButton.onClick.AddListener(OnCloseClueDisplayButtonClick);
        requestNewClueButton.onClick.AddListener(OnRequestNewClueButtonClick);

        if (closeClueDisplayImage != null && requestNewClueImage != null)
        {
            closeClueDisplayImage.raycastTarget = true;
            requestNewClueImage.raycastTarget = true;
        }

        // If the other 2 are done, set the object inactive
        if (levelClueScriptIsDone && settingsDisplayScriptIsDone)
        {
            gameObject.SetActive(false);
        }
        else
        {
            startIsDone = true; // Else set the flag this script is done
        }
    }

    public void ReadyToSetInactiveLevelScript()
    {
        // If the other 2 are done, set the object inactive
        if (startIsDone && settingsDisplayScriptIsDone)
        {
            gameObject.SetActive(false);
        }
        else
        {
            levelClueScriptIsDone = true; // Else set the flag this script is done
        }
    }

    public void ReadyToSetInactiveSettingsDisplayScript()
    {
        // If the other 2 are done, set the object inactive
        if (startIsDone && levelClueScriptIsDone)
        {
            gameObject.SetActive(false);
        }
        else
        {
            settingsDisplayScriptIsDone = true; // Else set the flag this script is done
        }
    }

    public void OnCloseClueDisplayButtonClick()
    {
        clueScript.CloseClueDisplay();
    }

    public void OnRequestNewClueButtonClick()
    {
        clueScript.GetClue();
    }
}
