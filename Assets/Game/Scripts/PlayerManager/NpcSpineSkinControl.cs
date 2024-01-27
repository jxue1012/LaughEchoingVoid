using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Sirenix.OdinInspector;

public class NpcSpineSkinControl : MonoBehaviour
{
    private SkeletonAnimation SA;
    private Skin mySkin;
    [SpineSkin] public List<string> headSets;
    [SpineSkin] public List<string> clothSets;

    public void Init()
    {
        SA = this.GetComponent<SkeletonAnimation>();
        GenerateSkin();
    }

    [Button]
    public void GenerateSkin()
    {
        mySkin = new Skin("mySkin");

        Skeleton skeleton = SA.Skeleton;
        SkeletonData skeletonData = skeleton.Data;

        //Add Hair
        int headIndex = Random.Range(0, headSets.Count);
        string headSkin = headSets[headIndex];
        mySkin.AddSkin(skeletonData.FindSkin(headSkin));

        //Add Cloth
        int clothIndex = Random.Range(0, clothSets.Count);
        string clothSkin = clothSets[clothIndex];
        mySkin.AddSkin(skeletonData.FindSkin(clothSkin));

        SetSkin(mySkin);

    }


    public void SetSkin(Skin skin)
    {
        Skeleton skeleton = SA.Skeleton;
        skeleton.SetSkin(skin);
        skeleton.SetSlotsToSetupPose();
    }

    [Button]
    public void FindSkinData()
    {
        SA = this.GetComponent<SkeletonAnimation>();

        headSets = new();
        clothSets = new();

        var skins = SA.Skeleton.Data.Skins;
        foreach (var skin in skins)
        {
            if (skin.Name.StartsWith("up"))
            {
                headSets.Add(skin.Name);
            }
            else if (skin.Name.StartsWith("clo"))
            {
                clothSets.Add(skin.Name);
            }

        }


    }

}
