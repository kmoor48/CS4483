using UnityEngine;
using TMPro;

public class PINPuzzleScript : MonoBehaviour
{
    public GameObject pinPuzzleDisplay;
    public GameObject incorrectPINMsg;
    public GameObject correctPINMsg;
    public GameObject audioTrackDisplay;
    public TMP_InputField pinInputField;

    private string correctPIN = "brain";

    public void CheckPIN()
    {
        Debug.Log("here");
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
        audioTrackDisplay.SetActive(true);
    }
}
