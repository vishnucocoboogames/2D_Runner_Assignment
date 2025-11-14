using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Toggle))]
public class SoundToggle : MonoBehaviour
{
    [SerializeField] bool hasEffects = true;
    Toggle toggle;
    [SerializeField] SoundEffectType soundEffectType = SoundEffectType.ButtonClick;
    [SerializeField] GameObject OnVisual;
    [SerializeField] GameObject OffVisual;
  
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                PlaySound();
            }
            PlayToggleAnimation();

            ChangeVisual(value);
        });
        ChangeVisual(toggle.isOn);

    }

    private void ChangeVisual(bool value)
    {
        OnVisual.SetActive(value);
        OffVisual.SetActive(!value);
    }

    private void PlaySound()
    {
        AudioManager.Instance?.PlaySoundOfType(soundEffectType);
       // MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    void PlayToggleAnimation()
    {
        float scale = 1;
        if (hasEffects)
            transform.DOScale(Vector3.one * scale * .75f, .1f).SetUpdate(true).SetEase(Ease.InOutQuint).OnComplete(() =>
            {
                transform.DOScale(Vector3.one * scale, .1f).SetUpdate(true);
            });
    }
}
