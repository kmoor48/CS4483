using UnityEngine;

public class Book : MonoBehaviour
{
    private BookSwapManager manager;
    private Renderer bookRenderer;
    private Color originalColor;
    private bool isSelected = false;
    private bool isSpecialBook = false;

    void Start()
    {
        manager = FindObjectOfType<BookSwapManager>();
        bookRenderer = GetComponent<Renderer>();
        originalColor = bookRenderer.material.color;
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
        bookRenderer.material.color = Color.red;
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
}
