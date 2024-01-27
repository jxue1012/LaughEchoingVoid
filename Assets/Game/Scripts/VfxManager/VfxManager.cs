using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class VfxManager : MonoBehaviour
{
    [Header("------ Data -----")]
    public List<VfxPoolInfo> PoolSettings = new();

    private Dictionary<EnumVfx, MonoObjectPool<VfxInstanceControl>> dicPools;

    public void Init()
    {
        dicPools = new Dictionary<EnumVfx, MonoObjectPool<VfxInstanceControl>>();
        foreach (var info in PoolSettings)
        {
            var pool = new MonoObjectPool<VfxInstanceControl>(info.Size, info.Prefab);
            dicPools.Add(info.Type, pool);
            foreach (var instance in pool.qObjects)
            {
                instance.SetInstance(info.Type);
            }
        }
    }

    public void PlayVFX(EnumVfx type, Vector3 pos)
    {
        var pool = dicPools[type];
        var vfx = pool.GetFromPool();
        vfx.Play(pos);
    }

    public void ReturnVFXToPool(VfxInstanceControl vfx)
    {
        var type = vfx.Type;
        var pool = dicPools[type];
        pool.ReturnToPool(vfx);
    }
    

}

[System.Serializable]
public class VfxPoolInfo
{
    public EnumVfx Type;
    public int Size;
    public GameObject Prefab;
}
