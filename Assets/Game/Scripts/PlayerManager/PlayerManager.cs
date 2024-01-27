using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PlayerManager : SerializedMonoBehaviour
{

    public Dictionary<EnumAnim, string> DicAnims;
    public Dictionary<EnumPlayerStatus, PlayerStatusInfo> DicPlayerStatus;

    public PlayerController Player;

    public void Init()
    {
        Player.Init();
    }

    public string GetAnimStr(EnumAnim animType)
    {
        return DicAnims[animType];
    }

    public PlayerStatusInfo GetPlayerStatusInfo(EnumPlayerStatus type)
    {
        return DicPlayerStatus[type];
    }


    public void SetCanMoveStatus(bool canMove)
    {
        Player.CanMove = canMove;
    }

}

[System.Serializable]
public class PlayerStatusInfo
{
    public float moveSpeed;
    public EnumAnim idleAnimType;
    public EnumAnim moveAnimType;

}
