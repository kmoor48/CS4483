using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    private Transform player;
    public Transform wallPosition;
    private Transform playerHand;
    public GameObject throwText;
    public float throwDistance = 2.0f;
    public float throwForce = 5f;

    private bool isNearWall = false;
    private GameObject heldItem;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerHand = GameObject.FindWithTag("PlayerRightHandTarget").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, wallPosition.position);

        if (distance <= throwDistance)
        {
            if (!isNearWall)
            {
                throwText.SetActive(true);
                isNearWall = true;
            }
        }
        else
        {
            if (isNearWall)
            {
                throwText.SetActive(false);
                isNearWall = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ThrowItem();
            throwText.SetActive(false);
        }
    }

    void ThrowItem()
    {
        if (playerHand.childCount > 0)
        {
            heldItem = playerHand.GetChild(0).gameObject;
            heldItem.transform.SetParent(null);

            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = heldItem.AddComponent<Rigidbody>();
            }

            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(player.forward * throwForce, ForceMode.Impulse);

            Collider col = heldItem.GetComponent<Collider>();
            if (col == null)
            {
                col = heldItem.AddComponent<BoxCollider>(); 
            }

            ThrowPowderCollision powderCollision = heldItem.AddComponent<ThrowPowderCollision>();
            powderCollision.Setup(messageOnWall);

            heldItem = null;
        }
        else
        {
            Debug.Log("No item in hand to throw.");
        }
    }
}

public class ThrowPowderCollision : MonoBehaviour
{
    private GameObject messageOnWall;
    private GameObject universalLogicHandler;

    public void Setup(GameObject message)
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        messageOnWall = message;
        Debug.Log(messageOnWall);
        messageOnWall.SetActive(true);
        LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        clueScript.IncrementPuzzleCounter();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            messageOnWall.SetActive(true);
            Destroy(gameObject, 0.5f); 
        }
    }
}

