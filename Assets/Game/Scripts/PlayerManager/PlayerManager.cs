using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PlayerManager : SerializedMonoBehaviour
{

    public Dictionary<EnumAnim, string> DicAnims;

    public PlayerController Player;

    public void Init()
    {
        Player.Init();
    }

    public string GetAnimStr(EnumAnim animType)
    {
        return DicAnims[animType];
    }

}
