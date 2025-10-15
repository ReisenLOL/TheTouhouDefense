using TMPro;
using UnityEngine;

public class BlessingIcon : MonoBehaviour
{
    public Blessing attachedBlessing;
    public TextMeshProUGUI nameTextPlaceholder;
    public TextMeshProUGUI amountTextPlaceholder;

    public void RefreshIcon()
    {
        amountTextPlaceholder.text = attachedBlessing.stackAmount.ToString();
    }
}
