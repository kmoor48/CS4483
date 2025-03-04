using UnityEngine;

public class PadLockEmissionColor : MonoBehaviour
{
    public bool _isSelect = false;
    private Material _material;
    private Color _defaultColor;
    private Color _highlightColor = Color.yellow; // Change as needed

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    private void Update()
    {
        if (_isSelect)
        {
            BlinkingMaterial();
        }
        else
        {
            _material.color = _defaultColor; // Ensure it's not blinking when not selected
        }
    }

    public void BlinkingMaterial()
    {
        _material.color = _highlightColor;
    }
}
