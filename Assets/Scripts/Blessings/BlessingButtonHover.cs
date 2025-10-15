using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlessingButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Blessing attachedBlessing;
    public GameObject descriptionUI;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionUI.SetActive(true);
        descriptionText.text = attachedBlessing.blessingDescription;
        nameText.text = attachedBlessing.name;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionUI.SetActive(false);
    }
}
