using UnityEngine;

public class CameraRoomSwitcher : MonoBehaviour
{
    public Transform[] roomPositions; // Assign room transforms in Unity Inspector
    private int currentRoomIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToNextRoom();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToPreviousRoom();
        }
    }

    void MoveToNextRoom()
    {
        if (currentRoomIndex < roomPositions.Length - 1)
        {
            currentRoomIndex++;
            MoveCamera();
        }
    }

    void MoveToPreviousRoom()
    {
        if (currentRoomIndex > 0)
        {
            currentRoomIndex--;
            MoveCamera();
        }
    }

    //void MoveCamera()
    //{
    //    transform.position = roomPositions[currentRoomIndex].position;
    //}
    void MoveCamera()
    {
        transform.SetParent(roomPositions[currentRoomIndex]);
        transform.localPosition = Vector3.zero; // Reset to center of the room
        transform.SetParent(null); // Unparent after setting position
    }
}

