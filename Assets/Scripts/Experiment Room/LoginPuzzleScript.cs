using UnityEngine;
using TMPro;

public class LoginPuzzleScript : MonoBehaviour
{
    public TMP_InputField inputFieldH;
    public TMP_InputField inputFieldC;
    public TMP_InputField inputFieldA;
    public TMP_InputField inputFieldT;

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
            Debug.Log("Worked!");
        }
        else
        {
            Debug.Log("Incorrect Input!");
        }
    }
}
