using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    private float rotationX = 0f;
    private CharacterController controller;

    // For loading the next scene
    private GameObject universalLogicHandler;
    private UniversalLogicHandler universalLogicHandlerScript;
    private static Vector3[] defaultPlayerPositions = new Vector3[] {
        new Vector3(24.15f, 7.3f, -33.617f), // Level 2
        new Vector3(-21.07f, 8.23f, -15.44f), // Level 3
        new Vector3(-4.163f, 4.35f, -1.4f), // Level 4
        new Vector3(5.944682f, 7f, -8.056301f) // Level 5
    };

    // For the walking audio source
    AudioSource walkingAudio;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked; // Lock and hide the cursor
        //Cursor.visible = false;

        // Adjust the camera's local position (set Y value to desired height)
        playerCamera.transform.localPosition = new Vector3(0f, 1.5f, 0f);

        // For loading the next scene
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        universalLogicHandlerScript = universalLogicHandler.GetComponent<UniversalLogicHandler>();

        // For the walking audio source attached to the player
        walkingAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        LookAround(); // Handle camera rotation
        ProcessInput(); // Handle player movement
    }

    void ProcessInput()
    {
        if (controller == null || !controller.enabled) {
            walkingAudio.Pause();
            return;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

       
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= moveSpeed;

        if (!controller.isGrounded)
        {
            move.y -= 100f * Time.deltaTime; 
        }
        else
        {
            move.y = -20f; 
        }

        // Check movement and control walking audio
        Vector2 inputVector = new Vector2(moveX, moveZ);
        bool isMoving = inputVector.magnitude > 0.1f;
        if (isMoving)
        {
            if (!walkingAudio.isPlaying)
                walkingAudio.Play();
        }
        else
        {
            if (walkingAudio.isPlaying)
                walkingAudio.Pause();
        }

        controller.Move(move * Time.deltaTime);
    }


    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotate the player left/right (around the Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down (around the X-axis)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider's tag matches the trigger tag
        if (other.CompareTag("ExitDoor"))
        {
            // Load the next scene
            universalLogicHandlerScript.LoadNextScene();
        }
    }

    public void ResetPlayerPositionBetweenScenes(int levelIndex)
    {
        Vector3 newPos = defaultPlayerPositions[levelIndex];
        controller.enabled = false; // Disable temporarily to manually set position
        transform.position = newPos;
        controller.enabled = true;
    }
}
