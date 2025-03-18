using UnityEngine;

public class BloodTestingPuzzleScript : MonoBehaviour
{
    // For changing the magnified blood sample display
    public GameObject magnifiedDisplay;
    public GameObject sampleA;
    public GameObject sampleB;
    public GameObject sampleC;
    public GameObject sampleD;

    // For dropper changing
    public Material bloodMaterial;
    public Material glassMaterial;
    public GameObject glassDroper;


    public void TriggerFunction(string gameObjectName)
    {
        magnifiedDisplay.SetActive(true);

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
        else
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
