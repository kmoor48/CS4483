using UnityEngine;

public class BloodTestingPuzzleScript : MonoBehaviour
{
    // For changing the magnified blood sample display
    public GameObject magnifiedDisplay;
    public GameObject sampleA;
    public GameObject sampleB;
    public GameObject sampleC;
    public GameObject sampleD;
    
    private  GameObject currentlyDisplayedSample;

    // For Sample Glass
    public GameObject glassDropper;
    private GlassDropperScript glassDropperScript;

    private GameObject universalLogicHandler;

    // For puzzle progression
    public GameObject flashlight;

    void Start()
    {
        glassDropperScript = glassDropper.GetComponent<GlassDropperScript>();
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
    }

    public void TriggerFunction(GameObject sampleGO)
    {
        magnifiedDisplay.SetActive(true);
        bool isBloodDisplayedOnSampleGlass = glassDropperScript.IsBloodOnSample();

        currentlyDisplayedSample = sampleGO;
        string gameObjectName = sampleGO.name;

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

            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();
            
            // Make flashlight available for next puzzle 
            flashlight.SetActive(true);
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

        // Reset the sample position
        Draggable3D sampleMovingScript = currentlyDisplayedSample.GetComponent<Draggable3D>();
        sampleMovingScript.ReturnToOriginalPosition();
    }
}
