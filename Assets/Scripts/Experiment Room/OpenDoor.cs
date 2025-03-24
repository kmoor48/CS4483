using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;

    public void OpenTheDoors()
    {
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
    }
}
