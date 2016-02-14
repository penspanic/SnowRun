using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour, IScene
{
    public Player currPlayer;
    public Vector2 originalPos
    {
        get 
        {
            return new Vector2(720, 1280);
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

    }



    #region Events

    public void Enter()
    {

    }

    public void Leave()
    {

    }

    public void EscapeProcess()
    {

    }
    
    public void OnTouchAreaEnter()
    {

    }

    public void OnTouchAreaExit()
    {

    }

    #endregion
}
