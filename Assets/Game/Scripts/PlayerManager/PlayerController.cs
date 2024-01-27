using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharBaseController
{

    public EnumPlayerStatus status;

    public override void Init()
    {
        base.Init();
        canMove = true;
        status = EnumPlayerStatus.Nomral;
    }

    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        UpdateMove();
    }


    #region ----------- Move -----------------
    [Space(20)]
    [Header("------- Move -------")]
    private bool canMove;
    public float MoveSpeed = 10f;


    private void UpdateMove()
    {
        if (!canMove) return;

        float hValue = Input.GetAxisRaw("Horizontal");

        if (hValue == 0)
        {
            PlayIdleAnim();
        }
        else
        {
            bool faceLeft = hValue > 0 ? false : true;
            SetFaceDir(faceLeft);
            PlayMoveAnim();
        }

        Vector3 dir = new Vector2(hValue, 0f).normalized;
        this.transform.position += dir * MoveSpeed * Time.deltaTime;

    }

    private void PlayIdleAnim()
    {
        EnumAnim animType = EnumAnim.NormalIdle;
        switch (status)
        {
            case EnumPlayerStatus.Nomral:
                break;

            case EnumPlayerStatus.Mask:
                animType = EnumAnim.MaskIdle;
                break;

            case EnumPlayerStatus.Drug:
                animType = EnumAnim.DrugIdle;
                break;

            case EnumPlayerStatus.Drunk:
                animType = EnumAnim.DrunkIdle;
                break;

            case EnumPlayerStatus.H:
                animType = EnumAnim.HIdle;
                break;
        }

        PlayBaseAnim(animType, true);
    }

    private void PlayMoveAnim()
    {
        EnumAnim animType = EnumAnim.NormalMove;
        switch (status)
        {
            case EnumPlayerStatus.Nomral:
                break;

            case EnumPlayerStatus.Mask:
                animType = EnumAnim.MaskMove;
                break;

            case EnumPlayerStatus.Drug:
                animType = EnumAnim.DrugMove;
                break;

            case EnumPlayerStatus.Drunk:
                animType = EnumAnim.DrunkMove;
                break;

            case EnumPlayerStatus.H:
                animType = EnumAnim.HMove;
                break;
        }

        PlayBaseAnim(animType, true);
    }

    #endregion

}
