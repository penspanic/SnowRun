using UnityEngine;
using System.Collections;

public enum GameState
{
    Main,
    Shop,
    Pick,
    InGame,
}
public class UIManager : MonoBehaviour
{
    #region Properties

    public IUserInterface currentUI
    {
        get;
        private set;
    }
    IUserInterface prevUI
    {
        get;
        set;
    }
    public GameState state
    {
        get;
        private set;
    }

    public InGame inGame
    {
        get;
        private set;
    }
    public Main main
    {
        get;
        private set;
    }
    public Pick pick
    {
        get;
        private set;
    }
    public Shop shop
    {
        get;
        private set;
    }
    public GameObject UIs
    {
        get;
        private set;
    }
    
    #endregion

    void Awake()
    {
        inGame  = GameObject.FindObjectOfType<InGame>();
        main    = GameObject.FindObjectOfType<Main>();
        pick    = GameObject.FindObjectOfType<Pick>();
        shop    = GameObject.FindObjectOfType<Shop>();
        UIs     = GameObject.Find("UIs");

    }
    void Start()
    {

        inGame.gameObject.SetActive(false);
        main.gameObject.SetActive(false);
        pick.gameObject.SetActive(false);
        shop.gameObject.SetActive(false);
    }
    void Update()
    {

    }
    public void SetGameState(GameState state)
    {
        //if (currentUI != null)
        //    currentUI.Leave();

        this.state = state;
        prevUI = currentUI;
        switch(this.state)
        {
            case GameState.Main:
                currentUI = main;
                StartCoroutine(CoroutineManager.LerpMove(UIs.gameObject, UIs.transform.localPosition, new Vector3(0, 0, 0), 2f, false, true, this.gameObject, "OnUIMoveEnd"));
                break;
            case GameState.Shop:
                currentUI = shop;
                StartCoroutine(CoroutineManager.LerpMove(UIs.gameObject, UIs.transform.localPosition, new Vector3(-720, 0, 0), 2f, false, true, this.gameObject, "OnUIMoveEnd"));
                break;
            case GameState.InGame:
                currentUI = inGame;
                StartCoroutine(CoroutineManager.LerpMove(UIs.gameObject, UIs.transform.localPosition, new Vector3(-720, -1280, 0), 2f, false, true, this.gameObject, "OnUIMoveEnd"));
                break;
            case GameState.Pick:
                currentUI = pick;
                StartCoroutine(CoroutineManager.LerpMove(UIs.gameObject, UIs.transform.localPosition, new Vector3(0, -1280, 0), 2f, false, true, this.gameObject, "OnUIMoveEnd"));
                break;
        }
        currentUI.Enter();
    }
    public void OnUIMoveEnd()   // Leave 실행
    {
        if (prevUI != null && prevUI != currentUI)
            prevUI.Leave();
        switch(this.state)
        {
            case GameState.Main:

                break;
            case GameState.Shop:

                break;
            case GameState.InGame:

                break;
            case GameState.Pick:

                break;
        }
    }
    public void EscapeProcess()
    {
        switch(this.state)
        {
            case GameState.Main:

                break;
            case GameState.Shop:
                SetGameState(GameState.Main);
                break;
            case  GameState.InGame:
                inGame.EscapeProcess();
                break;
            case GameState.Pick:
                SetGameState(GameState.Main);
                break;
        }
    }
}
