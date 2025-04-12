using UnityEngine;

public class CameraSwitcherPuzzleView : MonoBehaviour
{
    private Camera playerCamera;
    public Camera computerPuzzleCamera;
    public Camera recordPlayerPuzzleCamera;
    public Camera bloodTestPuzzleCamera;
    public Camera doorCodeCamera;

    void Start()
    {
        playerCamera = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<Camera>();
        
        // Ensure only the main camera is active at the start
        playerCamera.enabled = true;
        computerPuzzleCamera.enabled = false;
        recordPlayerPuzzleCamera.enabled = false;
        bloodTestPuzzleCamera.enabled = false;
        doorCodeCamera.enabled = false;
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
        if (puzzle == "door")
        {
            doorCodeCamera.enabled = true;
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
        if (puzzle == "door")
        {
            doorCodeCamera.enabled = false;
        }
    }
}
