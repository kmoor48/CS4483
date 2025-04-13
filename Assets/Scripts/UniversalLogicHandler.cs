using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalLogicHandler : MonoBehaviour
{
    // Singleton pattern to ensure only one instance exists
    private static UniversalLogicHandler instance;
    // Script reference to clue and level manager on this go
    private LevelClueAndProgressionManager clueLevelManagerScript;
    // Script reference to Inventory Bar to clear out inventory with unecessairy items
    private InventoryBar inventoryBarScript;

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

        clueLevelManagerScript.SetCurrentLevel(0);

        inventoryBarScript = gameObject.GetComponent<InventoryBar>();
    }

    public void LoadNextScene()
    {
        // Recalculate the next scene index dynamically every time
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);

            int nextLevelIndex = nextSceneIndex - 1; // Adjust for cutscene
            clueLevelManagerScript.SetCurrentLevel(nextLevelIndex);
            inventoryBarScript.ClearInventoryBetweenLevels(nextLevelIndex - 1);
            PlayerController playerControllerScript = PlayerPersistAcrossAllScenes.Instance.gameObject.GetComponent<PlayerController>();
            playerControllerScript.ResetPlayerPositionBetweenScenes(nextLevelIndex - 1);
        }
        else
        {
            Debug.LogWarning("No more scenes to load. End of build settings.");
        }
    }

}
