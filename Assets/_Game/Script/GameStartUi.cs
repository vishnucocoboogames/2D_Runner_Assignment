using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUi : MonoBehaviour
{
    [SerializeField] GameObject screen;


    public void ShowScreen()
    {
        screen.SetActive(true);
    }

    public void OnGameStartButtonPress()
    {
        screen.SetActive(false);
        GameManager.Instance.StartGame();
    }


}
