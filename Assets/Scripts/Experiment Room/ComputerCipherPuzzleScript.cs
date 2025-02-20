using UnityEngine;
using TMPro;

public class ComputerCipherPuzzleScript : MonoBehaviour
{
    public GameObject openFolderDisplay;
    public TMP_Text headerText;
    public TMP_Text folderContentText;
    public GameObject LockedFolderInput;

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
        else
        {
            LockedFolderInput.SetActive(true);
        }

        openFolderDisplay.SetActive(true);
    }

    public void CloseFolder()
    {
        folderContentText.text = "";
        headerText.text = "";
        LockedFolderInput.SetActive(false);
        openFolderDisplay.SetActive(false);
    }
}
