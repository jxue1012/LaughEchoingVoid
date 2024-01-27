using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCenter : MonoBehaviour
{

    public static GameCenter Instance;

    //Managers
    public SceneManager sceneManager;
    public PlayerManager playerManager;
    public CamManager camManager;
    public AudioManager audioManager;
    public VfxManager vfxManager;

    public UIManager uIManager;




    //Variables
    public GlobalSettingSO globalSettingSO;

    private void Awake()
    {
        Instance = this;

        BeforeInit();
        Init();
    }

    private void Start()
    {
        StartNight();
        var p = GameCenter.Instance.playerManager.Player;
        p.CanMask = true;
        p.ChangeStatus(EnumPlayerStatus.Tired);
    }

    private void BeforeInit()
    {

    }

    private void Init()
    {
        playerManager.Init();
        camManager.Init();
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

    }

    public void GameOver(bool success)
    {
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
    }

    public void StartNight()
    {
        sceneManager.home.Show();
        sceneManager.office.Hide();
        sceneManager.ShowAllStore();
        var p = playerManager.Player;
        p.CanMask = true;
        p.CanAttributeChange = true;
    }

    #endregion


}
