using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    public Transform player;
    public Transform wallPosition;
    public float throwDistance = 3.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("T was pressed!");

            float distance = Vector3.Distance(player.position, wallPosition.position);
            Debug.Log("Distance to wall: " + distance);

            if (distance <= throwDistance)
            {
                if (true/*InventoryManager.Instance.HasItem("Foundation_Powder")*/)
                {
                    Debug.Log("Throwing Powder...");
                    //InventoryManager.Instance.RemoveItem("Foundation_Powder");
                    //InventoryManager.Instance.AddItem("Powder Used");
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
