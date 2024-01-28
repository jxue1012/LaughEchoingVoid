using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


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
        foreach (var npc in NpcList)
            npc.Init();
    }

    public void ChangeNpcStatus(EnumPlayerStatus newStatus)
    {
        foreach (var npc in NpcList)
            npc.ChangePlayerStatus(newStatus);
    }

    public void SetNpcToWalkOnStreet()
    {
        foreach (var npc in NpcList)
        {
            npc.StartMove(true, true);
            npc.StartChat();
        }
    }

    public void SetNpcToEndScene()
    {
        HideAllNpc();
        var posList = GameCenter.Instance.sceneManager.NpcPosList;
        for (int i = 0; i < 15; i++)
        {
            var pos = posList[i].position;
            var npc = NpcList[i];
            npc.transform.position = pos;
            npc.ChangePlayerStatus(EnumPlayerStatus.Mask);
            npc.SetFaceDir(true);
            npc.PlayBaseAnim(EnumAnim.Npc_Idle, true);
            npc.Show();
        }
    }

    private void HideAllNpc()
    {
        foreach (var npc in NpcList)
        {
            npc.Hide();
            npc.EndChat();
        }
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
