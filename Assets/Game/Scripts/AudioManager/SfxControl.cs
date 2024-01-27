using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxControl : MonoBehaviour
{
    private AudioSource source;
    private bool IsPlayed;
    
    private void Awake() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = GameCenter.Instance.audioManager.SfxMixerGroup;
        source.playOnAwake = false;
        IsPlayed = false;
    }

    public void Play(AudioClip clip, float volume, bool randomPitch, bool loop)
    {
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;

        if(randomPitch)
            source.pitch = Random.Range(0.8f, 1.2f);
            
        source.Play();
        IsPlayed = true;
    }

    private void Update()
    {
        if (IsPlayed && !source.isPlaying)
        {
            Stop();
        }

    }

    public void Stop()
    {
        source.Stop();
        source.clip = null;
        GameCenter.Instance.audioManager.ReturnSFXToPool(this);
        IsPlayed = false;
    }

}
