using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ScreenTransitionUI screenTransitionUI;

    public Image hpFill;
    public Image sanFill;

    public GameObject TimeBar;
    public Image timeBarFill;

    public void Init()
    {
        screenTransitionUI.Init();

        UpdateBarFill();

        HideTimeBar();

    }

    public void UpdateBarFill()
    {
        var p = GameCenter.Instance.playerManager.Player;
        hpFill.fillAmount = p.HPFillAmount;
        sanFill.fillAmount = p.SanFillAmount;
    }

    public void ShowTimeBar(int index)
    {
        var so = GameCenter.Instance.globalSettingSO;
        switch (index)
        {
            case 0://drug
                timeBarFill.color = so.DrugUIColor;
                break;

            case 1://drink
                timeBarFill.color = so.DrinkUIColor;
                break;

            case 2: //H
                timeBarFill.color = so.HUIColor;
                break;

            case 3: //Work
                timeBarFill.color = so.WorkUIColor;
                break;

        }
        TimeBar.SetActive(true);
        UpdateTimeBar(0);
    }

    public void HideTimeBar()
    {
        TimeBar.SetActive(false);
    }

    public void UpdateTimeBar(float value)
    {
        timeBarFill.fillAmount = value;
    }

    #region ---------- Start Scene ------------

    public Transform HidePoint;
    public Transform EnterPoint;

    public GameObject StartScene;
    public GameObject ButtonSign;

    public void ShowStartScene()
    {
        GameCenter.Instance.audioManager.PlayBGM0(EnumSfxType.BGM0_MainScreen);
        StartScene.SetActive(true);
        ButtonSign.SetActive(false);
    }

    public void HideStartScene()
    {
        StartScene.SetActive(false);

    }

    public void OnStartBtnClick()
    {

        GameCenter.Instance.uIManager.screenTransitionUI.StartTransition(() =>
        {
            HideStartScene();
            GameCenter.Instance.StartGameDelay(0.5f);
        });

    }

    #endregion

}
