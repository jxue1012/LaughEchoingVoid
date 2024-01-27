using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;


public class AudioManager : SerializedMonoBehaviour
{

    private MonoObjectPool<SfxControl> sfxPool;
    public AudioMixerGroup SfxMixerGroup;

    //Data
    public Dictionary<EnumSfxType, AudioClip> DicSfx;

    public void Init()
    {
        sfxPool = new MonoObjectPool<SfxControl>(3);

    }

    private AudioClip GetSfxClip(EnumSfxType type)
    {
        AudioClip clip = DicSfx[type];
        return clip;
    }

    public void PlaySFX(EnumSfxType type, float volume = 1, bool randomPitch = true, bool loop = false)
    {
        var control = sfxPool.GetFromPool();
        var clip = GetSfxClip(type);
        control.Play(clip, volume, randomPitch, loop);
    }

    public void ReturnSFXToPool(SfxControl c)
    {
        sfxPool.ReturnToPool(c);
    }

}

