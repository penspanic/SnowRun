using UnityEngine;
using System.Collections;

public enum GameState
{
    Main,
    Shop,
    Pick,
    Ingame,
}
public class UIManager : MonoBehaviour
{
    public GameState state
    {
        get;
        private set;
    }

    public void SetGameState(GameState state)
    {
        this.state = state;

    }
}