using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    public Animator ANI;
    public GameObject drawerText;
    public GameObject closedText;
    public GameObject lockText;
    public GameObject padlock;
    public bool isLocked = true;
    private GameObject player;
    public Camera mainCamera;
    public Camera lockCamera;

    public AudioClip openSound; // 🔊 Add this
    public AudioClip closeSound; // 🔊 And this

    private bool open;
    private bool inReach;
    private bool interactingWithLock = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        drawerText.SetActive(false);
        closedText.SetActive(false);
        lockText.SetActive(false);

        ANI.SetBool("open", false);
        ANI.SetBool("close", false);
        open = false;

        if (lockCamera != null)
            lockCamera.gameObject.SetActive(false);
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
                lockText.SetActive(true);
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
            lockText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (isLocked)
            {
                StartLockInteraction();
            }
            else if (!open)
            {
                ANI.SetBool("open", true);
                ANI.SetBool("close", false);
                open = true;
                drawerText.SetActive(false);

                // 🔊 Play opening sound
                if (openSound != null)
                    AudioSource.PlayClipAtPoint(openSound, transform.position);
            }
            else
            {
                ANI.SetBool("open", false);
                ANI.SetBool("close", true);
                open = false;
                closedText.SetActive(false);

                // 🔊 Play closing sound
                if (closeSound != null)
                    AudioSource.PlayClipAtPoint(closeSound, transform.position);
            }
        }

        if (interactingWithLock && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLockInteraction();
        }
    }

    public void UnlockDrawer()
    {
        isLocked = false;
        lockText.SetActive(false);

        if (padlock != null)
        {
            padlock.SetActive(false);
        }
    }

    private void StartLockInteraction()
    {
        if (padlock != null)
        {
            interactingWithLock = true;
            padlock.GetComponent<MoveRuller>().StartLockInteraction();

            if (player != null)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
            }

            if (mainCamera != null && lockCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
                lockCamera.gameObject.SetActive(true);
            }

            lockText.SetActive(false);
        }
    }

    private void ExitLockInteraction()
    {
        interactingWithLock = false;

        if (player != null)
        {
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
        }

        if (mainCamera != null && lockCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            lockCamera.gameObject.SetActive(false);
        }
    }
}
