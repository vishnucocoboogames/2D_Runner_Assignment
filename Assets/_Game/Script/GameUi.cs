using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [field: SerializeField] public Wallet Wallet { get; private set; }
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
        Wallet.SetCoinCount();
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
