using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCenter : MonoBehaviour
{

    public static GameCenter Instance;

    //Managers
    public PlayerManager playerManager;
    public CamManager camManager;
    public AudioManager audioManager;
    public VfxManager vfxManager;




    //Variables

    private void Awake()
    {
        Instance = this;

        BeforeInit();
        Init();
    }

    private void Start()
    {

    }

    private void BeforeInit()
    {

    }

    private void Init()
    {
        playerManager.Init();
        camManager.Init();

        audioManager.Init();
        vfxManager.Init();

    }

    private void Update()
    {

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


    // #region ----------- LayerMask -----------------
    // //Layermask
    // [Space(15)]
    // [Header("------ Layer Mask ------")]

    // public bool CheckObjInLayer(GameObject obj, LayerMask lm)
    // {
    //     return obj.layer == Mathf.Log(lm.value, 2);
    // }

    // #endregion

    // #region ---------- Sorting Order ------------

    // [Space(15)]
    // [Header("------ Sorting Order ------")]
    // public int BackGroundOrder = 0;

    // public int JigsawNormalOrder = 10;
    // public int JigsawDragOrder = 100;


    // #endregion

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



}
