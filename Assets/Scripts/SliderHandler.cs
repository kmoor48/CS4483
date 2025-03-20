using UnityEngine;
using UnityEngine.UI;  // For UI components like Slider

public class SliderHandler : MonoBehaviour
{
    public Slider mySlider;  // Reference to the Slider

    void Start()
    {
        // Optionally initialize the slider here
        if (mySlider != null)
        {
            mySlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    public void OnSliderValueChanged(float value)
    {
        // Code to execute when the slider value changes
        Debug.Log("Slider Value: " + value);
    }
}
