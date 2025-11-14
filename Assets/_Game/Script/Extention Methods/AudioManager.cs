
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviourSingletonPersistent<AudioManager>
{
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource soundAudioSource;
    [SerializeField] List<AudioClip> mMusicsList = new List<AudioClip>();
    [SerializeField] List<SoundEffect> mSoundEffectsList = new List<SoundEffect>(6);
    int mCurrentMusicIndex;
    public bool IsSoundEffectsOn { get; private set; }
    float musicVolume = .5f;
    bool musicState;
    void OnEnable()
    {

        musicAudioSource.volume = .5f;
        soundAudioSource.volume = 1f;

        musicState = PlayerPrefs.GetInt(GameKeys.MUSIC_STATE_KEY, 1) == 1;
        IsSoundEffectsOn = PlayerPrefs.GetInt(GameKeys.SOUND_EFFECT_STATE_KEY, 1) == 1;

        SetMusicState(musicState);

        SettingsManager.OnMusicStateChange += SetMusicState;
        SettingsManager.OnSoundStateChange += SetSoundState;
    }

    void OnDisable()
    {
        SettingsManager.OnMusicStateChange -= SetMusicState;
        SettingsManager.OnSoundStateChange -= SetSoundState;
    }

    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void SetSoundState(bool on)
    {
        IsSoundEffectsOn = on;
        PlayerPrefs.SetInt(GameKeys.SOUND_EFFECT_STATE_KEY, on ? 1 : 0);
    }

    public void SetMusicState(bool on)
    {
        if (mMusicsList.Count == 0) return;
        PlayerPrefs.SetInt(GameKeys.MUSIC_STATE_KEY, on ? 1 : 0);
        if (on)
            PlayCurrentMusic();
        else
            musicAudioSource.Stop();
    }

    public virtual void SetMusicVolume(float volume)
    {
        if (mMusicsList.Count == 0) return;
        PlayerPrefs.SetFloat(GameKeys.MUSIC_VOLUME_KEY, volume);
        musicAudioSource.volume = volume;
        musicVolume = volume;
    }

    public virtual void ChangeMusic(int musicIndex)
    {
        mCurrentMusicIndex = musicIndex;
        PlayCurrentMusic();
    }
    public virtual void PlaySoundOfType(SoundEffectType soundType, float pitch = 1f, float volume = 1f)
    {
        if (!IsSoundEffectsOn) return;

        AudioClip clip = GetSoundOfType(soundType);
        if (clip == null) return;

        GameObject soundObj = new GameObject("TempAudio_" + soundType);
        AudioSource tempSource = soundObj.AddComponent<AudioSource>();

        tempSource.clip = clip;
        tempSource.volume = volume;// PlayerPrefs.GetFloat(GameKeys.SOUND_VOLUME_KEY, 1);
        tempSource.pitch = pitch;
        tempSource.Play();

        UnityEngine.Object.Destroy(soundObj, clip.length / Mathf.Abs(pitch));
    }
    /*public virtual void PlaySoundOfType(SoundEffectType soundType)
    {
        if (!IsSoundEffectsOn) return;
        soundAudioSource.volume = PlayerPrefs.GetFloat(GameKeys.SOUND_VOLUME_KEY, 1);
        soundAudioSource.PlayOneShot(GetSoundOfType(soundType));
    }*/

    public void StopSound()
    {
        soundAudioSource.Stop();
    }
    public void PlaySound(AudioClip clip)
    {
        if (!IsSoundEffectsOn) return;
        soundAudioSource.volume = PlayerPrefs.GetFloat(GameKeys.SOUND_VOLUME_KEY, 1);
        soundAudioSource.PlayOneShot(clip);
    }
    AudioClip GetSoundOfType(SoundEffectType soundType)
    {
        foreach (var _soundEffect in mSoundEffectsList)
        {
            if (_soundEffect.type == soundType) return _soundEffect.clip;
        }
        Debug.LogError("Can't Find Sound Effect With Type " + soundType.ToString());
        return null;
    }

    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();


        }
    }
    void PlayCurrentMusic()
    {
        musicAudioSource.clip = mMusicsList[mCurrentMusicIndex];
        musicAudioSource.Play();
        // musicAudioSource.DOFade(musicVolume, .25f);
    }

    public void PauseMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
        }
    }
    public void ResumeMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.UnPause();
        }
    }
    private void OnValidate()
    {
        foreach (var Sfx in mSoundEffectsList)
        {
            Sfx.sfxName = Sfx.type.ToString();
        }
    }
}


[System.Serializable]
public class SoundEffect
{
    [HideInInspector]
    public string sfxName;
    public SoundEffectType type;
    public AudioClip clip;
}

public enum SoundEffectType { None = -1, Win, Loose, ButtonClick, CoinCollect, LevelComplete, TrayCollect, BallDrop, BallRoll, BallCollect, BufferExit, AddMoves, OnHammerUse, OnShuffleUse ,Jump,PowerUp}