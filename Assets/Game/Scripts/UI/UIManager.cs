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

    public void Init()
    {
        screenTransitionUI.Init();

        UpdateBarFill();

    }

    public void UpdateBarFill()
    {
        var p = GameCenter.Instance.playerManager.Player;
        hpFill.fillAmount = p.HPFillAmount;
        sanFill.fillAmount = p.SanFillAmount;
    }

}
