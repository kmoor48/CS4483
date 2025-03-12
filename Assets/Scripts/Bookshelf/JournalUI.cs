using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalUI : MonoBehaviour
{
    public static JournalUI Instance;

    public GameObject journalPanel;
    public TextMeshProUGUI journalText;
    public Button nextButton, prevButton;

    private string[] pages =
    {
        "BP Cover - Dr. Pale's Research Journal",
        "September 10th, 2006:\r\nThey think I'm too obsessed. Too fixated on Lucy's case. But how could I not be? She’s the key to everything — her condition is unlike anything I've ever seen. My peers are too blind to see the potential here. They want to take her away from me, pull me off the case, and for what? To preserve their precious reputations? They say it’s “unethical” to focus so much on one patient, but they don't understand. She is the breakthrough. She could be the one who changes everything — and I will not let her slip through my fingers.",
        "June 23rd, 2006: I write to you from the Alberta Rockies.",
        "July 10th, 2006: This heat in Ontario is unbearable.",
        "August 2nd, 2006: Finally made it to the Pacific Ocean, BC is stunning.",
        "September 5th, 2006: They’ll regret taking Lucy from me. I will prove them wrong."
    };

    private int currentPage = 0;

    void Awake()
    {
        Instance = this;
        journalPanel.SetActive(false);
    }

    void Update()
    {
        // Navigate with arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevPage();
        }

        // Close the journal with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseJournal();
        }
    }

    public void ShowJournal()
    {
        currentPage = 0;
        UpdatePage();
        journalPanel.SetActive(true);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        journalText.text = pages[currentPage];
        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < pages.Length - 1;
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
    }
}
