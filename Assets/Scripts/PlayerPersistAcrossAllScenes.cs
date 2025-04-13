using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistAcrossAllScenes : MonoBehaviour
{
    public static PlayerPersistAcrossAllScenes Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");

        if (spawnPoint != null)
        {
            CharacterController cc = GetComponent<CharacterController>();
            Rigidbody rb = GetComponent<Rigidbody>();

            // Disable character controller to safely reposition
            if (cc != null) cc.enabled = false;

            // Move to spawn point
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;

            // Reset Rigidbody velocity (if used)
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Re-enable CharacterController after moving
            if (cc != null) cc.enabled = true;
        }
        else
        {
            Debug.LogWarning("No object with tag 'PlayerSpawn' found in scene: " + scene.name);
        }
    }
}

