using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProvinceButton : MonoBehaviour, IPointerClickHandler
{
    public string provinceName;
    public MapPuzzleManager puzzleManager;

    private Image provinceImage;
    private Color defaultColor;
    private Color highlightColor = new Color(1f, 0.8f, 0.2f); // Yellow highlight

    void Awake()
    {
        provinceImage = GetComponent<Image>();
        defaultColor = provinceImage.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        puzzleManager.ProvinceSelected(this);
    }

    public void SetHighlighted(bool highlighted)
    {
        provinceImage.color = highlighted ? highlightColor : defaultColor;
    }
}