using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : CharBaseController
{

    public Transform HeadPoint;
    public SortingGroup sortGroup;
    private Rigidbody2D rb;

    public override void Init()
    {
        base.Init();
        rb = GetComponent<Rigidbody2D>();
        CanMove = true;
        ChangeStatus(EnumPlayerStatus.Nomral);
        InitAttributes();
        InitFootstep();

    }

    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        SetSortOrder();
        UpdateMove();

        if (Input.GetKeyDown(KeyCode.Space) && CanIntact)
        {
            targetStore.Intact();
        }

        if (CanMask && Input.GetMouseButtonDown(1))
        {
            SwitchMask();
            CanMask = false;
            CanMove = false;
            if (CanIntact)
            {
                CanIntact = false;
                lastIntactFlag = true;
            }
        }

        UpdateAttributes();
    }

    public void SetSortOrder()
    {
        sortGroup.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    #region ----------- Status --------------

    public EnumPlayerStatus status;
    private PlayerStatusInfo statusInfo;
    [Button]
    public void ChangeStatus(EnumPlayerStatus newStatus)
    {

        status = newStatus;
        statusInfo = GameCenter.Instance.playerManager.GetPlayerStatusInfo(newStatus);
        moveSpeed = statusInfo.moveSpeed;
        GameCenter.Instance.playerManager.ChangeNpcStatus(newStatus);
        GameCenter.Instance.lightManager.SetStatusVolume(status);
    }

    #endregion


    #region ----------- Move -----------------
    [Space(20)]
    [Header("------- Move -------")]
    [HideInInspector] public bool CanMove;
    private Vector2 autoMoveDir = new Vector2(-1, 0f);
    private float moveSpeed = 5f;
    private bool autoMoveOn;


    private void UpdateMove()
    {
        if (autoMoveOn) return;

        if (!CanMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float hValue = Input.GetAxisRaw("Horizontal");

        if (hValue == 0)
        {
            PlayBaseAnim(statusInfo.idleAnimType, true);
        }
        else
        {
            bool faceLeft = hValue > 0 ? false : true;
            SetFaceDir(faceLeft);
            PlayBaseAnim(statusInfo.moveAnimType, true);
        }

        Vector3 dir = new Vector2(hValue, 0f).normalized;
        //this.transform.position += dir * moveSpeed * Time.deltaTime;
        rb.velocity = dir * moveSpeed;
    }

    public void PlayIdleAnim()
    {
        PlayBaseAnim(statusInfo.idleAnimType, true);
    }

    public void PlayMoveAnim()
    {
        PlayBaseAnim(statusInfo.moveAnimType, true);
    }

    public void StartAutoMoveToOffice()
    {
        autoMoveDir = new Vector2(-1, 0f);
        autoMoveOn = true;
        CanMove = false;
        rb.velocity = autoMoveDir * GameCenter.Instance.globalSettingSO.PlayerAutoMoveSpeed;
        PlayBaseAnim(statusInfo.moveAnimType, true);
    }

    public void StopAutoMove()
    {
        autoMoveOn = false;
        rb.velocity = Vector3.zero;
    }


    #endregion

    #region --------- Intact -------------

    public bool CanIntact;
    private StoreBase targetStore;

    public void SetTargetStore(StoreBase store)
    {
        targetStore = store;
        CanIntact = true;
    }

    [Button]
    public void LeaveStore()
    {
        targetStore.LeaveStore();
        targetStore = null;
        CanIntact = false;
    }

    public void OutStoreTrigger()
    {
        if (CanMove)
        {
            CanIntact = false;
            targetStore = null;
        }
    }

    #endregion

    #region ---------- Mask -----------

    public bool CanMask;
    private bool lastIntactFlag;
    private EnumPlayerStatus statusBeforeMask;

    private void SwitchMask()
    {
        if (status == EnumPlayerStatus.Mask)
            MaskOff();
        else
            MaskOn();

    }

    public void MaskOn()
    {
        SA.AnimationState.Complete += MaskOnCallback;
        PlayBaseAnim(EnumAnim.MaskOn, false);
    }

    private void MaskOnCallback(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.MaskOn))
        {
            statusBeforeMask = status;
            ChangeStatus(EnumPlayerStatus.Mask);
            CanMask = true;
            CanMove = true;
            if (lastIntactFlag)
                CanIntact = true;
            SA.AnimationState.Complete -= MaskOnCallback;
        }
    }

    public void MaskOff()
    {
        SA.AnimationState.Complete += MaskOffCallback;
        PlayBaseAnim(EnumAnim.MaskOff, false);
    }

    private void MaskOffCallback(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.MaskOff))
        {
            ChangeStatus(statusBeforeMask);
            CanMask = true;
            CanMove = true;
            if (lastIntactFlag)
                CanIntact = true;
            SA.AnimationState.Complete -= MaskOnCallback;
        }
    }

    #endregion

    #region ----------- Attributes -------------

    public int HP;
    public int MaxHP { get; private set; }
    private int DefaultMaxHP = 100;
    public float HPFillAmount => HP * 1f / DefaultMaxHP;

    private float hpTimer;
    private float hpTime;

    public int San;
    public int MaxSan { get; private set; }
    private int DefaultMaxSan = 100;
    public float SanFillAmount => San * 1f / DefaultMaxSan;

    private float sanTimer;
    private float sanTime;

    private float defaultAttributeTime = 1f;
    private int AttributeLoseValue = 1;
    public bool CanAttributeChange;

    private void InitAttributes()
    {
        HP = MaxHP = DefaultMaxHP;
        San = MaxSan = DefaultMaxSan;
        CanAttributeChange = true;
        defaultAttributeTime = GameCenter.Instance.globalSettingSO.defaultAttributeLoseTime;
        AttributeLoseValue = GameCenter.Instance.globalSettingSO.AttributeLoseValue;
    }

    public void ResetAttributeTimer()
    {
        hpTimer = hpTime = sanTimer = sanTime = 0;
        CanAttributeChange = true;
    }

    private void UpdateAttributes()
    {
        if (!CanAttributeChange) return;

        if (rb.velocity == Vector2.zero) return;

        if (status == EnumPlayerStatus.Mask)
        {
            sanTimer += Time.deltaTime;
            if (sanTimer >= defaultAttributeTime)
            {
                sanTimer = 0;
                ChangeSan(-AttributeLoseValue);
            }
        }
        else if (status == EnumPlayerStatus.Tired)
        {
            hpTimer += Time.deltaTime;
            if (hpTimer >= defaultAttributeTime)
            {
                hpTimer = 0;
                ChangeHP(-AttributeLoseValue);
            }
        }
    }

    public void ChangeHP(int change)
    {
        int temp = HP + change;
        HP = Mathf.Clamp(temp, 0, MaxHP);
        GameCenter.Instance.uIManager.UpdateBarFill();
        if (HP == 0)
            CanAttributeChange = false;
    }

    public void ChangeMaxHP(int change)
    {
        int temp = MaxHP + change;
        MaxHP = Mathf.Clamp(temp, 0, DefaultMaxHP);
        HP = Mathf.Min(HP, MaxHP);
        GameCenter.Instance.uIManager.UpdateBarFill();
        if (HP == 0)
            CanAttributeChange = false;
    }

    public void FullHP()
    {
        HP = MaxHP;
        GameCenter.Instance.uIManager.UpdateBarFill();
    }

    public void ChangeSan(int change)
    {
        int temp = San + change;
        San = Mathf.Clamp(temp, 0, MaxSan);
        GameCenter.Instance.uIManager.UpdateBarFill();
        if (San == 0)
        {
            ChangeStatus(EnumPlayerStatus.Crazy);
            CanAttributeChange = false;
        }
    }

    public void ChangeMaxSan(int change)
    {
        int temp = MaxSan + change;
        MaxSan = Mathf.Clamp(temp, 0, DefaultMaxSan);
        San = Mathf.Min(San, MaxSan);
        GameCenter.Instance.uIManager.UpdateBarFill();
        if (San == 0)
        {
            ChangeStatus(EnumPlayerStatus.Crazy);
            CanAttributeChange = false;
        }
    }

    public void FullSan()
    {
        San = MaxSan;
        GameCenter.Instance.uIManager.UpdateBarFill();
    }




    #endregion

    #region  ------------ Die -----------------

    public void PlayerDie(bool isFall)
    {
        if (isFall)
        {
            SA.AnimationState.Complete += OnFallDieComplete;
            PlayBaseAnim(EnumAnim.PlayerFallDie, false);
        }
        else
        {
            SA.AnimationState.Complete += OnHangDieComplete;
            PlayBaseAnim(EnumAnim.PlayerHangDie, false);
        }
    }

    private void OnFallDieComplete(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.PlayerFallDie))
        {
            Hide();
            GameCenter.Instance.GameOver(true);
            SA.AnimationState.Complete -= OnFallDieComplete;
        }
    }

    private void OnHangDieComplete(TrackEntry entry)
    {
        if (entry.Animation.Name == GameCenter.Instance.playerManager.GetAnimStr(EnumAnim.PlayerHangDie))
        {
            Hide();
            GameCenter.Instance.GameOver(true);
            SA.AnimationState.Complete -= OnHangDieComplete;
        }
    }

    #endregion

    #region --------- Footstep -------------

    private void InitFootstep()
    {
        SA.AnimationState.Event += HandleSpineEvent;
    }

    void HandleSpineEvent(TrackEntry trackEntry, Spine.Event e)
    {
        // 检查事件名称
        if (e.Data.Name == "foot")
        {
            GameCenter.Instance.audioManager.PlayFootStep();

        }
    }

    #endregion

}

