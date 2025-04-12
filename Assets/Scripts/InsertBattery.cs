using UnityEngine;

public class InsertBattery : MonoBehaviour
{
    private GameObject player;
    public Transform voicemailMachine;
    public GameObject insertText; // Text prompt to insert batteries
    public AudioSource voicemailAudio; // Audio source for voicemail
    public Animator drawerAnimator; // Animator to open drawer
    public int requiredBatteries = 2;

    private GameObject exitDoor; // ← Changed from ExitDoor script to GameObject
    private GameObject universalLogicHandler;
    private bool isNearVoicemail = false;
    private int insertedBatteries = 0;
    private bool hasInsertedBatteries = false;
    private InventoryBar inventory;

    private bool audioPlayed = false;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        inventory = FindObjectOfType<InventoryBar>();

        if (inventory == null)
        {
            Debug.LogError("InventoryBar not found!");
            return;
        }

        voicemailAudio.loop = false;
        insertText.SetActive(false);

        exitDoor = GameObject.FindWithTag("ExitDoor"); // ← Find exit door by tag
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, voicemailMachine.position);

        if (distance <= 2.0f && !hasInsertedBatteries)
        {
            if (!isNearVoicemail)
            {
                insertText.SetActive(true);
                isNearVoicemail = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                AttemptToInsertBattery();
            }
        }
        else
        {
            if (isNearVoicemail)
            {
                insertText.SetActive(false);
                isNearVoicemail = false;
            }
        }

        if (audioPlayed && !voicemailAudio.isPlaying)
        {
            drawerAnimator.SetBool("open", true);
            audioPlayed = false;
        }
    }

    private void OnPuzzleSolved()
    {
        if (exitDoor != null)
        {
            BoxCollider collider = exitDoor.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = true; // ← Enable the exit door collider
                Debug.Log("Exit door collider enabled from battery puzzle!");
            }
        }
    }

    void AttemptToInsertBattery()
    {
        InventoryBar inventory = InventoryBar.Instance;

        if (inventory != null && inventory.HasItem("BatteryAA"))
        {
            inventory.RemoveItem("BatteryAA");
            InsertBatteryIntoMachine();
        }
        else
        {
            Debug.Log("No batteries in inventory or already inserted!");
        }
    }

    void InsertBatteryIntoMachine()
    {
        insertedBatteries++;
        Debug.Log("Inserted battery: " + insertedBatteries);

        if (insertedBatteries >= requiredBatteries)
        {
            Debug.Log("All batteries inserted. Playing voicemail and opening drawer!");
            insertText.SetActive(false);
            voicemailAudio.Play();
            drawerAnimator.SetBool("open", true);
            hasInsertedBatteries = true;

            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();

            OnPuzzleSolved();
        }
    }

    void CopyItemsToNewInventory(InventoryBar newInventory)
    {
        if (inventory == null) return;

        if (newInventory != null)
        {
            string[] currentItems = inventory.GetItemNames();

            foreach (var itemName in currentItems)
            {
                if (!string.IsNullOrEmpty(itemName))
                {
                    newInventory.AddItem(null, itemName, null);
                }
            }

            Debug.Log("Items copied to new inventory.");
        }
        else
        {
            Debug.LogError("No inventory found to copy items to!");
        }
    }
}
