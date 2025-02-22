using UnityEngine;

public class CameraSwitcherPuzzleView : MonoBehaviour
{
    public Camera mainCamera;
    public Camera computerPuzzleCamera;
    public Camera posterPuzzleCamera;
    public Camera recordPlayerPuzzleCamera;

    void Start()
    {
        // Ensure only the main camera is active at the start
        //mainCamera.enabled = true;
        //puzzleCamera.enabled = false;

        //For testing:
        //mainCamera.enabled = false;
        //computerPuzzleCamera.enabled = false;
        //posterPuzzleCamera.enabled = false;
        //recordPlayerPuzzleCamera.enabled = true;
    }

    public void SwitchToPuzzleCamera(string puzzle)
    {
        mainCamera.enabled = false;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = true;
        }
        if (puzzle == "poster")
        {
            posterPuzzleCamera.enabled = true;
        }
        if (puzzle == "record")
        {
            recordPlayerPuzzleCamera.enabled = true;
        }
    }

    public void SwitchToMainCamera(string puzzle)
    {
        mainCamera.enabled = true;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = false;
        }
        if (puzzle == "poster")
        {
            posterPuzzleCamera.enabled = false;
        }
        if (puzzle == "record")
        {
            recordPlayerPuzzleCamera.enabled = false;
        }
    }
}
