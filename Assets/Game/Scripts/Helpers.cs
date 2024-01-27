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
    Nomral, Mask, Drug, Drunk, H, Tired
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
    MaskMove, MaskIdle, HMove, HIdle
}

public enum EnumSfxType
{
    BGM = 0,

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


