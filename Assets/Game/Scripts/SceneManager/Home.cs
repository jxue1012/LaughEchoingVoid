using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Networking.Types;

public class Home : MonoBehaviour
{

    private Collider2D col;
    public Collider2D blocker;

    public Transform EnterPoint;
    public Transform LeavePoint;
    public float distanceSpeed = 0.25f;

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
        GameCenter.Instance.sceneManager.CloseAllStore();
        var player = GameCenter.Instance.playerManager.Player;
        player.CanMove = false;
        player.CanMask = false;
        player.CanAttributeChange = false;
        player.PlayMoveAnim();
        player.transform.DOMove(LeavePoint.position, 1f).OnComplete(() =>
        {
            GameCenter.Instance.sceneManager.StartEndScene();
        });
        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition();
    }

    public void StartHomeShow()
    {
        int day = GameCenter.Instance.Day;
        Vector3 playerPos = GameCenter.Instance.sceneManager.GetPlayerPos(day);
        var player = GameCenter.Instance.playerManager.Player;
        player.PlayBaseAnim(EnumAnim.NormalMove, true);
        player.SetFaceDir(true);
        float moveTime = Vector3.Distance(playerPos, player.transform.position) * distanceSpeed;
        player.transform.DOMove(playerPos, moveTime).OnComplete(() =>
        {
            player.PlayBaseAnim(EnumAnim.NormalIdle, true);
            StartDie();
        });
    }

    private void StartDie()
    {
        int day = GameCenter.Instance.Day;
        var player = GameCenter.Instance.playerManager.Player;
        if (player.San == 0)
        {
            //Player Die San
            Transform diePoint = GameCenter.Instance.sceneManager.fallPoint;
            float moveTime = Vector3.Distance(player.transform.position, diePoint.position) * distanceSpeed;
            player.PlayBaseAnim(EnumAnim.NormalMove, true);
            player.transform.DOMove(diePoint.position, moveTime).OnComplete(() =>
            {
                player.PlayerDie(true);
            });
        }
        else if (player.HP == 0)
        {
            //Player Die HP
            Transform diePoint = GameCenter.Instance.sceneManager.fallPoint;
            float moveTime = Vector3.Distance(player.transform.position, diePoint.position) * distanceSpeed;
            player.PlayBaseAnim(EnumAnim.NormalMove, true);

            player.PlayerDie(false);

        }
        else
        {
            StartCoroutine(NpcDie());
        }
    }

    IEnumerator NpcDie()
    {
        //NPC Die
        deadNpcNum = 0;
        int restNum = GameCenter.Instance.Day + 1;
        restNum = Mathf.Clamp(restNum, 0, 6);
        int index = 0;
        Transform diePoint = GameCenter.Instance.sceneManager.fallPoint;
        var player = GameCenter.Instance.playerManager.Player;
        bool isFall = player.San > player.HP ? false : true;
        while (restNum > 0)
        {
            restNum -= 1;
            index += 1;
            var npc = GameCenter.Instance.playerManager.NpcList[index];
            float moveTime = Vector3.Distance(npc.transform.position, diePoint.position) * distanceSpeed;
            npc.PlayBaseAnim(EnumAnim.NPC_MaskMove, true);
            if (isFall)
            {
                npc.transform.DOMove(diePoint.position, moveTime).OnComplete(() =>
                {
                    npc.NpcDie(isFall, NpcDead);
                });
            }
            else
            {
                npc.NpcDie(isFall, NpcDead);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private int deadNpcNum;

    public void NpcDead()
    {
        deadNpcNum += 1;
        if (deadNpcNum >= GameCenter.Instance.Day + 1)
        {
            EndHomeShow();
        }
    }

    public void EndHomeShow()
    {

        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition(() =>
        {
            StartSleep();
        });

    }

    private void LeaveHomeDelay()
    {
        int value = GameCenter.Instance.globalSettingSO.SleepGain;
        GameCenter.Instance.playerManager.Player.ChangeHP(value);
        GameCenter.Instance.playerManager.Player.ChangeSan(value);

        GameCenter.Instance.sceneManager.LeaveHome();
    }

    protected bool timePassOn;
    protected float showTime;
    protected float timer;

    private void StartSleep()
    {
        timePassOn = true;
        timer = 0;
        showTime = GameCenter.Instance.globalSettingSO.SleepShowTime;
        GameCenter.Instance.uIManager.ShowTimeBar(4);

        GameCenter.Instance.audioManager.StopBGM();
        GameCenter.Instance.audioManager.PlayBGM0(EnumSfxType.BGM1_Sleep);

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
                LeaveHomeDelay();
            }
        }
    }




}
