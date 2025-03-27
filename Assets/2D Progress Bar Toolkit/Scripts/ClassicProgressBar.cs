using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicProgressBar : MonoBehaviour 
{
    [Header("Colors")]
    [SerializeField] private Color m_MainColor = Color.white;
    [SerializeField] private Color m_FillColor = Color.green;
    
    [Header("General")]
    [SerializeField] private int m_NumberOfSegments = 1;
    [SerializeField] private float m_SizeOfNotch = 0.2f;
    [Range(0, 1f)] [SerializeField] private float m_FillAmount = 0.0f;

    private RectTransform m_RectTransform;
    private Image m_Image;
    private Image m_ProgressToFill;
    private float m_SizeOfSegment;
    private int numOfNotchesFilled = 0;
    private int currentTotalNotches = 0;

    public void Awake() 
    {
        // get rect transform
        m_RectTransform = GetComponent<RectTransform>();
        
        // get image
        m_Image = GetComponentInChildren<Image>();
        m_Image.color = m_MainColor;
        m_Image.gameObject.SetActive(false);
    }

    public void FillNotch()
    {
        if (currentTotalNotches == 0) return;
        
        numOfNotchesFilled += 1;
        m_ProgressToFill.fillAmount = (float)numOfNotchesFilled / currentTotalNotches;
    }

    public void ResetProgressBar(int numOfPuzzlesInLevel)
    {
		Debug.Log("here");
        // Clear any existing segments
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        currentTotalNotches = numOfPuzzlesInLevel;
        numOfNotchesFilled = 0;
        float notchSize = 1f / numOfPuzzlesInLevel;

        // count size of segments
        m_SizeOfSegment = m_RectTransform.sizeDelta.x / m_NumberOfSegments;        
        GameObject currentSegment = Instantiate(m_Image.gameObject, transform.position, Quaternion.identity, transform);
		Debug.Log(currentSegment);
        currentSegment.SetActive(true);

        Image segmentImage = currentSegment.GetComponent<Image>();
        segmentImage.fillAmount = m_SizeOfSegment;

        RectTransform segmentRectTransform = currentSegment.GetComponent<RectTransform>();
        segmentRectTransform.sizeDelta = new Vector2(m_SizeOfSegment, segmentRectTransform.sizeDelta.y);
        segmentRectTransform.anchoredPosition = Vector2.zero;

        // Create fill image if it doesn't exist
        if (segmentImage.transform.childCount == 0)
        {
			Debug.Log("in if");
            GameObject fillObj = new GameObject("Fill");
            fillObj.transform.SetParent(segmentImage.transform);
            m_ProgressToFill = fillObj.AddComponent<Image>();
            m_ProgressToFill.color = m_FillColor;
            RectTransform fillRect = fillObj.GetComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.sizeDelta = Vector2.zero;
            fillRect.anchoredPosition = Vector2.zero;
        }
        else
        {
			Debug.Log("in else");
            m_ProgressToFill = segmentImage.transform.GetChild(0).GetComponent<Image>();
        }

        m_ProgressToFill.fillAmount = 0f;
    }
}