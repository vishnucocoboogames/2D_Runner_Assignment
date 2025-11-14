using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviourSingleton<UiManager>
{
    [field: SerializeField] public GameUi GameUI { get; private set; }
    [SerializeField] GameStartUi gameStartUi;

    void Start()
    {
        gameStartUi.ShowScreen();
    }

}
