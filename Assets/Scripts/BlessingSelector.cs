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
        }
        instance = this;
    }
    #endregion
    public List<Blessing> blessingsList = new();
    // players get a list of blessings purchasable, can refresh for power. random insertion of each tier. 
    public GameObject blessingSelectorUI;
    public Transform blessingSelectorGrid;
    public PlayerController player;
    public float refreshCost;
    public Button templateButton;
    public int optionAmount;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void SetBlessingSelectorUI()
    {
        blessingSelectorUI.gameObject.SetActive(!blessingSelectorUI.activeSelf);
    }

    private void SelectBlessing(Blessing selectedBlessing)
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
        }
    }

    public void RefreshList(bool firstTime = false)
    {
        if (!firstTime && !ResourceManager.instance.RemovePower(refreshCost))
        {
            return;
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
                Button newButton = Instantiate(templateButton, blessingSelectorGrid);
                newButton.gameObject.SetActive(true);
                newButton.onClick.AddListener(() => SelectBlessing(choice));
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.blessingID;
                break;
            }
        }
    }
}
