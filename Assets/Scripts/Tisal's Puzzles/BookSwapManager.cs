using UnityEngine;
using System.Collections.Generic;

public class BookSwapManager : MonoBehaviour
{
    private Book firstSelectedBook = null;
    private Book secondSelectedBook = null;
    private Dictionary<Book, Vector3> bookPositions = new Dictionary<Book, Vector3>();
    private GameObject universalLogicHandler;


    // Define correct positions for books
    private Dictionary<Vector3, string> correctPositions = new Dictionary<Vector3, string>
{
    { new Vector3(-38.33f, 8.82f, -19.73f), "Book-4" },
    { new Vector3(-38.16f, 8.82f, -19.73f), "Book-3" },
    { new Vector3(-38.01f, 8.82f, -19.73f), "Book-1" },
    { new Vector3(-37.86f, 8.82f, -19.73f), "Book-2" },
    { new Vector3(-37.70f, 8.82f, -19.73f), "Book-5" }
};


    void Start()
    {
        Book[] books = FindObjectsOfType<Book>();  // Find all books in the scene
        foreach (Book book in books)
        {
            bookPositions[book] = book.transform.position;
        }
        universalLogicHandler = GameObject.FindWithTag("UniversalLogicHandler");

    }

    // Select a book when clicked
    public void SelectBook(Book book)
    {
        if (firstSelectedBook == null)
        {
            firstSelectedBook = book;
            book.HighlightBook();
        }
        else if (secondSelectedBook == null && book != firstSelectedBook)
        {
            secondSelectedBook = book;
            SwapBooks();
        }
    }

    // Swap the positions of two selected books
    private void SwapBooks()
    {
        Vector3 tempPos = firstSelectedBook.transform.position;
        firstSelectedBook.transform.position = secondSelectedBook.transform.position;
        secondSelectedBook.transform.position = tempPos;

        // Update positions in the dictionary
        bookPositions[firstSelectedBook] = firstSelectedBook.transform.position;
        bookPositions[secondSelectedBook] = secondSelectedBook.transform.position;

        firstSelectedBook.DeselectBook();
        secondSelectedBook.DeselectBook();

        // Reset selection
        firstSelectedBook = null;
        secondSelectedBook = null;

        // Check if the puzzle is solved
        CheckPuzzleCompletion();
    }

    // Check if the books are in the correct order
    private void CheckPuzzleCompletion()
    {
        bool isSolved = true;
        Book specialBook = null;

        foreach (var pair in bookPositions)
        {
            string correctBook = GetCorrectBookForPosition(pair.Value);

            if (correctBook == null || pair.Key.gameObject.name != correctBook)
            {
                isSolved = false;
            }
            else if (correctBook == "Book-3") // If "Book-3" is at its correct position
            {
                specialBook = pair.Key;
            }
        }

        if (isSolved && specialBook != null)
        {
            Debug.Log("Puzzle Solved! Middle book is now clickable.");
            specialBook.MakeSpecialBook();
            // Mark the puzzle as complete
            LevelClueAndProgressionManager clueScript = universalLogicHandler.GetComponent<LevelClueAndProgressionManager>();
            clueScript.IncrementPuzzleCounter();
        }
        else
        {
            Debug.Log("Puzzle Not Solved Yet.");
        }
    }




    // Get the correct book for a specific position
    private string GetCorrectBookForPosition(Vector3 position)
    {
        float threshold = 0.1f; // Increased threshold to allow for more margin of error

        foreach (var entry in correctPositions)
        {
            if (Vector3.Distance(position, entry.Key) < threshold)
            {
                return entry.Value;
            }
        }

        Debug.LogError($"No correct book found for position: {position}");
        return null;
    }

}
