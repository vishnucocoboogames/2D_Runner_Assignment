using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static event Action OnGameStart;
    public static event Action<bool> OnPauseGame;
    public static event Action OnGameFail;
    public static event Action OnCoinCollcet;
    public static event Action<PowerUpType> OnPowerUpUse;
    public static event Action OnRevive;
    public static event Action OnCharecterChange;

    [field: SerializeField] public bool PauseGame { get; private set; }
    [field: SerializeField] public bool IsGameStrated { get; private set; }
    [field: SerializeField] public bool IsGameFailed { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public int CharecterIndex { get; private set; }
    [field: SerializeField] public Player player { get; private set; }

    public void StartGame()
    {
        SetCharecterIndex();
        OnGameStart?.Invoke();
        IsGameStrated = true;
    }

    public void OnCoinCollect()
    {
        OnCoinCollcet?.Invoke();
    }

    public void OnGameOver()
    {
        IsGameFailed = true;
        OnGameFail?.Invoke();

    }

    public void OnPowerUpUsed(PowerUpType type)
    {
        OnPowerUpUse?.Invoke(type);
    }
    public void OnGemePause(bool state)
    {
        OnPauseGame?.Invoke(state);
        PauseGame = state;
    }

    public void Revieve()
    {
        if (EconomyManager.Instance.CoinCount >= 100)
        {
            OnRevive?.Invoke();
            IsGameFailed = false;
            EconomyManager.Instance.RemoveCoin(100);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetCharecterIndex()
    {
        CharecterIndex = PlayerPrefs.GetInt(GameKeys.CHARECTERINDEX);
    }

    public void ChangeCharecter(int intx)
    {
        CharecterIndex = intx;
        PlayerPrefs.SetInt(GameKeys.CHARECTERINDEX, CharecterIndex);
        OnCharecterChange?.Invoke();
    }

    public void SetMoveSpeed(float speed) => MoveSpeed = speed;

}
