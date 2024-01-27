using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightManager : MonoBehaviour
{

    public Transform LightRoot;
    public Light2D GlobalLight;
    public List<Light2D> SceneLights;

    public void Init()
    {

    }

    [Button]
    public void InitLightList()
    {
        var list = LightRoot.GetComponentsInChildren<Light2D>().ToList();
        SceneLights = new();
        GlobalLight = list[0];
        list.RemoveAt(0);
        SceneLights = list;
    }

    [Button]
    public void GenerateLightData(LightDataSO so)
    {
        so.SceneLightList = new();
        var list = LightRoot.GetComponentsInChildren<Light2D>().ToList();
        var globalLight = list[0];
        so.GlobalLightInfo = new LightInfo(globalLight);
        list.RemoveAt(0);

        foreach (var x in list)
        {
            var info = new LightInfo(x);
            so.SceneLightList.Add(info);
        }
    }

    [Button]
    public void SetLightsByData(LightDataSO so)
    {
        GlobalLight.intensity = so.GlobalLightInfo.intensity;
        GlobalLight.color = so.GlobalLightInfo.color;

        for (int i = 0; i < SceneLights.Count; i++)
        {
            var info = so.SceneLightList[i];
            var light = SceneLights[i];
            light.intensity = info.intensity;
            light.color = info.color;
        }

    }



}

