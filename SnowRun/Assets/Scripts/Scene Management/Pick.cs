using UnityEngine;
using System.Collections;

public class Pick : MonoBehaviour, IScene
{
    UIManager uiMgr;
    public Vector2 originalPos
    {
        get 
        {
            return new Vector2(0, 1280);
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
