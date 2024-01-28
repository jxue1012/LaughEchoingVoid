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

    public void ShowTimeBar()
    {
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

}
