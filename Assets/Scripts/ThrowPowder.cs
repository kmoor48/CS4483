using UnityEngine;

public class ThrowPowder : MonoBehaviour
{
    public GameObject messageOnWall;
    public Transform player;
    public Transform wallPosition;
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
            }
        }
    }
}
