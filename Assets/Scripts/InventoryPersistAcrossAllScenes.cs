using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryPersistAcrossAllScenes : MonoBehaviour
{
    public static InventoryPersistAcrossAllScenes Instance { get; private set; }
    
    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This makes the canvas persist
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if a canvas already exists
        }
    }
}
