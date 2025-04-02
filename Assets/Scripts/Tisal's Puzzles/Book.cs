using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
    private BookSwapManager manager;
    private Renderer bookRenderer;
    private Color originalColor;
    private bool isSelected = false;
    private bool isSpecialBook = false;

    [Header("Special Book Settings")]
    [SerializeField] private float hoverHeight = 0.1f;
    [SerializeField] private float hoverSpeed = 1.0f;
    [SerializeField] private Color specialBookColor = new Color(1f, 0.5f, 0f, 1f); // Orange color

    // For the glow effect
    [SerializeField] private GameObject glowEffectPrefab;
    private GameObject activeGlowEffect;

    private Vector3 originalPosition;
    private bool isHovering = false;

    void Start()
    {
        manager = FindObjectOfType<BookSwapManager>();
        bookRenderer = GetComponent<Renderer>();
        originalColor = bookRenderer.material.color;
        originalPosition = transform.position;
    }

    void Update()
    {
        // Animate the special book
        if (isSpecialBook && isHovering)
        {
            // Apply hovering effect
            float newY = originalPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight * 0.5f;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    void OnMouseDown()
    {
        if (isSpecialBook)
        {
            OpenJournal();
        }
        else if (manager != null)
        {
            manager.SelectBook(this);
        }
    }

    void OnMouseEnter()
    {
        // Optional: Subtle effect when mouse hovers over any book
        if (!isSelected && !isSpecialBook)
        {
            bookRenderer.material.color = Color.Lerp(originalColor, Color.white, 0.3f);
        }
    }

    void OnMouseExit()
    {
        // Reset hover effect
        if (!isSelected && !isSpecialBook)
        {
            bookRenderer.material.color = originalColor;
        }
    }

    public void HighlightBook()
    {
        isSelected = true;
        bookRenderer.material.color = Color.cyan;
    }

    public void DeselectBook()
    {
        isSelected = false;
        bookRenderer.material.color = originalColor;
    }

    public void MakeSpecialBook()
    {
        isSpecialBook = true;
        StartCoroutine(ActivateSpecialBook());
    }

    private IEnumerator ActivateSpecialBook()
    {
        // Transition to special state
        float duration = 1.0f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Color transition
            bookRenderer.material.color = Color.Lerp(originalColor, specialBookColor, t);

            yield return null;
        }

        // Create glow effect
        CreateGlowEffect();

        // Start the hover effect
        isHovering = true;
    }

    private void CreateGlowEffect()
    {
        // Method 1: Use a prefab (recommended)
        if (glowEffectPrefab != null)
        {
            activeGlowEffect = Instantiate(glowEffectPrefab, transform.position, transform.rotation, transform);
            activeGlowEffect.transform.localScale = transform.localScale * 1.05f; // Slightly larger
        }
        // Method 2: Create a point light
        else
        {
            GameObject lightObj = new GameObject("BookGlow");
            lightObj.transform.SetParent(transform);
            lightObj.transform.localPosition = Vector3.zero;

            Light glowLight = lightObj.AddComponent<Light>();
            glowLight.type = LightType.Point;
            glowLight.color = specialBookColor;
            glowLight.intensity = 1.5f;
            glowLight.range = 1f;

            activeGlowEffect = lightObj;
        }
    }

    private void OpenJournal()
    {
        if (JournalUI.Instance != null)
        {
            JournalUI.Instance.ShowJournal();
        }
        else
        {
            Debug.LogError("JournalUI.Instance is null! Make sure the JournalUI script is in the scene.");
        }
    }

    public void ResetBook()
    {
        isSpecialBook = false;
        isSelected = false;
        isHovering = false;
        bookRenderer.material.color = originalColor;
        transform.position = originalPosition;

        if (activeGlowEffect != null)
        {
            Destroy(activeGlowEffect);
        }
    }
}