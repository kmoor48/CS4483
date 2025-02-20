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

    private string correctValueH = "ippocampus";  // Replace with your expected values
    private string correctValueC = "ortex";
    private string correctValueA = "mygdala";
    private string correctValueT = "halamus";

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
    }
}
