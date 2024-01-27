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
        InitNPC();
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

    #region ------------- NPC -----------------
    public List<NpcController> NpcList = new();

    public void InitNPC()
    {
        foreach(var npc in NpcList)
            npc.Init();
    }

    #endregion


}

[System.Serializable]
public class PlayerStatusInfo
{
    public float moveSpeed;
    public EnumAnim idleAnimType;
    public EnumAnim moveAnimType;

}
