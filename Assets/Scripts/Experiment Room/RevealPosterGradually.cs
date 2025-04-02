using UnityEngine;
using System.Collections;
using TMPro;

public class RevealPosterGradually : MonoBehaviour
{
    public GameObject otherPannel;
    public TextMeshPro originalPanelText;
    public GameObject computerPowerOnText;

    private float elapsedTime = 0f;
    private float fadeDuration = 5f; // Time in seconds
    private Material planeMaterialToReveal; // Material of the 3D plane
    private Color originalColorToReveal;
    private Material planeMaterialOG; // Material of the 3D plane
    private Color originalColorOG;
    private Color originalTextColor;
    private bool isPaused = false;

    private GameObject universalLogicHandler;

    void Start()
    {
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

        // Get the material of the 3D plane
        planeMaterialToReveal = GetComponent<Renderer>().material;
        originalColorToReveal = planeMaterialToReveal.color; // Store the original color

        // For the existing plane
        planeMaterialOG = otherPannel.GetComponent<Renderer>().material;
        originalColorOG = planeMaterialOG.color; // Store the original color

        // For the existing plane text
        originalTextColor = originalPanelText.color;

        // Start with full transparency
        planeMaterialToReveal.color = new Color(originalColorToReveal.r, originalColorToReveal.g, originalColorToReveal.b, 0f);
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            Color childColor = childRenderer.material.color;
            childRenderer.material.color = new Color(childColor.r, childColor.g, childColor.b, 0f);
        }


        // Start with full opacity
        planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, 1f);
        originalPanelText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 1f);
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
            // Fade all children materials
            foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
            {
                Color childColor = childRenderer.material.color;
                childRenderer.material.color = new Color(childColor.r, childColor.g, childColor.b, alphaNewPanel);
            }
    
            planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, alphaOriginalPanel); // fade out old plane
            originalPanelText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, alphaOriginalPanel);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure full visibility at the end
        planeMaterialToReveal.color = new Color(originalColorToReveal.r, originalColorToReveal.g, originalColorToReveal.b, 1f);
        planeMaterialOG.color = new Color(originalColorOG.r, originalColorOG.g, originalColorOG.b, 0f);
        originalPanelText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 0f);
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {   
            Material childMat = childRenderer.material;
            Color childColor = childMat.color;
            childMat.color = new Color(childColor.r, childColor.g, childColor.b, 1f);
            childMat.SetFloat("_Surface", 0); // 0 = Opaque, 1 = Transparent
            childMat.SetFloat("_AlphaClip", 1.0f);
            childMat.SetFloat("_Cutoff", 0.5f);
            // Make sure these blend modes are correctly set for Opaque
            childMat.SetFloat("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            childMat.SetFloat("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            // Enable ZWrite for opaque materials
            childMat.SetFloat("_ZWrite", 1);

            // Update render queue to default for opaque materials
            childMat.renderQueue = -1;
        }

        // Set the computer screen text on
        computerPowerOnText.SetActive(true);

        // Clue and Progression code
        LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
        clueScript.IncrementPuzzleCounter();
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
