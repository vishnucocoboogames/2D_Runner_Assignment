using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviourSingleton<UiManager>
{
    [SerializeField] GameUi gameUi;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] GameStartUi gameStartUi;

    void Start()
    {
        gameStartUi.ShowScreen();
    }

}
