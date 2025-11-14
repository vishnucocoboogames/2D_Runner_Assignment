using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    [SerializeField] GameObject PauseScreen;

    void OnEnable()
    {
        EconomyManager.OnCoinValueChange += OnCoinValueChange;
    }

    void OnDisable()
    {
        EconomyManager.OnCoinValueChange -= OnCoinValueChange;
    }

    private void OnCoinValueChange()
    {
        wallet.SetCoinCount();
    }
    public void OnPauseButtonPress()
    {
        GameManager.Instance.OnGemePause(true);
        PauseScreen.SetActive(true);
    }
    public void OnResumeButtonPress()
    {
        GameManager.Instance.OnGemePause(false);
        PauseScreen.SetActive(false);
    }

}
