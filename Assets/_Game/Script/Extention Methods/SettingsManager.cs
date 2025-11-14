using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviourSingleton<SettingsManager>
{
    public static event Action<bool> OnSoundStateChange;
    public static event Action<bool> OnHapticStateChange;
    public static event Action<bool> OnMusicStateChange;

    [SerializeField] GameObject screen;
    [SerializeField] Transform popUp;
    [SerializeField] Toggle hapticToggle;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;

    private void Start()
    {
        if (musicToggle != null)
        {
            musicToggle.isOn = PlayerPrefs.GetInt(GameKeys.MUSIC_STATE_KEY, 1) == 1;

            musicToggle.onValueChanged.AddListener((isOn) =>
            {
                //AudioManager.Instance.SetMusic(isOn);
                OnMusicStateChange?.Invoke(isOn);
            });
        }

        // hapticToggle.isOn = PlayerPrefs.GetInt(GameKeys.HAPTIC_STATE_KEY, 1) == 1;
        // MMVibrationManager.SetHapticsActive(hapticToggle.isOn);
        // hapticToggle.onValueChanged.AddListener((isOn) =>
        // {
        //     OnHapticStateChange?.Invoke(isOn);
        //     PlayerPrefs.SetInt(GameKeys.HAPTIC_STATE_KEY, isOn ? 1 : 0);
        //     MMVibrationManager.SetHapticsActive(isOn);
        // });

        if (soundToggle != null)
        {
            soundToggle.isOn = PlayerPrefs.GetInt(GameKeys.SOUND_EFFECT_STATE_KEY, 1) == 1;

            soundToggle.onValueChanged.AddListener((isOn) =>
            {
                OnSoundStateChange?.Invoke(isOn);
            });
        }
    }

    public void Show()
    {
        screen.SetActive(true);
        popUp.localScale = Vector3.zero;
        popUp.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
        GameManager.Instance.OnGemePause(true);
        //LevelManager.Instance.HasOverleyInLevel = true;
    }
    public void Hide()
    {
        popUp.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            screen.SetActive(false);
            GameManager.Instance.OnGemePause(false);

            //LevelManager.Instance.HasOverleyInLevel = false;
        });

    }
}
