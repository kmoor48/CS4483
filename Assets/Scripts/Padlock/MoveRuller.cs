using System.Collections.Generic;
using UnityEngine;

public class MoveRuller : MonoBehaviour
{
    PadLockPassword _lockPassword;

    [HideInInspector] public List<GameObject> _rullers = new List<GameObject>();
    private int _changeRuller = 0;
    [HideInInspector] public int[] _numberArray = { 0, 0, 0, 0 };

    private int _numberRuller = 0;
    private bool _lockActive = false;

    public Camera mainCamera;
    public Camera lockCamera;

    void Awake()
    {
        _lockPassword = FindObjectOfType<PadLockPassword>();

        _rullers.Add(GameObject.Find("Ruller1"));
        _rullers.Add(GameObject.Find("Ruller2"));
        _rullers.Add(GameObject.Find("Ruller3"));
        _rullers.Add(GameObject.Find("Ruller4"));

        foreach (GameObject r in _rullers)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }

    void Update()
    {
        if (_lockActive)
        {
            MoveRulles();
            RotateRullers();
            _lockPassword.Password();

            if (Input.GetKeyDown(KeyCode.Escape)) // Press Escape to exit lock interaction
            {
                ExitLockInteraction();
            }
        }
    }

    public void StartLockInteraction()
    {
        _lockActive = true;
        mainCamera.gameObject.SetActive(false);
        lockCamera.gameObject.SetActive(true);
    }


    public void ExitLockInteraction()
    {
        // Switch back to main camera or UI when exiting the lock screen
        Debug.Log("Exiting Lock Interaction");
    }


    void MoveRulles()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _changeRuller++;
            _numberRuller = (_numberRuller + 1) % 4;
            BlinkSelectedRuller();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _changeRuller--;
            _numberRuller = (_numberRuller - 1 + 4) % 4;
            BlinkSelectedRuller();
        }
    }

    void RotateRullers()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rullers[_changeRuller].transform.Rotate(-36, 0, 0, Space.Self);
            _numberArray[_changeRuller] = (_numberArray[_changeRuller] + 1) % 10;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rullers[_changeRuller].transform.Rotate(36, 0, 0, Space.Self);
            _numberArray[_changeRuller] = (_numberArray[_changeRuller] - 1 + 10) % 10;
        }
    }

    void BlinkSelectedRuller()
    {
        for (int i = 0; i < _rullers.Count; i++)
        {
            var emission = _rullers[i].GetComponent<PadLockEmissionColor>();
            emission._isSelect = (i == _changeRuller);
            emission.BlinkingMaterial();
        }
    }
}
