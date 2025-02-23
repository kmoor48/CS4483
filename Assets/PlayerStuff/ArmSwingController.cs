using UnityEngine;

public class ArmSwingController : MonoBehaviour
{
    public float baseSwingAmount = 0.1f;  
    public float baseBounceAmount = 0.05f; 
    public float baseSwingSpeed = 3f;  
    public float baseBounceSpeed = 6f; 

    public float speedMultiplier = 1.5f;  

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
            Debug.LogError("No CharacterController found! Make sure your Player GameObject has one.");
        }
    }

    void Update()
    {
        float time = Time.time + timeOffset;
        float playerSpeed = GetPlayerSpeed();
        float swingAmount = baseSwingAmount * (1 + playerSpeed * speedMultiplier);
        float bounceAmount = baseBounceAmount * (1 + playerSpeed * speedMultiplier);
        float swingSpeed = baseSwingSpeed * (1 + playerSpeed);
        float bounceSpeed = baseBounceSpeed * (1 + playerSpeed);

        float swayOffset = Mathf.Sin(time * swingSpeed) * swingAmount;

        float bounceOffset = Mathf.Abs(Mathf.Cos(time * bounceSpeed)) * bounceAmount;

        transform.localPosition = initialPosition + new Vector3(swayOffset, bounceOffset, 0);
    }

    float GetPlayerSpeed()
    {
        if (playerController != null)
        {
            return playerController.moveSpeed * Time.deltaTime;  
        }
        return 0f;
    }
}
