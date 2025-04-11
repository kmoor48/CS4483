
using UnityEngine;

public class ArmSwingController : MonoBehaviour
{
    public float baseSwingAmount = 10f; 
    public float baseBounceAmount = 10f; 
    public float baseSwingSpeed = 0.025f; 
    public float baseBounceSpeed = 0.025f; 
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
