using UnityEngine;

public class BloodTestingPuzzleScript : MonoBehaviour
{
    // For changing the magnified blood sample display
    public GameObject magnifiedDisplay;
    public GameObject sampleA;
    public GameObject sampleB;
    public GameObject sampleC;
    public GameObject sampleD;
    
    // For Sample Glass
    public GameObject glassDropper;
    private GlassDropperScript glassDropperScript;

    void Start()
    {
        glassDropperScript = glassDropper.GetComponent<GlassDropperScript>();
    }

    public void TriggerFunction(string gameObjectName)
    {
        magnifiedDisplay.SetActive(true);
        bool isBloodDisplayedOnSampleGlass = glassDropperScript.IsBloodOnSample();

        if (gameObjectName == "Blood Sample D")
        {
            sampleD.SetActive(true);
        }
        else if (gameObjectName == "Blood Sample B")
        {
            sampleB.SetActive(true);
        }
        else if (gameObjectName == "Blood Sample C")
        {
            sampleC.SetActive(true);
        }
        else if (gameObjectName == "Blood Sample A")
        {
            sampleA.SetActive(true);
        }
        else if (gameObjectName == "Sample Glass" && isBloodDisplayedOnSampleGlass)
        {
            sampleA.SetActive(true);
        }
    }

    public void CloseButtonClicked()
    {
        magnifiedDisplay.SetActive(false);

        // ensure all the sample views are not active
        sampleA.SetActive(false);
        sampleB.SetActive(false);
        sampleC.SetActive(false);
        sampleD.SetActive(false);
    }
}
