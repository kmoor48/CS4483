using UnityEngine;
using System.Collections;

public class RevealPosterGradually : MonoBehaviour
{
    public GameObject otherPannel;

    private float elapsedTime = 0f;
    private float fadeDuration = 5f; // Time in seconds
    private Material planeMaterialToReveal; // Material of the 3D plane
    private Color originalColorToReveal;
    private Material planeMaterialOG; // Material of the 3D plane
    private Color originalColorOG;
    private bool isPaused = false;

    void Start()
    {
        // Get the material of the 3D plane
        planeMaterialToReveal = GetComponent<Renderer>().material;
        originalColorToReveal = planeMaterialToReveal.color; // Store the original color

        // For the existing plane
        planeMaterialOG = otherPannel.GetComponent<Renderer>().material;
        originalColorOG = planeMaterialOG.color; // Store the original color

        // Start with full transparency
        planeMaterialToReveal.color = new Color(originalColorToReveal.r, originalColorToReveal.g, originalColorToReveal.b, 0f);

        // Start with full opacity
        planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, 1f);
    }

    IEnumerator FadeInPlane()
    {
        while (elapsedTime < fadeDuration)
        {
            // Pause the coroutine if isPaused is true
            while (isPaused)
            {
                yield return null; // Wait until isPaused is false
            }

            // Lerp the alpha value from 0 to 1
            float alphaNewPanel = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            float alphaOriginalPanel = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            planeMaterialToReveal.color = new Color(originalColorToReveal.r, originalColorToReveal.g, originalColorToReveal.b, alphaNewPanel); // fade in new plane
            planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, alphaOriginalPanel); // fade out old plane

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure full visibility at the end
        planeMaterialToReveal.color = new Color(originalColorToReveal.r, originalColorToReveal.g, originalColorToReveal.b, 1f);
        planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, 0f);
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInPlane());
    }

    public void PauseFadeIn()
    {
        isPaused = true;
    }

    public void UnPauseFadeIn()
    {
        isPaused = false;
    }
}
