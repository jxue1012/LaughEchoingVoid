using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -------------- Delegates -------------------
public delegate void Action();

public delegate void Action<T>(T value);

public delegate void AnyFunc(params object[] values);

public delegate TResult Func<TResult>();

public delegate void PathFollowFunc(bool success);
public delegate void TimeChangeFunc(int value);


#endregion

#region ------------ Enum -----------------

public enum EnumPlayerStatus
{
    Nomral, Mask, Drug, Drunk, H, Tired, Crazy
}

public enum EnumCharacterType
{
    Player, Npc
}

public enum EnumCharSkin
{
    SkinTest
}

public enum EnumAnim
{
    ChatBox, MaskOn, MaskOff, NormalMove, NormalIdle, TiredMove, TiredIdle,
    Crazy, DrugMove, DrugIdle, DrunkMove, DrunkIdle,
    MaskMove, MaskIdle, HMove, HIdle,
    NPC_HangDie, NPC_FallDie, NPC_Jump, Npc_Idle, 
    NPC_NormalMove, NPC_TireMove, NPC_CrazyMove, NPC_DrugMove, NPC_DrunkMove,
    NPC_MaskMove, NPC_HMove,
    PlayerHangDie, PlayerFallDie

}

public enum EnumSfxType
{
    BGM0_MainScreen = 0, BGM0_Street,
    BGM1_Work, BGM1_Night, 
    SFX_Mask, BGM1_MaskOn,
    SFX_Drug, BGM1_AfterDrug,
    SFX_Drink, BGM1_AfterDrink,
    SFX_H, BGM1_AfterH,
    BGM1_Sleep,
    SFX_San, SFX_HP,
    BGM_Cliff

}

public enum EnumVfx
{
    Test
}

#endregion

#region ------------- Method -----------------
public static class Helpers
{

    public static int GetEnumLength(System.Type type)
    {
        string[] str = System.Enum.GetNames(type);
        return str.Length;
    }

    public static T GetEnumFromInt<T>(int value) where T : System.Enum
    {
        return (T)System.Enum.ToObject(typeof(T), value);
    }

    public static T GetRandomEnum<T>() where T : System.Enum
    {
        List<T> list = new List<T>();
        int length = GetEnumLength(typeof(T));
        int rand = UnityEngine.Random.Range(0, length);
        return GetEnumFromInt<T>(rand);
    }

}

#endregion

#region -------------- Class ---------------------

public class MonoObjectPool<T> where T : MonoBehaviour
{
    public Queue<T> qObjects = new Queue<T>();
    private GameObject prefab;

    public MonoObjectPool(int defaultNum, GameObject prefab = null)
    {
        this.prefab = prefab;
        for (int i = 0; i < defaultNum; i++)
        {
            CreateNewObject();
        }

    }

    public T GetFromPool()
    {
        if (qObjects.Count == 0)
            CreateNewObject();

        var obj = qObjects.Dequeue();
        return obj;
    }

    public void ReturnToPool(T target)
    {
        qObjects.Enqueue(target);
    }

    public void CreateNewObject()
    {
        if (prefab != null)
        {
            var obj = GameObject.Instantiate(prefab);
            var s = obj.AddComponent<T>();
            qObjects.Enqueue(s);
        }
        else
        {
            var obj = new GameObject(typeof(T).ToString());
            var o = obj.AddComponent<T>();
            qObjects.Enqueue(o);
        }
    }

}


#endregion


