using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkStore : StoreBase
{
    protected override void EnterTrigger(Collider2D other)
    {
        base.EnterTrigger(other);
    }

    protected override void ExitTrigger(Collider2D other)
    {
        base.ExitTrigger(other);

    }

    protected override void StartStoreAction()
    {
        base.StartStoreAction();
        showTime = GameCenter.Instance.globalSettingSO.DrinkStoreShowTime;

    }


    protected override void LeaveFunc()
    {
        base.LeaveFunc();
        GameCenter.Instance.playerManager.Player.ChangeStatus(EnumPlayerStatus.Drunk);
    }

}
