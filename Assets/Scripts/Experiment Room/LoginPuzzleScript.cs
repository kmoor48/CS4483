using UnityEngine;
using TMPro;

public class LoginPuzzleScript : MonoBehaviour
{
    public TMP_InputField inputFieldH;
    public TMP_InputField inputFieldC;
    public TMP_InputField inputFieldA;
    public TMP_InputField inputFieldT;
    public GameObject incorrectPasswordText;
    public GameObject correctPasswordText;
    public GameObject loginPuzzleTextParent;
    public GameObject computerDesktopScreen;

    private string correctValueH = "ippocampus";  // Replace with your expected values
    private string correctValueC = "ortex";
    private string correctValueA = "mygdala";
    private string correctValueT = "halamus";

    private GameObject universalLogicHandler;

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
    }

    public void CheckInputs()
    {
        if (inputFieldH.text == correctValueH &&
            inputFieldC.text == correctValueC &&
            inputFieldA.text == correctValueA &&
            inputFieldT.text == correctValueT)
        {
            loginPuzzleTextParent.SetActive(false);
            correctPasswordText.SetActive(true);
            Invoke("HideCorrectMsg", 2f);
        }
        else
        {
            incorrectPasswordText.SetActive(true);
            Invoke("HideIncorrectMsg", 2f);
        }
    }

    void HideIncorrectMsg()
    {
        incorrectPasswordText.SetActive(false);
    }

    void HideCorrectMsg()
    {
        correctPasswordText.SetActive(false);
        computerDesktopScreen.SetActive(true);

        // Mark the puzzle as complete
        LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        clueScript.IncrementPuzzleCounter();
    }
}
