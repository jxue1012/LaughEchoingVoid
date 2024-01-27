using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CamManager : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public float shakeIntensity = 1f;

    public void Init()
    {

    }

    [Button]
    public void TriggerShake()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randomDirection.Normalize(); // 标准化以保持方向但不改变大小

        // 触发抖动
        impulseSource.GenerateImpulseAt(transform.position, randomDirection * shakeIntensity);
    }
}
