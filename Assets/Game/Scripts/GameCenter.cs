using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCenter : MonoBehaviour
{

    public static GameCenter Instance;

    //Managers
    public LightManager lightManager;
    public SceneManager sceneManager;
    public PlayerManager playerManager;
    public CamManager camManager;
    public AudioManager audioManager;
    public VfxManager vfxManager;

    public UIManager uIManager;




    //Variables
    public GlobalSettingSO globalSettingSO;
    public int Day;

    private void Awake()
    {
        Instance = this;
        Day = 0;
        BeforeInit();
        Init();
    }

    private void Start()
    {
        uIManager.ShowStartScene();
    }

    private void BeforeInit()
    {

    }

    private void Init()
    {
        playerManager.Init();
        camManager.Init();
        lightManager.Init();
        sceneManager.Init();

        audioManager.Init();
        vfxManager.Init();

        uIManager.Init();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 5f;
        }
    }


    public void StartGame()
    {
        uIManager.screenTransitionUI.EndTransition();
    }

    public void StartGameDelay(float time)
    {
        Invoke("StartGame", time);
        StartNight();
        var p = playerManager.Player;
        p.CanMask = true;
        p.ChangeStatus(EnumPlayerStatus.Tired);
        playerManager.SetNpcToWalkOnStreet();

    }

    [Button]
    public void GameOver(bool success)
    {
        uIManager.screenTransitionUI.StartTransition(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });

        if (success)
        {

        }
        else
        {

        }
    }


    #region --------- Test -----------



    #endregion


    #region --------------- Cursor -----------------
    [Space(15)]
    [Header("------ Cursor ------")]
    public Texture2D CursorTex;

    private void InitCursor()
    {
        Cursor.SetCursor(CursorTex, new Vector2(32f, 32f), CursorMode.Auto);
    }

    public void ShowCursor()
    {
        Cursor.SetCursor(CursorTex, new Vector2(32f, 32f), CursorMode.Auto);
    }

    public void HideCursor()
    {
        Cursor.SetCursor(null, new Vector2(32f, 32f), CursorMode.Auto);
    }

    #endregion

    #region ---------- Time ---------------

    public void StartDay()
    {
        sceneManager.home.Hide();
        sceneManager.office.Show();
        sceneManager.CloseAllStore();
        audioManager.StopBGM();
        audioManager.PlayBGM0(EnumSfxType.BGM0_Street);
    }

    public void StartNight()
    {
        sceneManager.home.Show();
        sceneManager.office.Hide();
        sceneManager.ShowAllStore();
        var p = playerManager.Player;
        p.CanMask = true;
        p.CanAttributeChange = true;
        audioManager.PlayBGM0(EnumSfxType.BGM0_Street);
        audioManager.PlayBGM1(EnumSfxType.BGM1_Night);
    }

    public void SpeedUp()
    {
        Time.timeScale = globalSettingSO.SpeedUpTimeValue;
    }

    public void ResetTimeSpeed()
    {
        Time.timeScale = 1f;
    }

    #endregion


}
