using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] GameObject visual;


    public void OnCollecetion()
    {
        OnCoinCollcet();
    }

    void OnCoinCollcet()
    {
            AudioManager.Instance.PlaySoundOfType(SoundEffectType.CoinCollect);
            GameManager.Instance.OnCoinCollect();
            Destroy(gameObject);
    }


}
