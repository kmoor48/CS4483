using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;
    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            // Password is correct, unlock the drawer
            Debug.Log("Password correct");

            // Find the drawer lock and unlock it
            DrawerLock drawerLock = FindObjectOfType<DrawerLock>(); // Assuming there's only one DrawerLock in the scene
            if (drawerLock != null)
            {
                drawerLock.UnlockDrawer();
            }

            // Optionally disable the blinking material or other visual effects after unlocking
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
            }
        }
    }
}
