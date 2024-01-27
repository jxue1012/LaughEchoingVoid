using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugStore : StoreBase
{


    protected override void EnterTrigger(Collider2D other)
    {
        base.EnterTrigger(other);
    }

    protected override void ExitTrigger(Collider2D other)
    {
        base.ExitTrigger(other);

    }

    protected override void LeaveFunc()
    {
        base.LeaveFunc();
        GameCenter.Instance.playerManager.Player.ChangeStatus(EnumPlayerStatus.Drug);
    }


}
