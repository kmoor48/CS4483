using UnityEngine;

public class SampleGlassCollisionDetection : MonoBehaviour
{
    private bool isHoveringOverSampleGlass = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Glass_medical_dropper")
        {
            isHoveringOverSampleGlass = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Glass_medical_dropper")
        {
            isHoveringOverSampleGlass = false;
        }
    }

    public bool IsSampleGlassHoveredOver()
    {
        return isHoveringOverSampleGlass;
    }
}
