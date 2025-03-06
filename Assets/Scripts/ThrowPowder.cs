//using UnityEngine;

//public class ThrowPowder : MonoBehaviour
//{
<<<<<<< HEAD
//    public GameObject messageOnWall;
//    public Transform player;
//    public Transform wallPosition;
//    public float throwDistance = 3.0f;

//    void Update()
//    {
//        if (Input.GetKey(KeyCode.T))
//        {
//            Debug.Log("T was pressed!");

//            float distance = Vector3.Distance(player.position, wallPosition.position);
//            Debug.Log("Distance to wall: " + distance);

//            if (distance <= throwDistance)
//            {
//                if (InventoryManager.Instance.HasItem("Foundation_Powder"))
//                {
//                    Debug.Log("Throwing Powder...");
//                    InventoryManager.Instance.RemoveItem("Foundation_Powder");
//                    InventoryManager.Instance.AddItem("Powder Used");
//                    messageOnWall.SetActive(true);
//                }
//                else
//                {
//                    Debug.Log("You don't have the foundation powder!");
//                }
//            }
//            else
//            {
//                Debug.Log("Too far from the wall!");
//            }
=======
//    public GameObject messageOnWall; // Reference to the hidden message object
//    public Transform wallPosition;   // Position where powder is thrown

//    void Update()
//    {
//        if (InventoryManager.Instance.HasItem("Foundation Powder") && Input.GetKeyDown(KeyCode.E))
//        {
//            Debug.Log("Throwing Powder...");
//            InventoryManager.Instance.AddItem("Powder Used"); // Track that powder is used
//            messageOnWall.SetActive(true); // Reveal the message
>>>>>>> 8275135 (add puzzle with foundation and text)
//        }
//    }
//}

<<<<<<< HEAD

=======
>>>>>>> 8275135 (add puzzle with foundation and text)
using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    public Transform player;
    public Transform wallPosition;
<<<<<<< HEAD
    public float throwDistance = 2.0f;
    public GameObject throwText; 

    private bool isNearWall = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, wallPosition.position);

        if (distance <= throwDistance)
        {
            if (!isNearWall) 
            {
                throwText.gameObject.SetActive(true);
                isNearWall = true;
            }
        }
        else
        {
            if (isNearWall)
            {
                throwText.gameObject.SetActive(false);
                isNearWall = false;
            }
        }

        if (isNearWall && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T was pressed!");

            if (InventoryManager.Instance.HasItem("Foundation_Powder"))
            {
                Debug.Log("Throwing Powder...");
                InventoryManager.Instance.RemoveItem("Foundation_Powder");
                InventoryManager.Instance.AddItem("Powder Used");
                messageOnWall.SetActive(true);
                throwText.gameObject.SetActive(false); 
            }
            else
            {
                Debug.Log("You don't have the foundation powder!");
=======
    public float throwDistance = 3.0f; // Adjust as needed

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E was pressed!");

            float distance = Vector3.Distance(player.position, wallPosition.position);
            Debug.Log("Distance to wall: " + distance);

            if (distance <= throwDistance)
            {
                if (InventoryManager.Instance.HasItem("Foundation_Powder"))
                {
                    Debug.Log("Throwing Powder...");
                    InventoryManager.Instance.RemoveItem("Foundation_Powder");
                    InventoryManager.Instance.AddItem("Powder Used");
                    messageOnWall.SetActive(true);
                }
                else
                {
                    Debug.Log("You don't have the foundation powder!");
                }
            }
            else
            {
                Debug.Log("Too far from the wall!");
>>>>>>> 8275135 (add puzzle with foundation and text)
            }
        }
    }
}
