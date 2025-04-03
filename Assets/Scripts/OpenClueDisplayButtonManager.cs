using UnityEngine;
using UnityEngine.UI;

public class OpenClueDisplayButtonManager : MonoBehaviour
{
    private GameObject universalLogicHandler;
    private Button buttonComponent;

    void Start()
    {
        // Get the Button component attached to this GameObject
        buttonComponent = GetComponent<Button>();

        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClickOpenClueDisplay);
        }

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        if (universalLogicHandler == null)
        {
            Debug.LogError("No UniversalLogicHandler found. Ensure tag is attached and prefab exists in the hierarchy.");
        }
    }

    public void OnButtonClickOpenClueDisplay()
    {
        LevelClueAndProgressionManager clueManagerScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        Debug.Log(universalLogicHandler);
        Debug.Log(clueManagerScript);

        clueManagerScript.OpenClueDisplay();
    }


}
