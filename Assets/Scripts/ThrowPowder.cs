//using UnityEngine;

//public class ThrowPowder : MonoBehaviour
//{
//    public GameObject messageOnWall; // Reference to the hidden message object
//    public Transform wallPosition;   // Position where powder is thrown

//    void Update()
//    {
//        if (InventoryManager.Instance.HasItem("Foundation Powder") && Input.GetKeyDown(KeyCode.E))
//        {
//            Debug.Log("Throwing Powder...");
//            InventoryManager.Instance.AddItem("Powder Used"); // Track that powder is used
//            messageOnWall.SetActive(true); // Reveal the message
//        }
//    }
//}

using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    public Transform player;
    public Transform wallPosition;
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
            }
        }
    }
}
