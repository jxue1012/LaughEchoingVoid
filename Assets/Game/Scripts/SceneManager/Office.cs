using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Office : MonoBehaviour
{
    private Collider2D col;
    public Collider2D blocker;

    public Transform EnterPoint;
    public Transform LeavePoint;

    private bool timePassOn;
    protected float showTime;
    protected float timer;


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

    private void Update()
    {
        if (timePassOn)
        {
            timer += Time.deltaTime;
            GameCenter.Instance.uIManager.UpdateTimeBar(timer / showTime);
            if (timer >= showTime)
            {
                timer = 0f;
                timePassOn = false;
                GameCenter.Instance.uIManager.HideTimeBar();
                GameCenter.Instance.sceneManager.LeaveOffice();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = GameCenter.Instance.playerManager.Player;
        player.CanMove = false;
        player.CanMask = false;
        player.CanAttributeChange = false;
        player.PlayMoveAnim();
        player.transform.DOMove(LeavePoint.position, 1f);
        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition(StartWork);
    }

    private void StartWork()
    {
        timePassOn = true;
        timer = 0;
        showTime = GameCenter.Instance.globalSettingSO.WorkShowTime;
        GameCenter.Instance.uIManager.ShowTimeBar();
    }

}
