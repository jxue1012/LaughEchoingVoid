using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Home : MonoBehaviour
{

    private Collider2D col;
    public Collider2D blocker;

    public Transform EnterPoint;
    public Transform LeavePoint;

    public void Init()
    {
        col = this.gameObject.GetComponent<Collider2D>();
    }

    public void Show()
    {
        col.enabled = true;
        blocker.enabled = false;
    }

    public void Hide()
    {
        col.enabled = false;
        blocker.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = GameCenter.Instance.playerManager.Player;
        player.CanMove = false;
        player.CanMask = false;
        player.CanAttributeChange = false;
        player.PlayMoveAnim();
        player.transform.DOMove(LeavePoint.position, 1f);
        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition();
    }


}
