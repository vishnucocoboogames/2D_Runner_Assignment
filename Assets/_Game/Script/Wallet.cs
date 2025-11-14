using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] TMP_Text CoinTxt;

    public void SetCoinCount()
    {
        CoinTxt.text = EconomyManager.Instance.CoinCount.ToString();
    }
}
