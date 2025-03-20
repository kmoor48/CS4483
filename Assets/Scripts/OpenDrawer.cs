using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    public Animator ANI;
    public GameObject drawerText;
    public GameObject closedText;
    public GameObject lockText; // UI text for locked state
    public GameObject padlock; // Reference to the lock GameObject (not the script)
    public bool isLocked = true; // Set to false when lock is solved
    public GameObject player; // Reference to the player
    public Camera mainCamera; // The player's main camera
    public Camera lockCamera; // Camera to view the lock

    private bool open;
    private bool inReach;
    private bool interactingWithLock = false;

    void Start()
    {
        drawerText.SetActive(false);
        closedText.SetActive(false);
        lockText.SetActive(false); // Ensure the lock text is hidden initially

        ANI.SetBool("open", false);
        ANI.SetBool("close", false);
        open = false;

        if (lockCamera != null)
            lockCamera.gameObject.SetActive(false); // Hide lock camera initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;

            if (!open && !isLocked)
            {
                drawerText.SetActive(true);
            }
            else if (open && !isLocked)
            {
                closedText.SetActive(true);
            }
            else if (isLocked)
            {
                drawerText.SetActive(false);
                closedText.SetActive(false);
                lockText.SetActive(true); // Show lockText when in reach and locked
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            drawerText.SetActive(false);
            closedText.SetActive(false);
            lockText.SetActive(false); // Hide lockText when out of range
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (isLocked)
            {
                StartLockInteraction(); // Show lock interaction and camera
            }
            else if (!open)
            {
                ANI.SetBool("open", true);
                ANI.SetBool("close", false);
                open = true;
                drawerText.SetActive(false);
            }
            else
            {
                ANI.SetBool("open", false);
                ANI.SetBool("close", true);
                open = false;
                closedText.SetActive(false);
            }
        }

        // Allow player to leave lock interaction
        if (interactingWithLock && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLockInteraction(); // Exit lock interaction
        }
    }

    public void UnlockDrawer()
    {
        isLocked = false; // Unlock the drawer
        Debug.Log("Drawer Unlocked!");
        lockText.SetActive(false); // Hide the lock text after unlocking

        if (padlock != null)
        {
            padlock.SetActive(false); // Make the padlock disappear when unlocked
        }
    }

    private void StartLockInteraction()
    {
        if (padlock != null)
        {
            interactingWithLock = true;
            padlock.GetComponent<MoveRuller>().StartLockInteraction(); // Lock script handles rotation

            // Disable player movement
            if (player != null)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false; // Replace with actual movement script
            }

            // Switch to lock camera
            if (mainCamera != null && lockCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
                lockCamera.gameObject.SetActive(true);
            }

            // Hide lock UI text after interacting with the lock
            lockText.SetActive(false);
        }
    }

    private void ExitLockInteraction()
    {
        interactingWithLock = false;

        // Re-enable player movement
        if (player != null)
        {
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true; // Replace with actual movement script
        }

        // Switch back to main camera
        if (mainCamera != null && lockCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            lockCamera.gameObject.SetActive(false);
        }
    }
}
