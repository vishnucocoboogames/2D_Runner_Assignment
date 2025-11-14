using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : GameObstacles, ICollectable
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] GameObject visual;


    public void OnCollecetion()
    {
        OnPowerUpUse();
    }
    private void OnPowerUpUse()
    {
        switch (powerUpType)
        {
            case PowerUpType.Sheild:
                GameManager.Instance.OnPowerUpUsed(powerUpType);
                AudioManager.Instance.PlaySoundOfType(SoundEffectType.PowerUp);
                Destroy(gameObject);
                break;
            case PowerUpType.Boost:
                GameManager.Instance.OnPowerUpUsed(powerUpType);
                AudioManager.Instance.PlaySoundOfType(SoundEffectType.PowerUp);
                Destroy(gameObject);
                break;
            default:
                break;
        }


    }

}
public enum PowerUpType
{
    Sheild,
    Boost,
}
