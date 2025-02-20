using UnityEngine;
using TMPro;

public class ComputerCipherPuzzleScript : MonoBehaviour
{
    public GameObject openFolderDisplay;
    public TMP_Text headerText;

    public void OpenFolder(string folderName)
    {
        Debug.Log(folderName);
        headerText.text = folderName;
        openFolderDisplay.SetActive(true);
    }

    public void CloseFolder()
    {
        openFolderDisplay.SetActive(false);
    }
}
