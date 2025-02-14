using UnityEngine;

public class DrawerUnlocker : MonoBehaviour
{
    private bool isUnlocked = false; // Is the drawer unlocked?

    public GameObject drawer; // Reference to the drawer GameObject

    // Call this method when the password is correct
    public void UnlockDrawer()
    {
        isUnlocked = true;
        Debug.Log("Drawer unlocked!");

        // Perform any additional unlocking animation or actions here
        OpenDrawer();
    }

    // Method to open the drawer (called when unlocked)
    private void OpenDrawer()
    {
        if (isUnlocked)
        {
            // Example action: Moving the drawer to an open position (can be adjusted)
            Vector3 openPosition = new Vector3(drawer.transform.position.x, drawer.transform.position.y + 0.5f, drawer.transform.position.z);
            drawer.transform.position = Vector3.Lerp(drawer.transform.position, openPosition, Time.deltaTime * 2f); // Smooth opening
        }
    }

    // Optional: Method to check if the drawer is unlocked
    public bool IsDrawerUnlocked()
    {
        return isUnlocked;
    }
}
