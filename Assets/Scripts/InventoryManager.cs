using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
=======
using UnityEngine;
>>>>>>> 8275135 (add puzzle with foundation and text)

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private HashSet<string> inventory = new HashSet<string>();

<<<<<<< HEAD

=======
>>>>>>> 8275135 (add puzzle with foundation and text)
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

<<<<<<< HEAD

=======
>>>>>>> 8275135 (add puzzle with foundation and text)
    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log(itemName + " added to inventory!");
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log(itemName + " removed from inventory.");
        }
    }
}
