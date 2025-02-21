using UnityEngine;

public class CameraSwitcherPuzzleView : MonoBehaviour
{
    public Camera mainCamera;
    public Camera puzzleCamera;

    void Start()
    {
        // Ensure only the main camera is active at the start
        //mainCamera.enabled = true;
        //puzzleCamera.enabled = false;

        //For testing:
        //mainCamera.enabled = false;
        //puzzleCamera.enabled = true;
    }

    public void SwitchToPuzzleCamera()
    {
        mainCamera.enabled = false;
        puzzleCamera.enabled = true;
    }

    public void SwitchToMainCamera()
    {
        mainCamera.enabled = true;
        puzzleCamera.enabled = false;
    }
}
