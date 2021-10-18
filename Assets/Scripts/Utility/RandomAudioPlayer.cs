using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

    
[Serializable]
public class SoundBank
{
    public string name;
    public AudioClip[] clips;
}
[Serializable]
public class MaterialAudioOverride
{
    public Material[] materials;
    public SoundBank[] banks;

}
[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    public bool randomizePitch = true;
    public float pitchRandomRange = 0.2f;
    public float playDelay = 0;
    public SoundBank defaultBank = new SoundBank();
    public MaterialAudioOverride[] overrides;

    public bool playing;
    public bool canPlay;

    protected AudioSource _audioSource;
    protected Dictionary<Material,SoundBank[]> _archive = new Dictionary<Material, SoundBank[]>();
    public AudioSource audioSource => _audioSource;
    public AudioClip clip { get; private set; }

    private void Awake()
    {
        if (TryGetComponent(out _audioSource))
        {
            print($"NO AUDIO SOURCE");
        }

        for (int i = 0; i < overrides.Length; i++)
        {
            foreach (var material in overrides[i].materials)
            {
                _archive[material] = overrides[i].banks;
            }
        }
    }

    public AudioClip PlayRandomClip(Material overrideMaterial, int bankId = 0)
    {
        if (overrideMaterial == null) return null;
        return InternalPlayRandomClip(null, bankId: 0);
    }

    private AudioClip InternalPlayRandomClip(Material overrideMaterial, int bankId)
    {
        SoundBank[] banks = null;
        var bank = defaultBank;
        if (overrideMaterial)
        {
            if (_archive.TryGetValue(overrideMaterial, out banks))
            {
                if (bankId < banks.Length)
                {
                    bank = banks[bankId];
                }
            }
        }

        if (bank.clips == null || bank.clips.Length == 0) return null;
        var clip = bank.clips[Random.Range(0, bank.clips.Length)];
        if (!clip) return null;
        _audioSource.pitch = randomizePitch ? Random.Range(1.0f - pitchRandomRange, 1.0f + pitchRandomRange) : 1.0f;
        _audioSource.clip = clip;
        _audioSource.PlayDelayed(playDelay);
        return clip;
    }

}


