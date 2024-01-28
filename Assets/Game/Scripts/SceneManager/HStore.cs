using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HStore : StoreBase
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
        showTime = GameCenter.Instance.globalSettingSO.HStoreShowTime;
        GameCenter.Instance.uIManager.ShowTimeBar(2);
        GameCenter.Instance.audioManager.StopBGM();
        GameCenter.Instance.audioManager.PlaySFX(EnumSfxType.SFX_H);
    }

    protected override void LeaveFunc()
    {
        base.LeaveFunc();
        GameCenter.Instance.playerManager.Player.ChangeStatus(EnumPlayerStatus.H);
        GameCenter.Instance.audioManager.PlayBGM0(EnumSfxType.BGM0_Street);
        GameCenter.Instance.audioManager.PlayBGM1(EnumSfxType.BGM1_AfterH);
    }

}
