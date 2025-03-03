using UnityEngine;

public class BloodTestingPuzzleScript : MonoBehaviour
{
    public GameObject magnifiedDisplay;
    public GameObject sampleA;
    public GameObject sampleB;
    public GameObject sampleC;
    public GameObject sampleD;


    public void TriggerFunction(string gameObjectName)
    {
        magnifiedDisplay.SetActive(true);

        if (gameObjectName == "Blood Sample A")
        {
            sampleA.SetActive(true);
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
            sampleD.SetActive(true);
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
