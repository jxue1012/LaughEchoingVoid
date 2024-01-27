using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;



[CreateAssetMenu(fileName = "GlobalSettingSO", menuName = "LaughEchoingVoid/GlobalSettingSO", order = 0)]
public class GlobalSettingSO : SerializedScriptableObject
{
    [Header("----- 数值条自动减少变量 ------")]
    
    public float defaultAttributeLoseTime = 1f;
    public int AttributeLoseValue = 1;

    [Space(15)]
    [Header(" ------ 音效数据 -------")]
    public Dictionary<EnumSfxType, AudioClip> DicAudio;

    [Space(15)]
    [Header("------- 移动变量 -------")]
    public float NpcMoveMinSpeed = 2.5f;
    public float NpcMoveMaxSpeed = 4f;
}
