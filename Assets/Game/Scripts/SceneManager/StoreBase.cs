using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class StoreBase : MonoBehaviour
{

    public GameObject Sign;
    public Transform EnterPoint, LeavePoint;
    private Collider2D col;
    private bool canEnter;
    protected bool TimePassOn;
    protected float showTime;
    protected float timer;

    public void Init()
    {
        Sign.SetActive(false);
        col = this.GetComponent<Collider2D>();
        canEnter = true;
    }

    private void Update()
    {
        if (TimePassOn)
            UpdateFunc();
    }

    protected virtual void UpdateFunc()
    {
        timer += Time.deltaTime;
        GameCenter.Instance.uIManager.UpdateTimeBar(timer / showTime);
        if (timer >= showTime)
        {
            timer = 0f;
            TimePassOn = false;
            GameCenter.Instance.uIManager.HideTimeBar();
            GameCenter.Instance.playerManager.Player.LeaveStore();
        }
    }

    public void Show()
    {
        if (canEnter)
        {
            Sign.SetActive(false);
            col.enabled = true;
            this.gameObject.SetActive(true);
        }
        else
            CloseStore();
    }

    public void Hide()
    {
        Sign.SetActive(false);
        col.enabled = false;
    }

    public void CloseStore()
    {
        Hide();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnterTrigger(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ExitTrigger(other);
    }

    protected virtual void EnterTrigger(Collider2D other)
    {
        Sign.SetActive(true);
        GameCenter.Instance.playerManager.Player.SetTargetStore(this);
    }

    protected virtual void ExitTrigger(Collider2D other)
    {
        Sign.SetActive(false);
        GameCenter.Instance.playerManager.Player.OutStoreTrigger();
    }

    public void Intact()
    {
        //canEnter = false;

        var player = GameCenter.Instance.playerManager.Player;
        player.CanMove = false;
        player.CanMask = false;
        player.CanIntact = false;

        player.PlayMoveAnim();
        player.SetFaceDir(EnterPoint.position);
        player.transform.DOMove(EnterPoint.position, 1f).OnComplete(() =>
        {
            bool faceLeft = EnterPoint.transform.localScale.x < 0 ? true : false;
            player.SetFaceDir(faceLeft);
            player.PlayIdleAnim();
            EnterStore();
        });

        Hide();
    }

    protected virtual void EnterStore()
    {
        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition(StartStoreAction);
    }

    protected virtual void StartStoreAction()
    {
        TimePassOn = true;
        timer = 0;
        showTime = 10f;
    }

    public void LeaveStore()
    {

        LeaveFunc();

        GameCenter.Instance.uIManager.screenTransitionUI.EndTransition();

        var player = GameCenter.Instance.playerManager.Player;
        player.CanMove = false;
        player.CanAttributeChange = false;
        player.PlayMoveAnim();
        player.SetFaceDir(false);
        player.transform.DOMove(LeavePoint.position, 1f).OnComplete(() =>
        {
            player.CanMove = true;
            player.CanMask = false;
            player.PlayIdleAnim();
        });

    }

    protected virtual void LeaveFunc()
    {

        GameCenter.Instance.sceneManager.CloseAllStore();
        var p = GameCenter.Instance.playerManager.Player;
        p.ChangeMaxSan(-20);
        p.FullHP();
        p.FullSan();

    }

}
