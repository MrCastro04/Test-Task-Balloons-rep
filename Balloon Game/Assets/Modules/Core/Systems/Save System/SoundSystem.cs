using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.Utility.Singleton;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    [Header("Music Settings")]
    [SerializeField] private bool _playMusicOnStart = true;
    [SerializeField] private AudioClip _defaultMusic;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SaveSettingsGA>(ApplySettingsPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SaveSettingsGA>();
    }

    private void Start()
    {
        ApplySettings();

        if (_playMusicOnStart && _musicSource != null)
            PlayMusic(_defaultMusic);
    }

    private IEnumerator ApplySettingsPerformer(SaveSettingsGA ga)
    {
        ApplySettings();
        yield return null;
    }

    public void ApplySettings()
    {
        float musicVolume = SaveSystem.Instance.LoadMusicVolume();
        float soundVolume = SaveSystem.Instance.LoadSoundVolume();

        if (_musicSource != null)
        {
            _musicSource.volume = musicVolume;
        }

        if (_soundSource != null)
        {
            _soundSource.volume = soundVolume;
        }

        Debug.Log($"SoundSystem применил настройки: Sound={soundVolume}, Music={musicVolume}");
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource == null || clip == null)
            return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (_soundSource == null || clip == null)
            return;

        _soundSource.PlayOneShot(clip);
    }
}