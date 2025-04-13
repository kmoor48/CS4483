using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    private GameObject universalLogicHandler;
    MoveRuller _moveRull;
    OpenDrawer _drawer;
    private GameObject exitDoor;


    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
        _drawer = FindObjectOfType<OpenDrawer>(); // Reference to the drawer script
    }

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
        exitDoor = GameObject.FindWithTag("ExitDoor"); // Find the door by tag
    }


    private void OnPuzzleSolved()
    {
        if (exitDoor != null)
        {
            BoxCollider collider = exitDoor.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = true;
                exitDoor.GetComponent<AudioSource>().Play(); // Opening door sound
            }
        }
    }



    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
            }

            _drawer.UnlockDrawer(); // Unlock the drawer and remove the lock

            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();

            OnPuzzleSolved();
        }
    }
}
