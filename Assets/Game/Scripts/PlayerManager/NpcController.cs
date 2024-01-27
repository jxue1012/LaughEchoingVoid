using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering;

public class NpcController : CharBaseController
{

    public override void Init()
    {
        base.Init();
        SkinControl.Init();
        StartMove(true, true);
        sortGroup = this.GetComponent<SortingGroup>();
    }

    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        UpdateMove();
    }

    public NpcSpineSkinControl SkinControl;
    public SortingGroup sortGroup;
    private Transform moveTarget;
    private bool canMove;
    private EnumPlayerStatus status;
    private string moveAnim;
    private Vector3 moveDir;
    private float moveSpeed;
    private bool moveLeft;

    public void StartMove(bool toLeft, bool initMove)
    {
        if (initMove)
        {
            transform.position = GameCenter.Instance.sceneManager.GetRandomPositionForNpc();
            sortGroup.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
            float rand = Random.Range(0, 100f);
            toLeft = rand > 50f ? true : false;
        }

        status = EnumPlayerStatus.Nomral;
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
        PlayBaseAnim(moveAnim, true);
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
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_NormalMove);
                break;

            case EnumPlayerStatus.Mask:
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_MaskMove);
                break;

            case EnumPlayerStatus.Drug:
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_DrugMove);
                break;

            case EnumPlayerStatus.Drunk:
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_DrunkMove);
                break;

            case EnumPlayerStatus.H:
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_HMove);
                break;

            case EnumPlayerStatus.Tired:
                moveAnim = pm.GetAnimStr(EnumAnim.NPC_TireMove);
                break;
        }
    }



}
