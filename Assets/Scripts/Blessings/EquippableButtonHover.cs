using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippableButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Equippable attachedEquippable;
    public GameObject descriptionUI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Button thisButton;
    public TextMeshProUGUI buttonText;
    
    [Header("Effect")]
    public float speedToMove;
    public Vector2 offset;
    public RectTransform rectTransform;
    public Vector2 targetPosition;
    private bool checkHovering;
    
    private void Update()
    {
        if (checkHovering)
        {
            if (rectTransform.anchoredPosition != targetPosition)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, speedToMove * Time.deltaTime);
            }
            else
            {
                checkHovering = false;
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionUI.SetActive(true);
        descriptionText.text = attachedEquippable.description;
        nameText.text = attachedEquippable.ID;
        targetPosition = Vector2.zero + offset;
        checkHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionUI.SetActive(false);
        targetPosition = Vector2.zero;
        checkHovering = true;
    }
}
