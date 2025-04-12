using UnityEngine;

public class InsertBattery : MonoBehaviour
{
    private GameObject player;
    public Transform voicemailMachine;
    public GameObject insertText; // Text prompt to insert batteries
    public AudioSource voicemailAudio; // Audio source for voicemail
    public Animator drawerAnimator; // Animator to open drawer
    public int requiredBatteries = 2;
    public ExitDoor exitDoor; // Reference to the ExitDoor script

    private GameObject universalLogicHandler;
    private bool isNearVoicemail = false; // Track if the player is near the voicemail machine
    private int insertedBatteries = 0; // Track number of inserted batteries
    private bool hasInsertedBatteries = false; // Flag to track if the required batteries are inserted
    private InventoryBar inventory; // Reference to inventory system

    private bool audioPlayed = false; // Flag to track if the voicemail is playing

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

        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        voicemailAudio.loop = false; // just in case
        insertText.SetActive(false); // Start with the prompt hidden
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, voicemailMachine.position);

        // Only show the "Insert Batteries" prompt if player is near and hasn't inserted all batteries
        if (distance <= 2.0f && !hasInsertedBatteries)
        {
            if (!isNearVoicemail)
            {
                insertText.SetActive(true); // Show the prompt
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
                insertText.SetActive(false); // Hide the prompt when player moves away
                isNearVoicemail = false;
            }
        }

        // Check if audio has finished
        if (audioPlayed && !voicemailAudio.isPlaying)
        {
            drawerAnimator.SetBool("open", true); // Open the drawer after voicemail ends
            audioPlayed = false; // Prevent it from firing again
        }
    }

    private void OnPuzzleSolved()
    {
        // Call the PuzzleSolved method from ExitDoor when the puzzle is completed
        exitDoor.PuzzleSolved();
    }

    void AttemptToInsertBattery()
    {
        // Reference the persistent InventoryBar instance
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
            insertText.SetActive(false); // Hide the "Insert Batteries" text

            voicemailAudio.Play(); // Play the voicemail
            drawerAnimator.SetBool("open", true); // Open the drawer immediately after voicemail plays
            hasInsertedBatteries = true; // Mark that batteries have been inserted

            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();

            OnPuzzleSolved();
        }
    }

    // Helper function to copy items from the current inventory (level 1) to the new inventory (level 2)
    void CopyItemsToNewInventory(InventoryBar newInventory)
    {
        // Get current inventory items
        if (inventory == null) return;

        // Check if there is already an inventory for the new level
        if (newInventory != null)
        {
            // Copy items from the old inventory to the new inventory
            string[] currentItems = inventory.GetItemNames(); // You can replace this with whatever method you use to get item names from the inventory

            foreach (var itemName in currentItems)
            {
                if (!string.IsNullOrEmpty(itemName))
                {
                    // Add each item to the new inventory (this assumes you have a method to add items to your new inventory)
                    newInventory.AddItem(null, itemName, null); // Adjust this part if needed to match your AddItem method
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
