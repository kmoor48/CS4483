using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;
    OpenDrawer _drawer;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
        _drawer = FindObjectOfType<OpenDrawer>(); // Reference to the drawer script
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
        }
    }
}
