using UnityEngine;
using TMPro;

public class PINPuzzleScript : MonoBehaviour
{
    public GameObject pinPuzzleDisplay;
    public GameObject incorrectPINMsg;
    public GameObject correctPINMsg;
    public TMP_InputField pinInputField;

    private string correctPIN = "49820";

    public void CheckPIN()
    {
        Debug.Log(pinInputField.text);
        if (pinInputField.text == correctPIN )
        {
            pinPuzzleDisplay.SetActive(false);
            correctPINMsg.SetActive(true);
            Invoke("DisableCorrectMsg", 2f);
        }
        else
        {
            incorrectPINMsg.SetActive(true);
            Invoke("DisableIncorrectMsg", 2f);
        }
    }

    void DisableIncorrectMsg()
    {
        incorrectPINMsg.SetActive(false);
    }

    void DisableCorrectMsg()
    {
        correctPINMsg.SetActive(false);
    }
}
