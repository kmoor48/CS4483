//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace NavKeypad { 
//public class KeypadInteractionFPV : MonoBehaviour
//{
//    private Camera cam;
//    private void Awake() => cam = Camera.main;
//    private void Update()
//    {
//        var ray = cam.ScreenPointToRay(Input.mousePosition);

//        if (Input.GetMouseButtonDown(0))
//        {
//            if (Physics.Raycast(ray, out var hit))
//            {
//                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
//                {
//                    keypadButton.PressButton();
//                }
//            }
//        }
//    }
//}
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        [SerializeField] private Camera customCamera; // Assign your camera in the Inspector

        private void Update()
        {
            // Skip if no camera is assigned
            if (customCamera == null) return;

            var ray = customCamera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                    {
                        keypadButton.PressButton();
                    }
                }
            }
        }
    }
}