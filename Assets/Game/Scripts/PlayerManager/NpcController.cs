using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NpcController : CharBaseController
{

    public override void Init()
    {
        base.Init();
        SkinControl.Init();
        sortGroup = this.GetComponent<SortingGroup>();
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        canMove = false;
    }

    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        UpdateMove();
    }

    public void SetSortOrder()
    {
        sortGroup.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    public NpcSpineSkinControl SkinControl;
    public SortingGroup sortGroup;
    private Transform moveTarget;
    private bool canMove;
    private EnumPlayerStatus status;
    public string MoveAnim { get; private set; }
    private Vector3 moveDir;
    private float moveSpeed;
    private bool moveLeft;

    public void StartMove(bool toLeft, bool initMove)
    {
        Show();

        if (initMove)
        {
            transform.position = GameCenter.Instance.sceneManager.GetRandomPositionForNpc();
            SetSortOrder();
            float rand = Random.Range(0, 100f);
            toLeft = rand > 50f ? true : false;
        }

        SetMoveAnim();
        canMove = true;
        var so = GameCenter.Instance.globalSettingSO;
        moveSpeed = Random.Range(so.NpcMoveMinSpeed, so.NpcMoveMaxSpeed);
        moveLeft = toLeft;

        if (moveLeft)
        {
            moveTarget = GameCenter.Instance.sceneManager.LeftSidePoint;
            SetFaceDir(true);
            moveDir = new Vector2(-1, 0);
        }
        else
        {
            moveTarget = GameCenter.Instance.sceneManager.RightSidePoint;
            SetFaceDir(false);
            moveDir = new Vector2(1, 0);
        }
    }

    public void StopMove()
    {
        canMove = false;
        PlayBaseAnim(EnumAnim.Npc_Idle, true);
    }

    private void UpdateMove()
    {
        if (!canMove) return;

        this.transform.position += moveDir * moveSpeed * Time.deltaTime;
        PlayBaseAnim(MoveAnim, true);
        if (moveLeft)
        {
            if (this.transform.position.x < moveTarget.position.x)
            {
                StartMove(!moveLeft, false);
                SkinControl.GenerateSkin();
            }
        }
        else
        {
            if (this.transform.position.x > moveTarget.position.x)
            {
                StartMove(!moveLeft, false);
                SkinControl.GenerateSkin();
            }
        }
    }

    public void ChangePlayerStatus(EnumPlayerStatus newStatus)
    {
        status = newStatus;
        SetMoveAnim();
    }

    private void SetMoveAnim()
    {
        var pm = GameCenter.Instance.playerManager;
        switch (status)
        {
            case EnumPlayerStatus.Nomral:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_NormalMove);
                break;

            case EnumPlayerStatus.Mask:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_MaskMove);
                break;

            case EnumPlayerStatus.Drug:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_DrugMove);
                break;

            case EnumPlayerStatus.Drunk:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_DrunkMove);
                break;

            case EnumPlayerStatus.H:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_HMove);
                break;

            case EnumPlayerStatus.Tired:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_TireMove);
                break;

            case EnumPlayerStatus.Crazy:
                MoveAnim = pm.GetAnimStr(EnumAnim.NPC_CrazyMove);
                break;
        }
    }

    Action dieCallback;

    public void NpcDie(bool isFall, Action callBack)
    {
        dieCallback = callBack;
        if (isFall)
        {
            SA.AnimationState.Complete += OnFallDieComplete;
            PlayBaseAnim(EnumAnim.NPC_FallDie, false);
        }
        else
        {
            SA.AnimationState.Complete += OnHangDieComplete;
            PlayBaseAnim(EnumAnim.NPC_HangDie, false);
        }
    }

    private void OnFallDieComplete(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.NPC_FallDie))
        {
            Hide();
            dieCallback?.Invoke();
            SA.AnimationState.Complete -= OnFallDieComplete;
        }
    }

    private void OnHangDieComplete(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.NPC_HangDie))
        {
            Hide();
            dieCallback?.Invoke();
            SA.AnimationState.Complete -= OnHangDieComplete;
        }
    }

    #region ----------- Chat bOX -----------
    [Space(15)]
    public Image ChatBoxImg;
    private bool chatStart;
    public void StartChat()
    {
        chatStart = true;

        StartCoroutine(ChatCo());
    }

    IEnumerator ChatCo()
    {
        while (chatStart)
        {
            var list = GameCenter.Instance.globalSettingSO.ChatBoxList;
            int rand = Random.Range(0, list.Count);
            var sprite = list[rand];
            ChatBoxImg.sprite = sprite;
            float time = Random.Range(0.5f, 1.5f);
            ChatBoxImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.75f);
            ChatBoxImg.gameObject.SetActive(false);
            yield return new WaitForSeconds(time);

        }
    }

    public void EndChat()
    {
        chatStart = false;
        ChatBoxImg.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    #endregion



}
