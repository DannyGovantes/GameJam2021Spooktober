using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;




public class SettingsMenu : Menu<SettingsMenu>
{

    [SerializeField]
    private Slider _masterVolumerSlider;

    [SerializeField]
    private Slider _sfxVolumeSlider;

    [SerializeField]
    private Slider _musicVolumerSlider;

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
            _dataManager.MasterVolume = volume;
        }
    }

    public void OnSFXVolumeChanged(float volume)
    {
        if (_dataManager != null)
        {
            _dataManager.SFXVolume = volume;
        }

    }

    public void OnMusicVolumeChanged(float volume)
    {
        if (_dataManager != null)
        {
            _dataManager.MusicVolume = volume;
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
