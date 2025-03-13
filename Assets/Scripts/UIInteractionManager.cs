using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    public static UIInteractionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EnableUIInteraction()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
        Cursor.visible = true;
    }

    public void DisableUIInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor for gameplay
        Cursor.visible = false;
    }
}

