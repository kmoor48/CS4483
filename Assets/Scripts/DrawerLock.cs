using UnityEngine;

public class DrawerLock : MonoBehaviour
{
    public GameObject lockObject;  // Reference to the lock object (make sure it's a child of the drawer)
    public bool isLocked = true;  // Start with the drawer locked
    public GameObject drawerObject; // Reference to the drawer that will open when unlocked

    private void Start()
    {
        // Initially show the lock when the drawer is locked
        lockObject.SetActive(isLocked);
    }

    // Method to unlock the drawer
    public void UnlockDrawer()
    {
        if (isLocked)
        {
            isLocked = false;
            lockObject.SetActive(false);  // Hide the lock when the drawer is unlocked
            OpenDrawer();  // Open the drawer after unlocking
        }
    }

    // Method to open the drawer (could be animation or movement)
    private void OpenDrawer()
    {
        // Logic to open the drawer (e.g., move it, play an animation, etc.)
        drawerObject.transform.Translate(Vector3.right * 2f);  // Move the drawer to the right as an example
        // Add your animation or movement logic here
    }
}
