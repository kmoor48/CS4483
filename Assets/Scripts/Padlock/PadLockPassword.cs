using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    private GameObject universalLogicHandler;
    MoveRuller _moveRull;
    OpenDrawer _drawer;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
        _drawer = FindObjectOfType<OpenDrawer>(); // Reference to the drawer script
    }

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
            }

            _drawer.UnlockDrawer(); // Unlock the drawer and remove the lock

            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();
        }
    }
}
