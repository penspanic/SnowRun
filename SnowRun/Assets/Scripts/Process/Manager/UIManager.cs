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
    public Main main;
    public Shop shop;
    public InGame inGame;
    public Pick pick;

    public GameObject scenes;
    public GameState state
    {
        get;
        private set;
    }


    IScene prevScene;
    IScene currScene;

    bool moveEnd = true;
    void Awake()
    {
        currScene = GameObject.FindObjectOfType<Main>() as IScene;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            currScene.EscapeProcess();
    }
    public void SetGameState(GameState state)
    {
        if (!moveEnd)
            return;

        prevScene = currScene;

        this.state = state;

        switch (this.state)
        {
            case GameState.Main:
                currScene = main;
                break;
            case GameState.Shop:
                currScene = shop;
                break;
            case GameState.Pick:
                currScene = pick;
                break;
            case GameState.InGame:
                currScene = inGame;
                break;
        }
        StartCoroutine(SceneMove());
    }

    IEnumerator SceneMove()
    {
        moveEnd = false;
        currScene.Enter();

        Vector2 startPos = scenes.transform.localPosition;
        Vector2 endPos = currScene.originalPos * -1;
        float elapsedTime = 0f;
        const float moveTime = 1f;

        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            scenes.transform.localPosition = EasingUtil.EaseVector2(
                EasingUtil.easeInCubic, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }
        scenes.transform.localPosition = endPos;

        if (prevScene != null && prevScene != currScene)
            prevScene.Leave();
        moveEnd = true;
    }
}