using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Sirenix.OdinInspector;

public class SpineSkinControl : MonoBehaviour
{

    protected SkeletonAnimation SA;
    // This "naked body" skin will likely change only once upon character creation,
    // so we store this combined set of non-equipment Skins for later re-use.
    protected Skin bodySkin;
    protected Skin dressSkin;


    [SpineSkin] public List<string> nakedSkinSets;

    [SpineSkin] public List<string> dressedSkinSets;

    private EnumCharSkin activeSkin;


    [Button]
    public virtual void Init()
    {

        this.SA = this.GetComponent<SkeletonAnimation>();

        GenerateBodySkin();
        GenerateClothSkin();

        SetSkin(dressSkin);
    }

    public virtual void GenerateBodySkin()
    {
        Skeleton skeleton = SA.Skeleton;
        SkeletonData skeletonData = skeleton.Data;
        bodySkin = new Skin("bodySkin");

        foreach (var s in nakedSkinSets)
        {
            bodySkin.AddSkin(skeletonData.FindSkin(s));
        }


    }

    public virtual void GenerateClothSkin()
    {
        Skeleton skeleton = SA.Skeleton;
        SkeletonData skeletonData = skeleton.Data;
        dressSkin = new Skin("clothSkin");

        foreach (var s in dressedSkinSets)
        {
            dressSkin.AddSkin(skeletonData.FindSkin(s));
        }

    }

    public void ChangeSkin(EnumCharSkin skinType)
    {
        if (skinType == activeSkin)
            return;

        // switch (skinType)
        // {
        //     case EnumCharSkin.Cloth:
        //         SetSkin(dressSkin);
        //         activeSkin = EnumCharSkin.Cloth;
        //         break;

        //     case EnumCharSkin.Naked:
        //         SetSkin(bodySkin);
        //         activeSkin = EnumCharSkin.Naked;
        //         break;

        //     default:
        //         break;
        // }


    }

    public void SetSkin(Skin skin)
    {
        Skeleton skeleton = SA.Skeleton;
        skeleton.SetSkin(skin);
        skeleton.SetSlotsToSetupPose();
    }

}

