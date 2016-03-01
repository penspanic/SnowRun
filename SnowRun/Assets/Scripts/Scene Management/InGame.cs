using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGame : MonoBehaviour, IScene
{
    [HideInInspector] public Player currPlayer;
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
    
   
    public Transform characterCreateTransform;
    public Image readyStartImage;
    public GameObject touchArea;

    public Text coinText;
    public Text scoreText;

    GameManager gameMgr;

    GameObject pauseUI;
    GameObject gameOverUI;

    public int scoreIncreasement = 5;
    int score = 0;
    CameraMove cameraMove;

    Sprite readySprite;
    Sprite startSprite;

    bool isWaitEnd = false;
    void Awake()
    {
        cameraMove = GameObject.FindObjectOfType<CameraMove>();
        readySprite = Resources.Load<Sprite>("UI/InGame/ready");
        startSprite = Resources.Load<Sprite>("UI/InGame/start");

        gameMgr = GameObject.FindObjectOfType<GameManager>();

        pauseUI = GameObject.Find("Pause UI");
        gameOverUI = GameObject.Find("Game Over UI");
    }

    #region Events

    public void Enter()
    {
        touchArea.SetActive(true);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        score = 0;
        isWaitEnd = false;
    }

    public void Leave()
    {

    }

    public void EscapeProcess()
    {

    }
    
    public void OnTouchAreaEnter()
    {
        if (!isWaitEnd)
            return;
        currPlayer.OnTouchAreaEnter();
    }

    public void OnTouchAreaExit()
    {
        if (!isWaitEnd)
            return;
        currPlayer.OnTouchAreaExit();
    }

    #region Pause UI
    public void OnPauseButtonDown()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResumeButtonDown()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnQuitButtonDown()
    {

    }

    #endregion

    #endregion

    public void GameStart(Player player)
    {
        currPlayer = Instantiate(player);
        currPlayer.transform.position = characterCreateTransform.position;
        currPlayer.transform.localRotation = Quaternion.Euler(-30, 0, 0);
        StartCoroutine(GameStartProces());
    }

    public void GameOver()
    {
        touchArea.SetActive(false);
    }

    public void PointGet(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    public void CoinGet()
    {
        gameMgr.GetCoin();
        coinText.text = gameMgr.coin.ToString();
    }

    IEnumerator GameStartProces()
    {
        readyStartImage.enabled = true;
        readyStartImage.sprite = readySprite;

        yield return new WaitForSeconds(2f);

        readyStartImage.sprite = startSprite;

        yield return new WaitForSeconds(1f);
        readyStartImage.enabled = false;

        isWaitEnd = true;
        cameraMove.SetFollowPlayer(true);

        yield return new WaitForSeconds(2f);
        currPlayer.OnWaitEnd();
        StartCoroutine(ScoreIncrease());

    }

    IEnumerator ScoreIncrease()
    {
        while (true)
        {
            score += scoreIncreasement;
            yield return new WaitForSeconds(1f);
        }
    }

}
