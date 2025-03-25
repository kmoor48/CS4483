using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalLogicHandler : MonoBehaviour
{
    // Singleton pattern to ensure only one instance exists
    private static UniversalLogicHandler instance;
    // Script reference to clue and level manager on this go
    private LevelClueAndProgressionManager clueLevelManagerScript;

    // Index of the next scene to load
    private int nextSceneIndex;

    void Awake()
    {
        // Ensure the object persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the next scene index to the current scene's build index + 1
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        clueLevelManagerScript = gameObject.GetComponent<LevelClueAndProgressionManager>();
        // testing purposes
        clueLevelManagerScript.SetCurrentLevel(3);
    }

    public void LoadNextScene()
    {
        Debug.Log(nextSceneIndex);
        // Check if the next scene index is valid
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            nextSceneIndex += 1;
        }
        else
        {
            Debug.LogWarning("No more scenes to load. End of build settings.");
            // Optionally, you can loop back to the first scene or show a game-over screen
            // SceneManager.LoadScene(0); // Uncomment to loop back to the first scene
        }
    }
}
