using UnityEngine;

public class BloodTestingPuzzleScript : MonoBehaviour
{
    public GameObject magnifiedDisplay;
    private GameObject[] magnifiedPuzzles;

    void Start()
    {
        //magnifiedPuzzles = gameObject.GetComponentsInChildren<GameObject>();
        //Debug.Log(magnifiedPuzzles);
        // Loop through all children of the current GameObject
        /*foreach (Transform child in magnifiedDisplay.transform)
        {
            Debug.Log("Child: " + child.name);
        }*/
        Transform specificChild = magnifiedDisplay.Find("Glass");
    }

    public void TriggerFunction(string gameObjectName)
    {
        Debug.Log("INSIDE: "+ gameObjectName);
        magnifiedDisplay.SetActive(true);
    }

    public void CloseButtonClicked()
    {
        magnifiedDisplay.SetActive(false);
    }
}
