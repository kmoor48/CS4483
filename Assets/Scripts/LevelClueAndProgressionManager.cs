using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LevelClueAndProgressionManager : MonoBehaviour
{
    // Storing list of clues per puzzle 
    private List<Dictionary<int, string[]>> cluesForLevel = new List<Dictionary<int, string[]>>();

    // Storing UI element that displays the clue
    private GameObject clueDisplay;
    private TextMeshProUGUI clueTextField;
    private TextMeshProUGUI clueNumTextField;

    // Tracking number of puzzles per level. index 0 -> level 1, index 1 -> level 2, index 2 -> level 3...etc.
    private int[] numOfPuzzlesPerLevel = {2, 2, 3, 4, 2};
    private int currentLevelInt = 0;
    private int currentPuzzleInt = 0;
    private int clueNum = 0;
    private int MAX_NUMBER_OF_CLUES = 3;

    void Start()
    {
        // Clue display manager
        clueDisplay = GameObject.FindWithTag("ClueDisplayPanel");
        clueTextField = clueDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        clueTextField.text = "No clue requested yet";
        clueNumTextField = clueDisplay.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();

        if (clueDisplay != null)
        {
            clueDisplay.SetActive(false);
        }
        else 
        {
            Debug.LogError("No clue display found");
        }

        for (int i = 0; i <= 5; i++)
        {
            cluesForLevel.Add(new Dictionary<int, string[]>());
        }

        /* Clues for Level 1 */
        Dictionary<int, string[]> level1Clues = new Dictionary<int, string[]>();
        cluesForLevel[0] = level1Clues;

        /* Clues for Level 2 */
        Dictionary<int, string[]> level2Clues = new Dictionary<int, string[]>();
        cluesForLevel[1] = level2Clues;

        /* Clues for Level 3 */
        Dictionary<int, string[]> level3Clues = new Dictionary<int, string[]>();
        cluesForLevel[2] = level3Clues;

        /* Clues for Level 4 */
        Dictionary<int, string[]> level4Clues = new Dictionary<int, string[]>();
        // Level 4 Puzzle 1 clues
        string[] level4Puzzle1Clues = {
            "Something lingers on what you carry. Perhaps a closer look could reveal a connection...",
            "Traces of blood remain on an item. Testing it might reveal its origin—compare it carefully with what's already here.",
            "Use the blood testing station to match the sample from your item with one of the four on the table. The right match may uncover the truth!"
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[0] = level4Puzzle1Clues;

        // Level 4 Puzzle 2 clues
        string[] level4Puzzle2Clues = {
            "The wall holds more than meets the eye. A little illumination might bring the truth to the surface.",
            "A simple glance won't show you everything. Try shedding some light on the poster—literally.",
            "Shine the flashlight on the poster—something hidden will be revealed in the glow."
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[1] = level4Puzzle2Clues;

        // Level 4 Puzzle 3a clues
        string[] level4Puzzle3aClues = {
            "It would seem like the poster somehow turned on the computer...",
            "Neuroscience holds the answer. Something from a previous room may be helpful here...",
            "Use the anatomy of the brain textbook you picked up in the Library to fill in the correct words"
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[2] = level4Puzzle3aClues;

        // Level 4 Puzzle 3b clues
        string[] level4Puzzle3bClues = {
            "Maybe something else on the computer will help you solve this.",
            "The letters don't mean what they seem. Are you sure you have all the items from the room?",
            "Use the cipher key to decode the mother's maiden name. The real name is the password you need."
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[3] = level4Puzzle3bClues;

        // Level 4 Puzzle 4 clues
        string[] level4Puzzle4Clues = {
            "A record is ready to play, but it's missing something essential. Maybe the answer is already in front of you.",
            "Did you miss something on the computer?",
            "The record player needs its pin to work. Check the computer screen for the code that unlocks it."
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[4] = level4Puzzle4Clues;

        // Level 4 Puzzle 5 clues
        string[] level4Puzzle5Clues = {
            "Listen closely. Not everything in the recording is just for atmosphere... some numbers might stand out.",
            "Pay attention to the numbers in the audio, they might unlock your way forward.",
            "The numbers read out at the start of the record playing is the key to unlocking the door code."
        };
        // Add the string array to the dictionary of level4Clues
        level4Clues[5] = level4Puzzle5Clues;

        cluesForLevel[3] = level4Clues;
    }

    public void SetCurrentLevel(int i)
    {
        currentLevelInt = i; // Update new level
        currentPuzzleInt = 0; // Reset which puzzle player is on bc new room
    }

    public void IncrementPuzzleCounter()
    {
        currentPuzzleInt += 1; // Update new puzzle
        clueNum = 0; // Reset the clue number the player is on for the new puzzle
    }

    public void IncrementClueNumber()
    {
        clueNum += 1;
    }

    public void GetClue()
    {
        // If the clue index is less than the maximum number of clues per puzzle (3)
        if (clueNum < MAX_NUMBER_OF_CLUES)
        {
            string clueToDisplay = cluesForLevel[currentLevelInt][currentPuzzleInt][clueNum];
            IncrementClueNumber();

            clueTextField.text = clueToDisplay;
            clueNumTextField.text = (clueNum).ToString() + "/3";

            OpenClueDisplay();
        }
    }

    public void OpenClueDisplay()
    {
        clueDisplay.SetActive(true);
    }

    public void CloseClueDisplay()
    {
        clueDisplay.SetActive(false);
    }
}
