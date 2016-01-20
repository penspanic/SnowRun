using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGame : MonoBehaviour,IUserInterface
{
    public int scoreIncreasement;
    UIManager uiMgr;
    GameManager gameMgr;
    CameraMove cameraMove;

    GameObject pauseUI;
    GameObject gameEndUI;
    GameObject touchArea;

    [HideInInspector] public Player currPlayer;

    public Sprite ready;
    public Sprite start;
    public Image readyStart;

    public Text scoreText;
    public Text moneyText;

    Vector3 scoreTextOriginalSize;
    Vector3 scoreTextMaxSize;
    Vector2 scoreTextOriginalPos;
    public int score
    {
        get;
        private set;
    }
    void Awake()
    {
        uiMgr       = GameObject.FindObjectOfType<UIManager>();
        gameMgr     = GameObject.FindObjectOfType<GameManager>();
        cameraMove  = GameObject.FindObjectOfType<CameraMove>();
        pauseUI     = GameObject.Find("Pause UI");
        gameEndUI   = GameObject.Find("GameEnd UI");   
        touchArea   = GameObject.Find("Touch Area");
        readyStart  = GameObject.Find("Ready Start").GetComponent<Image>();



        pauseUI.SetActive(false);
        gameEndUI.SetActive(false);

        scoreTextOriginalPos = scoreText.GetComponent<RectTransform>().localPosition;
        scoreTextOriginalSize = scoreText.transform.localScale;
        scoreTextMaxSize = scoreText.transform.localScale * 2;
    }
    void Update()
    {
        ScoreTextSizeControl();
    }
    #region Events
    public void Enter()
    {
        Debug.Log("InGame Enter!");
        this.gameObject.SetActive(true);
        readyStart.enabled = true;
        Init();
    }
    public void Leave()
    {
        Debug.Log("InGame Leave!");
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
    void Init()
    {
        isWaitEnd = false;
        Time.timeScale = 1f;
        touchArea.SetActive(true);
        GameObject.FindObjectOfType<ObjectCreate>().Init();
        SetScore(0);
        scoreText.GetComponent<RectTransform>().localPosition = scoreTextOriginalPos;
    }
    public void OnInGameProcess(GameObject character)
    {
        currPlayer = (Instantiate(character, Vector3.zero, Quaternion.Euler(-30, 0, 0)) as GameObject).GetComponent<Player>();
        GameObject deleteZone = GameObject.Find("Delete Zone");

        currPlayer.transform.SetParent(deleteZone.transform);
        currPlayer.transform.localPosition = Camera.ma
        currPlayer.transform.Translate(new Vector3(0, 0, -3));
        currPlayer.transform.SetParent(null);

        StartCoroutine(WaitForSlide());
    }
    bool isWaitEnd = false;
    IEnumerator WaitForSlide()
    {
        readyStart.sprite = ready;
        yield return new WaitForSeconds(1f);
        readyStart.sprite = start;
        yield return new WaitForSeconds(1f);
        readyStart.enabled = false;

        isWaitEnd = true;
        cameraMove.SetFollowPlayer(true);

        yield return new WaitForSeconds(2f);
        currPlayer.OnWaitEnd();
        StartCoroutine(ScoreIncrease());
    }
    IEnumerator ScoreIncrease()
    {
        while(true)
        {
            SetScore(score + scoreIncreasement);
            yield return new WaitForSeconds(1f);
        }
    }
    int prevChangeCipher;
    const int textXMoveValue = 13;
    public void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
        if(score >= 100000 && prevChangeCipher != 100000)
        {
            scoreText.GetComponent<RectTransform>().localPosition += Vector3.right * textXMoveValue;
            prevChangeCipher = 100000;
        }
        else if(score >= 10000 && score < 100000 && prevChangeCipher != 10000)
        {
            scoreText.GetComponent<RectTransform>().localPosition += Vector3.right * textXMoveValue;
            prevChangeCipher = 10000;
        }
        else if(score >= 1000 && score < 10000 && prevChangeCipher != 1000)
        {
            scoreText.GetComponent<RectTransform>().localPosition += Vector3.right * textXMoveValue;
            prevChangeCipher = 1000;

        }
        else if(score >= 100 && score < 1000 && prevChangeCipher != 100)
        {
            scoreText.GetComponent<RectTransform>().localPosition += Vector3.right * textXMoveValue;
            prevChangeCipher = 100;

        }
        else if(score >= 10 &&score < 100 && prevChangeCipher != 10)
        {
            scoreText.GetComponent<RectTransform>().localPosition += Vector3.right * textXMoveValue;
            prevChangeCipher = 10;

        }
        
    }
    public void OnTouchAreaEnter()
    {
        if (!isWaitEnd)
            return;
        currPlayer.OnTouchAreaEnter();
    }
    public void OnAreaExit()
    {
        if (!isWaitEnd)
            return;
        currPlayer.OnTouchAreaExit();
    }
    public void OnGameEnd()
    {
        gameEndUI.SetActive(true);
        touchArea.SetActive(false);

        Time.timeScale = 0f;
        cameraMove.SetFollowPlayer(false);
        StopCoroutine(ScoreIncrease());
    }
    public void OnPauseButtonDown()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        touchArea.SetActive(false);
    }
    public void OnResumeButtonDown()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        touchArea.SetActive(true);
    }
    public void OnQuitButtonDown()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void OnPickButtonDown()
    {
        Destroy(currPlayer.gameObject);
        gameEndUI.SetActive(false);
        uiMgr.SetGameState(GameState.Pick);
    }
    public void OnCharacterButtonDown()
    {
        Destroy(currPlayer.gameObject);
        gameEndUI.SetActive(false);
        uiMgr.SetGameState(GameState.Shop);
    }
    public void OnReplayButtonDown()
    {
        Destroy(currPlayer.gameObject);
        gameEndUI.SetActive(false);
        uiMgr.shop.OnGoButtonDown();
    }
    public void EscapeProcess()
    {
        if (gameEndUI.activeSelf)
            return;
        if (Time.timeScale == 0)
            OnResumeButtonDown();
        else
            OnPauseButtonDown();
    }
    public void PointGet()
    {
        if (scoreText.transform.localScale.magnitude >= scoreTextMaxSize.magnitude)
        {
            return;
        }
        else
        {
            scoreText.transform.localScale += scoreText.transform.localScale * 0.5f;
        }
    }
    public void CoinGet()
    {
        gameMgr.money += 1;
        moneyText.text = gameMgr.money.ToString();
    }
    #endregion
    void ScoreTextSizeControl()
    {
        if(scoreText.transform.localScale.magnitude <= scoreTextOriginalSize.magnitude)
            return;
        scoreText.transform.localScale -= scoreText.transform.localScale * Time.deltaTime;
    }
}
