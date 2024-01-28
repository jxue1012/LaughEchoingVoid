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
    public float PlayerAutoMoveSpeed = 5f;

    [Space(15)]
    [Header("------- 商店变量 -------")]
    public float DrugStoreShowTime = 3f;
    public Color DrugUIColor;
    public float DrinkStoreShowTime = 3f;
    public Color DrinkUIColor;
    public float HStoreShowTime = 3f;
    public Color HUIColor;
    public float WorkShowTime = 5f;
    public Color WorkUIColor;
    

    [Space(15)]
    [Header("------- 对话图片 -------")]
    public List<Sprite> ChatBoxList;

    [Space(15)]
    [Header("------- 加速设置 -------")]
    public float SpeedUpTimeValue = 8f;
}
