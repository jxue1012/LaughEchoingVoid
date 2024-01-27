using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public List<StoreBase> StoreList;

    public Home home;
    public Office office;

    public void Init()
    {
        foreach (var s in StoreList)
            s.Init();

        home.Init();
        office.Init();
    }

    public void CloseAllStore()
    {
        foreach (var s in StoreList)
            s.CloseStore();
    }

    public void ShowAllStore()
    {
        foreach(var s in StoreList)
            s.Show();
    }

    [Button]
    public void LeaveHome()
    {
        home.Hide();

        GameCenter.Instance.uIManager.screenTransitionUI.EndTransition();
        var player = GameCenter.Instance.playerManager.Player;
        player.ChangeStatus(EnumPlayerStatus.Nomral);
        player.PlayMoveAnim();
        player.SetFaceDir(true);
        player.transform.DOMove(home.EnterPoint.position, 1f).OnComplete(() =>
        {
            player.CanMove = true;
            GameCenter.Instance.StartDay();
            player.PlayIdleAnim();
        });

    }

    [Button]
    public void LeaveOffice()
    {
        office.Hide();

        GameCenter.Instance.uIManager.screenTransitionUI.EndTransition();
        var player = GameCenter.Instance.playerManager.Player;
        player.ChangeStatus(EnumPlayerStatus.Tired);
        player.PlayMoveAnim();
        player.SetFaceDir(false);
        player.transform.DOMove(office.EnterPoint.position, 1f).OnComplete(() =>
        {
            player.CanMove = true;
            GameCenter.Instance.StartNight();
            player.PlayIdleAnim();
        });

    }





}
