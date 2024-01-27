using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxInstanceControl : MonoBehaviour
{

    private ParticleSystem system;
    public EnumVfx Type;

    private void Awake()
    {
        system = this.GetComponent<ParticleSystem>();

        //system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void SetInstance(EnumVfx type)
    {
        Type = type;
    }

    public void Play(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
        system.Play();
    }

    private void OnParticleSystemStopped()
    {
        // 这里添加你的自定义逻辑
        GameCenter.Instance.vfxManager.ReturnVFXToPool(this);

    }

}
