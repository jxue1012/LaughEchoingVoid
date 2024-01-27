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

    public virtual void Init()
    {
        this.SA = this.GetComponent<SkeletonAnimation>();
    }

    public void SetSkin(Skin skin)
    {
        Skeleton skeleton = SA.Skeleton;
        skeleton.SetSkin(skin);
        skeleton.SetSlotsToSetupPose();
    }

}

