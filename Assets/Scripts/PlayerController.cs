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
    }

    void Update()
    {
        LookAround(); // Handle camera rotation
        ProcessInput(); // Handle player movement
    }

    void ProcessInput()
    {
        if (controller == null || !controller.enabled) {
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


}
