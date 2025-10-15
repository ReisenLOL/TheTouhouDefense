using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlessingSelector : MonoBehaviour
{
    #region Statication
    public static BlessingSelector instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public List<Blessing> blessingsList = new();
    // players get a list of blessings purchasable, can refresh for power. random insertion of each tier. 
    public GameObject blessingSelectorUI;
    public Transform blessingSelectorGrid;

    public float baseRefreshCost;
    public float currentRefreshCost;
    public TextMeshProUGUI refreshText;
    public int optionAmount;
    [Header("CACHE")]
    public PlayerController player;
    public BlessingButtonHover templateButton;
    public BlessingIcon templateIcon;
    public Transform blessingListUI;
    public List<BlessingIcon> allIcons = new();

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        currentRefreshCost = baseRefreshCost;
    }

    public void SetBlessingSelectorUI()
    {
        blessingSelectorUI.gameObject.SetActive(!blessingSelectorUI.activeSelf);
    }

    private void SelectBlessing(Blessing selectedBlessing, GameObject buttonSelection = null)
    {
        if (!ResourceManager.instance.RemovePower(selectedBlessing.cost))
        {
            return;
        }

        bool playerHasBlessing = false;
        foreach (Blessing foundBlessing in player.equippedBlessings)
        {
            if (foundBlessing.blessingID == selectedBlessing.blessingID)
            {
                foundBlessing.stackAmount++;
                foundBlessing.ApplyBlessing();
                foreach (BlessingIcon blessingIcon in allIcons)
                {
                    blessingIcon.RefreshIcon();
                }
                playerHasBlessing = true;
                break;
            }
        }
        if (!playerHasBlessing)
        {
            Blessing newBlessing = Instantiate(selectedBlessing, player.transform);
            player.equippedBlessings.Add(newBlessing);
            newBlessing.player = player;
            newBlessing.ApplyBlessing();
            BlessingIcon newIcon = Instantiate(templateIcon, blessingListUI);
            newIcon.gameObject.SetActive(true);
            newIcon.attachedBlessing = newBlessing;
            newIcon.nameTextPlaceholder.text = selectedBlessing.blessingID;
            newIcon.amountTextPlaceholder.text = selectedBlessing.stackAmount.ToString();
            allIcons.Add(newIcon);
        }
        if (buttonSelection)
        {
            Destroy(buttonSelection);
        }
    }

    public void RefreshList(bool firstTime = false)
    {
        if (!firstTime && !ResourceManager.instance.RemovePower(currentRefreshCost))
        {
            return;
        }
        refreshText.text = $"Refresh (P: {currentRefreshCost})";
        currentRefreshCost += currentRefreshCost / 2;
        foreach (Transform oldButton in blessingSelectorGrid)
        {
            Destroy(oldButton.gameObject); 
        }
        List<Blessing> currentBlessingList = new();
        for (int i = 0; i < optionAmount; i++)
        {
            int attempts = 50;
            while (attempts > 0)
            {
                attempts--;
                int random = Random.Range(0, blessingsList.Count);
                Blessing choice = blessingsList[random];
                if (currentBlessingList.Contains(choice))
                {
                    continue;
                }
                currentBlessingList.Add(choice);
                BlessingButtonHover newButton = Instantiate(templateButton, blessingSelectorGrid);
                newButton.gameObject.SetActive(true);
                newButton.attachedBlessing = choice;
                newButton.GetComponent<Button>().onClick.AddListener(() => SelectBlessing(choice, newButton.gameObject));
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{choice.blessingID} (P {choice.cost})";
                break;
            }
        }
    }
}
