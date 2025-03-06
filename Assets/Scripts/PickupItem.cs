using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    public GameObject openText;

    private bool playerInRange = false;

    void Start()
    {
        openText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openText.SetActive(false);
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(itemName + " picked up!");

            InventoryManager.Instance.AddItem(itemName);

            openText.SetActive(false);
            Destroy(gameObject);
        }
    }
}

