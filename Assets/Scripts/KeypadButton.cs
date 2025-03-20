//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class KeypadButton : MonoBehaviour
//{
//    public string buttonValue; // Number or "Submit"

//    public void OnButtonPress()
//    {
//        LockInteraction lockScript = FindObjectOfType<LockInteraction>();

//        if (lockScript != null)
//        {
//            if (buttonValue == "Submit")
//            {
//                lockScript.SubmitCode();
//            }
//            else
//            {
//                lockScript.AddDigit(buttonValue);
//            }
//        }
//    }

//}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadButton : MonoBehaviour
{
    public string buttonValue; // Number or "Submit"

    public void OnButtonPress()
    {
        Debug.Log($"Button {buttonValue} was pressed!");

        LockInteraction lockScript = FindObjectOfType<LockInteraction>();

        if (lockScript != null)
        {
            if (buttonValue == "Submit")
            {
                Debug.Log("Submit button pressed!");
                lockScript.SubmitCode();
            }
            else
            {
                Debug.Log($"Adding digit {buttonValue} to input.");
                lockScript.AddDigit(buttonValue);
            }
        }
        else
        {
            Debug.LogError("LockInteraction script not found in the scene!");
        }
    }
}


