using UnityEngine;
using UnityEngine.UI;  

public class SliderHandler : MonoBehaviour
{
    public Slider mySlider; 

    void Start()
    {
        if (mySlider != null)
        {
            mySlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    public void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider Value: " + value);
    }
}
