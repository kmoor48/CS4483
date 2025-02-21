using UnityEngine;

public class CameraSwitcherPuzzleView : MonoBehaviour
{
    public Camera mainCamera;
    public Camera computerPuzzleCamera;
    public Camera posterPuzzleCamera;

    void Start()
    {
        // Ensure only the main camera is active at the start
        //mainCamera.enabled = true;
        //puzzleCamera.enabled = false;

        //For testing:
        //mainCamera.enabled = false;
        //computerPuzzleCamera.enabled = false;
        //posterPuzzleCamera.enabled = true;
    }

    public void SwitchToPuzzleCamera(string puzzle)
    {
        mainCamera.enabled = false;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = true;
        }
        else
        {
            posterPuzzleCamera.enabled = true;
        }
    }

    public void SwitchToMainCamera(string puzzle)
    {
        mainCamera.enabled = true;

        if (puzzle == "computer")
        {
            computerPuzzleCamera.enabled = false;
        }
        else
        {
            posterPuzzleCamera.enabled = false;
        }
    }
}
