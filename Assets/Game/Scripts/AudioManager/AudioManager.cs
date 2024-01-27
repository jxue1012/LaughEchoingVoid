using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;


public class AudioManager : SerializedMonoBehaviour
{

    private MonoObjectPool<SfxControl> sfxPool;
    public AudioMixerGroup SfxMixerGroup;

    public AudioSource BGM0;
    public AudioSource BGM1;

    public void Init()
    {
        sfxPool = new MonoObjectPool<SfxControl>(3);

    }

    private AudioClip GetAudioClip(EnumSfxType type)
    {
        AudioClip clip = GameCenter.Instance.globalSettingSO.DicAudio[type];
        return clip;
    }

    [Button]
    public void PlaySFX(EnumSfxType type, float volume = 1, bool randomPitch = true, bool loop = false)
    {
        var control = sfxPool.GetFromPool();
        var clip = GetAudioClip(type);
        control.Play(clip, volume, randomPitch, loop);
    }

    [Button]
    public void PlayBGM0(EnumSfxType type)
    {
        var clip = GetAudioClip(type);
        BGM0.clip = clip;
        BGM0.Play();
    }
    
    [Button]
    public void PlayBGM1(EnumSfxType type)
    {
        var clip = GetAudioClip(type);
        BGM1.clip = clip;
        BGM1.Play();
    }

    public void ReturnSFXToPool(SfxControl c)
    {
        sfxPool.ReturnToPool(c);
    }

}

