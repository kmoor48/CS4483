using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Color mainColor;
    public Color fillColor;

    private float fillAmountPerPuzzle;

    private Image fill; 

    void Start()
    {
        fill = transform.GetChild(0).GetChild(0).GetComponent<Image>();

        if (fill == null)
        {
            Debug.Log("No Image found on Fill in Progress Bar");
        }

        // Setting the components as active
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    public void IncrementProgressBar()
    {
        if (fill.fillAmount == 1f)
        {
            Debug.Log("Progress bar is full!");
        }
        else 
        {
            fill.fillAmount = fill.fillAmount + fillAmountPerPuzzle;
        }
    }

    public void ResetProgressBar(int numOfPuzzlesInLevel)
    {
        if (fill == null)
        {
            fill = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        }
        fillAmountPerPuzzle = 1f / numOfPuzzlesInLevel;
        fill.fillAmount = 0f;
    }
}
