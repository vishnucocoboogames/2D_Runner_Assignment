using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterToggle : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject lockPanel;

    [Header("Character Settings")]
    [SerializeField] private int id;
    [SerializeField] private int unlockCost ;
    [SerializeField] private bool isUnlocked;

    private void Start()
    {
        LoadUnlockState();
        SetToggleState();

        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn && isUnlocked)
            {
                GameManager.Instance.ChangeCharecter(id);
                PlayerPrefs.SetInt(GameKeys.CHARECTERINDEX, id);
            }
        });
    }

    private void LoadUnlockState()
    {
        // 0 = locked, 1 = unlocked
        isUnlocked = PlayerPrefs.GetInt($"CHAR_UNLOCK_{id}", id == 0 ? 1 : 0) == 1;

        lockPanel.SetActive(!isUnlocked);
        toggle.interactable = isUnlocked;

        if (costText != null)
            costText.text = unlockCost.ToString();
    }

    private void SetToggleState()
    {
        int selectedId = PlayerPrefs.GetInt(GameKeys.CHARECTERINDEX, 0);
        toggle.isOn = id == selectedId;
        
    }

    public  void UnlockCharacter()
    {


        if (EconomyManager.Instance.CoinCount >= unlockCost)
        {
            EconomyManager.Instance.RemoveCoin(unlockCost);
            isUnlocked = true;
            PlayerPrefs.SetInt($"CHAR_UNLOCK_{id}", 1);

            lockPanel.SetActive(false);
            toggle.interactable = true;

            Debug.Log($"Character {id} unlocked!");
        }
        else
        {
            Debug.Log("Not enough coins to unlock this character!");
        }
    }
}
