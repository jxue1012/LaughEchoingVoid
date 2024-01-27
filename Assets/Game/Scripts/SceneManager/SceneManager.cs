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

    public Transform LeftSidePoint, RightSidePoint;

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
        foreach (var s in StoreList)
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

    public Vector3 GetRandomPositionForNpc()
    {
        float minX = LeftSidePoint.position.x;
        float maxX = RightSidePoint.position.x;
        float minY = -7f;
        float maxY = -5f;
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector3(x, y, 0);
    }





}
