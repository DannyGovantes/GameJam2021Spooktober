using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField]
    private List<AudioSource> _backgroundMusic = new List<AudioSource>();
    public List<AudioSource> BackgroundMusic => _backgroundMusic;


    public void PlayMusicClip(int index)
    {
        _backgroundMusic[index].Play();
    }
    public void StopMusicClip(int index)
    {
        _backgroundMusic[index].Stop();
    }

}
