//using UnityEngine;

//public class ThrowPowder : MonoBehaviour
//{
//    public GameObject messageOnWall;
//    public Transform player;
//    public Transform wallPosition;
//    public Transform playerHand;
//    public GameObject throwText;
//    public float throwDistance = 2.0f;
//    public float throwForce = 5f;

//    private bool isNearWall = false;
//    private GameObject heldItem;

//    void Update()
//    {
//        float distance = Vector3.Distance(player.position, wallPosition.position);

//        if (distance <= throwDistance)
//        {
//            if (!isNearWall)
//            {
//                throwText.SetActive(true);
//                isNearWall = true;
//            }
//        }
//        else
//        {
//            if (isNearWall)
//            {
//                throwText.SetActive(false);
//                isNearWall = false;
//            }
//        }

//        // Press R to throw the item
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            ThrowItem();
//            throwText.SetActive(false);
//        }
//    }

//    void ThrowItem()
//    {
//        if (playerHand.childCount > 0)
//        {
//            Debug.Log("inside throw");
//            heldItem = playerHand.GetChild(0).gameObject;
//            heldItem.transform.SetParent(null);

//            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
//            if (rb == null)
//            {
//                rb = heldItem.AddComponent<Rigidbody>();
//            }

//            rb.isKinematic = false;
//            rb.useGravity = true;
//            rb.AddForce(player.forward * throwForce, ForceMode.Impulse);

//            Collider col = heldItem.GetComponent<Collider>();
//            if (col == null)
//            {
//                col = heldItem.AddComponent<BoxCollider>();
//            }
//            Debug.Log("inside here later");
//            heldItem.AddComponent<ThrowPowderCollision>().Setup(messageOnWall);

//            heldItem = null;
//        }
//        else
//        {
//            Debug.Log("No item in hand to throw.");
//        }
//    }
//}

//public class ThrowPowderCollision : MonoBehaviour
//{
//    private GameObject messageOnWall;

//    public void Setup(GameObject message)
//    {
//        Debug.Log("message"+ message);
//        messageOnWall = message;
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Wall")) 
//        {
//            Debug.Log("Powder hit the wall!");
//            messageOnWall.SetActive(true); 
//            Destroy(gameObject, 0.5f);
//        }
//    }
//}

using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    public Transform player;
    public Transform wallPosition;
    public Transform playerHand;
    public GameObject throwText;
    public float throwDistance = 2.0f;
    public float throwForce = 5f;

    private bool isNearWall = false;
    private GameObject heldItem;

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

        // Press R to throw the item
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
            Debug.Log("inside throw");
            heldItem = playerHand.GetChild(0).gameObject;
            heldItem.transform.SetParent(null);

            // Ensure Rigidbody and Collider components are attached before throwing
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
                col = heldItem.AddComponent<BoxCollider>(); // Default to BoxCollider if no collider exists
            }

            // Add collision handler for the thrown powder
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

    public void Setup(GameObject message)
    {
        Debug.Log("message" + message);
        messageOnWall = message;
        messageOnWall.SetActive(true);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("inside collisionnnnnnn");
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Powder hit the wall!");
            messageOnWall.SetActive(true);
            Destroy(gameObject, 0.5f); // Destroy the thrown item after 0.5 seconds
        }
    }
}

