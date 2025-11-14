using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,ICollectable
{
    [SerializeField] GameObject visual;

    public void OnCollecetion()
    {
        OnCoinCollcet();
    }

    void OnCoinCollcet()
    {
        GameManager.Instance.OnCoinCollect();
        AudioManager.Instance.PlaySoundOfType(SoundEffectType.CoinCollect);
        Destroy(gameObject);
    }
}
