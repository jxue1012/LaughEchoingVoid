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
        StartNight();
        var p = playerManager.Player;
        p.CanMask = true;
        p.ChangeStatus(EnumPlayerStatus.Tired);
        playerManager.SetNpcToWalkOnStreet();
        
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

    }

    public void GameOver(bool success)
    {
        uIManager.screenTransitionUI.StartTransition();

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
