using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Sirenix.OdinInspector.Editor;

public class SceneManager : MonoBehaviour
{
    public GameObject DayScene, NightScene, EndScene;
    public GameObject DayVFX;

    public List<StoreBase> StoreList;

    public Home home;
    public Office office;

    public Transform LeftSidePoint, RightSidePoint;

    //EndScene
    public List<Transform> PlayerPos;//5
    public List<Transform> NpcPosList;//15
    public Transform fallPoint, HangPoint, enterPoint;

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

    private void StartDayScene()
    {
        NightScene.SetActive(false);
        EndScene.SetActive(false);
        DayScene.SetActive(true);

        int day = GameCenter.Instance.Day;
        if (day < 4)
            DayVFX.SetActive(false);
        else
            DayVFX.SetActive(true);
        GameCenter.Instance.lightManager.SetDayVolume(GameCenter.Instance.Day);
    }

    private void StartNightScene()
    {
        DayScene.SetActive(false);
        EndScene.SetActive(false);
        NightScene.SetActive(true);
        GameCenter.Instance.lightManager.SetNightVolume(GameCenter.Instance.Day);
    }

    public void StartEndScene()
    {
        DayScene.SetActive(false);
        NightScene.SetActive(false);
        EndScene.SetActive(true);
        GameCenter.Instance.uIManager.screenTransitionUI.EndTransition(home.StartHomeShow);
        var pm = GameCenter.Instance.playerManager;
        pm.Player.transform.position = enterPoint.position;
        pm.Player.ChangeStatus(EnumPlayerStatus.Nomral);
        pm.SetNpcToEndScene();
    }

    [Button]
    public void LeaveHome()
    {
        GameCenter.Instance.Day += 1;
        StartDayScene();
        home.Hide();

        GameCenter.Instance.uIManager.screenTransitionUI.EndTransition();
        GameCenter.Instance.playerManager.SetNpcToWalkOnStreet();
        var player = GameCenter.Instance.playerManager.Player;
        player.ChangeStatus(EnumPlayerStatus.Nomral);
        player.transform.position = home.LeavePoint.position;
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
        StartNightScene();
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
