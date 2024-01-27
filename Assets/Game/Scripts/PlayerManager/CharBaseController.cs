using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class CharBaseController : MonoBehaviour
{
    public EnumCharacterType CharType;
    public GameObject CharRoot;


    private void Update()
    {
        UpdateFunc();
    }

    protected virtual void UpdateFunc()
    {
        UpdateCharTimerFunction();
    }

    public virtual void Init()
    {
        InitSpineControl();
        InitTimerFunction();
    }

    public virtual void Show() => CharRoot.SetActive(true);
    public virtual void Hide() => CharRoot.SetActive(false);

    private void OnDestroy()
    {
        DestoryController();
    }

    protected virtual void DestoryController()
    {

    }



    #region ------------ Spine Control ---------------
    public SkeletonAnimation SA;
    public Spine.AnimationState spineAnimationState;
    [HideInInspector]public bool FaceLeft;
    protected string activeBaseAnim;
    public string ActiveBaseAnim => activeBaseAnim;

    public SpineSkinControl SkinControl;



    protected virtual void InitSpineControl()
    {

        spineAnimationState = SA.AnimationState;

    }

    public void InitializeSkin(int seed)
    {
        Random.InitState(seed);
        SkinControl = this.GetComponentInChildren<SpineSkinControl>();
        SkinControl.Init();
    }

    public void SetFaceDir(Vector2 targetPos)
    {
        float diff = targetPos.x - transform.position.x;
        float absValue = Mathf.Abs(diff);
        //if (diff == 0) return;
        if (absValue < 0.1f) return;

        bool left = diff < 0;
        if (left != FaceLeft)
        {
            SA.skeleton.ScaleX *= -1;
            FaceLeft = !FaceLeft;
        }
    }

    public void SetFaceDir(bool toLeft)
    {
        if (toLeft != FaceLeft)
        {
            SA.skeleton.ScaleX *= -1;
            FaceLeft = !FaceLeft;
        }
    }

    public void ChangeAnimSpeed(float speed)
    {
        SA.timeScale = speed;
    }

    public void ChangeTrackPlaySpeed(int trackIndex, float speed)
    {
        SA.state.GetCurrent(trackIndex).TimeScale = speed;
    }

    public virtual void PlayBaseAnim(EnumAnim animType, bool loop, float speed = 1f, int trackIndex = 0)
    {
        string anim = GameCenter.Instance.playerManager.GetAnimStr(animType);
        if (activeBaseAnim == anim)
            return;

        // EmptyFaceAnimTracks();
        // PlayAlwaysOnFaceAnim();

        SA.AnimationState.SetAnimation(trackIndex, anim, loop);
        activeBaseAnim = anim;

    }

    public void ResetToTargetAnim(EnumAnim animType, int trackIndex = 0)
    {

        string anim = GameCenter.Instance.playerManager.GetAnimStr(animType);
        SA.AnimationState.SetAnimation(trackIndex, anim, true);
        activeBaseAnim = anim;
    }

    public void AppendAnim(EnumAnim animType, bool loop, float speed = 1f, int trackIndex = 0)
    {
        string anim = GameCenter.Instance.playerManager.GetAnimStr(animType);
        SA.AnimationState.AddAnimation(trackIndex, anim, loop, 0);
    }


    public void ChangeSkin(EnumCharSkin skinType)
    {
        SkinControl.ChangeSkin(skinType);
    }


    #endregion




    #region ------------- Timer ----------------

    private event AnyFunc eCharTimerEvent;
    private float maxTime;
    private float timer;
    object[] values;
    private bool timerFunctionOn;

    private void InitTimerFunction()
    {
        eCharTimerEvent = null;
        timerFunctionOn = false;
    }

    public void SetTimerFunction(AnyFunc func, float time, params object[] values)
    {
        timer = 0;
        maxTime = time;
        eCharTimerEvent += func;
        this.values = values;
        timerFunctionOn = true;
    }

    private void UpdateCharTimerFunction()
    {

        if (timerFunctionOn)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                timer = 0;
                eCharTimerEvent?.Invoke(values);

                ClearTimerFunction();
            }
        }
    }

    public void ClearTimerFunction()
    {
        timerFunctionOn = false;
        eCharTimerEvent = null;
        values = null;
    }


    #endregion

}
