using UnityEngine;
using TMPro;

public class ComputerCipherPuzzleScript : MonoBehaviour
{
    public GameObject openFolderDisplay;
    public TMP_Text headerText;
    public TMP_Text folderContentText;
    public GameObject lockedFolderInput;
    public TMP_InputField folderPasswordInputField;
    public GameObject incorrectPasswordMsg;
    public GameObject correctPasswordMsg;

    private string folderPassword = "Konstyl";

    public void OpenFolder(string folderName)
    {
        Debug.Log(folderName);
        headerText.text = folderName;

        // Populating file content
        if (folderName == "Photos")
        {
            folderContentText.text = "Folder Empty";
        }
        if (folderName == "Family Info")
        {
            folderContentText.text = "Date of Birth: September 15th, 1983 " +
                "\nCity Born in: Columbus Ohio" +
                "\nDoctor's Phone Number: 123-583-9013" +
                "\nDoctor's Address: 580 Cherry Lane" +
                "\nFather's Name: Jerome Pale " +
                "\nWiFi Password: DaRwIn34!" +
                "\nMother's Name: Lois Konstyl";
        }
        if (folderName == "Angela Lucy Experiment")
        {
            lockedFolderInput.SetActive(true);
        }

        openFolderDisplay.SetActive(true);
    }

    public void CloseFolder()
    {
        folderContentText.text = "";
        headerText.text = "";
        lockedFolderInput.SetActive(false);
        openFolderDisplay.SetActive(false);
    }

    public void CheckFolderPassword()
    {
        Debug.Log("Entered Password: "+ folderPasswordInputField.text);
        if (folderPasswordInputField.text == folderPassword)
        {
            lockedFolderInput.SetActive(false);
            correctPasswordMsg.SetActive(true);
            Invoke("DeactivateCorrectPasswordMsg", 2f);
        }
        else
        {
            incorrectPasswordMsg.SetActive(true);
            Invoke("DeactivateIncorrectPasswordMsg", 2f);
        }
    }

    void DeactivateIncorrectPasswordMsg()
    {
        incorrectPasswordMsg.SetActive(false);
    }

    void DeactivateCorrectPasswordMsg()
    {
        correctPasswordMsg.SetActive(false);
    }
}
