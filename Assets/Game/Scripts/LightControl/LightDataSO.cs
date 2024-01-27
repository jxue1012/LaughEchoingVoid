using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "LightDataSO", menuName = "LaughEchoingVoid/LightDataSO", order = 0)]
public class LightDataSO : ScriptableObject
{
    public LightInfo GlobalLightInfo;

    public List<LightInfo> SceneLightList = new();

}

[System.Serializable]
public class LightInfo
{
    public float intensity;
    public Color color;

    public LightInfo(Light2D obj)
    {
        intensity = obj.intensity;
        color = obj.color;
    }
}