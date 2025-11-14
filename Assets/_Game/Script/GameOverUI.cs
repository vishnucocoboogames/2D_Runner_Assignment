using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] Button reviveButton;

    void OnEnable()
    {
        GameManager.OnGameFail += OnGameFail;

    }

    void OnDisable()
    {
        GameManager.OnGameFail -= OnGameFail;

    }


    private void OnGameFail()
    {
        screen.SetActive(true);

        if (EconomyManager.Instance.CoinCount < 100)
            reviveButton.interactable = false;
    }

    public void OnReviveButtonPress()
    {
        GameManager.Instance.Revieve();
        screen.SetActive(false);
    }

    public void OnRestartButtonPress()
    {
        screen.SetActive(false);
        GameManager.Instance.Restart();
    }

}
