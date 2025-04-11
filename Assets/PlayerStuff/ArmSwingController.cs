//using UnityEngine;

//public class ArmSwingController : MonoBehaviour
//{
//    public float baseSwingAmount = 0.1f;  
//    public float baseBounceAmount = 0.05f; 
//    public float baseSwingSpeed = 3f;  
//    public float baseBounceSpeed = 6f; 

//    public float speedMultiplier = 1.5f;  

//    private Vector3 initialPosition;
//    private float timeOffset;
//    private PlayerController playerController;

//    void Start()
//    {

//        initialPosition = transform.localPosition;
//        timeOffset = Random.Range(0f, Mathf.PI * 2);

//        playerController = FindFirstObjectByType<PlayerController>();

//        if (playerController == null)
//        {
//            Debug.LogError("No CharacterController found! Make sure your Player GameObject has one.");
//        }
//    }

//    void Update()
//    {
//        float time = Time.time + timeOffset;
//        float playerSpeed = GetPlayerSpeed();
//        float swingAmount = baseSwingAmount * (1 + playerSpeed * speedMultiplier);
//        float bounceAmount = baseBounceAmount * (1 + playerSpeed * speedMultiplier);
//        float swingSpeed = baseSwingSpeed * (1 + playerSpeed);
//        float bounceSpeed = baseBounceSpeed * (1 + playerSpeed);

//        float swayOffset = Mathf.Sin(time * swingSpeed) * swingAmount;

//        float bounceOffset = Mathf.Abs(Mathf.Cos(time * bounceSpeed)) * bounceAmount;

//        transform.localPosition = initialPosition + new Vector3(swayOffset, bounceOffset, 0);
//    }

//    float GetPlayerSpeed()
//    {
//        if (playerController != null)
//        {
//            return playerController.moveSpeed * Time.deltaTime;  
//        }
//        return 0f;
//    }
//}


using UnityEngine;

public class ArmSwingController : MonoBehaviour
{
    public float baseSwingAmount = 10f; // Increased for wider swing
    public float baseBounceAmount = 10f; // Increased for wider bounce
    public float baseSwingSpeed = 0.025f; // Decreased for slower swing
    public float baseBounceSpeed = 0.025f; // Decreased for slower bounce
    public float speedMultiplier = 0.1f;

    private Vector3 initialPosition;
    private float timeOffset;
    private PlayerController playerController;

    void Start()
    {
        initialPosition = transform.localPosition;
        timeOffset = Random.Range(0f, Mathf.PI * 2);
        playerController = FindFirstObjectByType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("No PlayerController found! Make sure your Player GameObject has one.");
        }
    }

    void Update()
    {
        float playerSpeed = GetPlayerSpeed();

        if (playerSpeed < 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
            return;
        }

        float time = Time.time + timeOffset;
        float swingAmount = baseSwingAmount * speedMultiplier;
        float bounceAmount = baseBounceAmount * speedMultiplier;
        float swingSpeed = baseSwingSpeed * playerSpeed;
        float bounceSpeed = baseBounceSpeed * playerSpeed;

        float swayOffset = Mathf.Sin(time * swingSpeed) * swingAmount;
        float bounceOffset = Mathf.Abs(Mathf.Sin(time * bounceSpeed)) * bounceAmount;

        Vector3 targetPosition = initialPosition + new Vector3(swayOffset, bounceOffset, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 5f);
    }

    float GetPlayerSpeed()
    {
        return playerController != null ? playerController.moveSpeed : 0f;
    }
}
