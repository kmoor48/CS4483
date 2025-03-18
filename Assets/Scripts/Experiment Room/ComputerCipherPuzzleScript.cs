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
    public GameObject passwordFolder;

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
            folderContentText.text = "Date of Birth: PQBQLAJBO 15QE, 1983" +
                "\nCity Born in: ZFLJKYLU VLKLF" +
                "\nDoctor's Phone Number: 123-583-9013" +
                "\nDoctor's Address: 580 ZLOIIZ NIXPB" +
                "\nFather's Name: MCQLOBK OXIB" +
                "\nWiFi Password: XTLOVRF34!" +
                "\nMother's Name: OXZEBI HLKPQV";
        }
        if (folderName == "Angela Lucy Experiment")
        {
            lockedFolderInput.SetActive(true);
        }
        if (folderName == "Passwords")
        {
            folderContentText.text = "Record Player PIN: 49820";
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
            passwordFolder.SetActive(true);
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
        folderContentText.text = "Brain_Extraction_Report.pdf" +
            "\nAngela_PreOp_Notes.txt" +
            ":\nFailed_Graft_Analysis.docx" +
            "\nConscious_Transfer_Trial_3.log" +
            "\nLucy_Final_Words.wav" +
            "\nNeural_Host_Compatibility.pdf" +
            "\nCortex_Removal_Procedure.txt" +
            "\nSubject_042_PostMortem_Report.pdf" +
            "\nMemory_Wipe_Protocol.docx" +
            "\nEmergency_Termination_Order.txt";
    }
}
