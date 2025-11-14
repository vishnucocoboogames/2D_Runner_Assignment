using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviourSingleton<EconomyManager>
{
    public static event Action OnCoinValueChange;
    [field: SerializeField] public int CoinCount { get; private set; }


    void Start()
    {
        SetCoinCount();
    }
    void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnCoinCollcet += OnCoinCollcet;
    }


    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnCoinCollcet -= OnCoinCollcet;


    }
    private void OnCoinCollcet()
    {
        CoinCount++;
        PlayerPrefs.SetInt(GameKeys.CURRENCY_COIN_KEY, CoinCount);
        OnCoinValueChange?.Invoke();
    }

    private void OnGameStart()
    {
        SetCoinCount();
    }


    private void SetCoinCount()
    {
        CoinCount = PlayerPrefs.GetInt(GameKeys.CURRENCY_COIN_KEY);
        OnCoinValueChange?.Invoke();
    }

    public void RemoveCoin(int coin)
    {
        CoinCount -= coin;
        PlayerPrefs.SetInt(GameKeys.CURRENCY_COIN_KEY, CoinCount);
        OnCoinValueChange?.Invoke();
    }


}
