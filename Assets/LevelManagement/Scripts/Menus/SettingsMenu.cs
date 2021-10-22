using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : Menu<SettingsMenu>
{

    [SerializeField]
    private Slider _masterVolumerSlider;

    [SerializeField]
    private Slider _sfxVolumeSlider;

    [SerializeField]
    private Slider _musicVolumerSlider;

    [SerializeField]
    private AudioMixer _audioMixer;

    private DataManager _dataManager;

    protected override void Awake()
    {
        base.Awake();
        _dataManager = Object.FindObjectOfType<DataManager>();

    }

    private void Start()
    {
        LoadData();
    }

    public void OnMasterVolumeChanged(float volume)
    {
        if (_dataManager != null)
        {
            _audioMixer.SetFloat("Master", Mathf.Log10(volume)* 20f);
            _dataManager.MasterVolume = Mathf.Log10(volume)* 20f;
        }
    }

    public void OnSFXVolumeChanged(float volume)
    {
        if (_dataManager != null)
        {
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume)* 20f);
            _dataManager.SFXVolume = Mathf.Log10(volume)* 20f;
        }

    }

    public void OnMusicVolumeChanged(float volume)
    {
        if (_dataManager != null)
        {
            _audioMixer.SetFloat("Music", Mathf.Log10(volume)* 20f);
            _dataManager.MusicVolume = Mathf.Log10(volume)* 20f;
        }
    }

    public override void OnBackPressed()
    {
        //Some shit that can be useful
        base.OnBackPressed();

        if (_dataManager != null)
        {
            _dataManager.Save();
        }

        //Shit that prolly useful too
    }

    public void LoadData()
    {

        if (_dataManager == null || _masterVolumerSlider == null || _sfxVolumeSlider == null || _musicVolumerSlider == null)
        {
            return;
        }
        _dataManager.Load();

        _masterVolumerSlider.value = _dataManager.MasterVolume;
        _sfxVolumeSlider.value = _dataManager.SFXVolume;
        _musicVolumerSlider.value = _dataManager.MusicVolume;

    }
}
