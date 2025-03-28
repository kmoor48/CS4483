using UnityEngine;
using UnityEngine.UI;

public class ClueDisplayManager : MonoBehaviour
{
    private GameObject universalLogicHandler;
    private Button closeClueDisplayButton;
    private Button requestNewClueButton;
    private LevelClueAndProgressionManager clueScript;

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
