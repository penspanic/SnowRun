using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour, IScene
{

    public Text characterNameText;
    public Text selectedNumberText;
    public Text coinText;
    public Button goButton;

    UIManager uiMgr;

    public Vector2 originalPos
    {
        get
        {
            return new Vector2(720, 0);
        }
    }
    public GameObject sceneObject
    {
        get
        {
            return this.gameObject;
        }
    }
    void Awake()
    {
        uiMgr = GameObject.FindObjectOfType<UIManager>();
    }

    public void OnGoButtonDown()
    {
        uiMgr.SetGameState(GameState.InGame);
    }

    public void Enter()
    {

    }

    public void Leave()
    {

    }

    public void EscapeProcess()
    {
        uiMgr.SetGameState(GameState.Main);
    }
}
