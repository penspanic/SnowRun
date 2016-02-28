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

        touchArea.SetActive(false);
    }

    #region Events

    public void Enter()
    {
        touchArea.SetActive(true);
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
    }

    public void CoinGet()
    {

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
