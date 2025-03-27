using UnityEngine;

public class CameraSwitcherPuzzleView : MonoBehaviour
{
    public Camera playerCamera;
    public Camera computerPuzzleCamera;
    public Camera recordPlayerPuzzleCamera;
    public Camera bloodTestPuzzleCamera;

    void Start()
    {
        // Ensure only the main camera is active at the start
        playerCamera.enabled = true;
        computerPuzzleCamera.enabled = false;
        recordPlayerPuzzleCamera.enabled = false;
        bloodTestPuzzleCamera.enabled = false;

        //For testing:
        //playerCamera.enabled = false;
        //computerPuzzleCamera.enabled = false;
        //recordPlayerPuzzleCamera.enabled = true;
    }

    public void SwitchToPuzzleCamera(string puzzle)
    {
        playerCamera.enabled = false;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = true;
        }
        if (puzzle == "record")
        {
            recordPlayerPuzzleCamera.enabled = true;
        }
        if (puzzle == "blood")
        {
            bloodTestPuzzleCamera.enabled = true;
        }
    }

    public void SwitchToMainCamera(string puzzle)
    {
        playerCamera.enabled = true;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = false;
        }
        if (puzzle == "record")
        {
            recordPlayerPuzzleCamera.enabled = false;
        }
        if (puzzle == "blood")
        {
            bloodTestPuzzleCamera.enabled = false;
        }
    }
}
