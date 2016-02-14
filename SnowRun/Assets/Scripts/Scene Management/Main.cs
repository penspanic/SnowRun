using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour, IScene
{
    UIManager uiMgr;
    GameManager gameMgr;
    SoundManager soundMgr;

    Button soundButton;
    Sprite sound_on;
    Sprite sound_off;

    public Vector2 originalPos
    {
        get
        {
            return new Vector2(0, 0);
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
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        soundMgr = GameObject.FindObjectOfType<SoundManager>();

        soundButton = GameObject.Find("Sound Button").GetComponent<Button>();

        sound_on = Resources.Load<Sprite>("UI/Main/sound_on");
        sound_off = Resources.Load<Sprite>("UI/Main/sound_off");
    }
    void Update()
    {

    }

    public void Enter()
    {
        Debug.Log("Main Enter!");

        this.gameObject.SetActive(true);
    }
    public void Leave()
    {
        Debug.Log("Main Leave!");

        this.gameObject.SetActive(false);
    }
    public void EscapeProcess()
    {

    }

    public void OnPlayButtonDown()
    {
        uiMgr.SetGameState(GameState.Shop);
    }
    public void OnRankingButtonDown()
    {

    }
    public void OnSoundButtonDown()
    {
        if (soundMgr.isSoundOn)
        {
            soundMgr.SetSoundOn(false);
            soundButton.image.sprite = sound_off;
        }
        else
        {
            soundMgr.SetSoundOn(true);
            soundButton.image.sprite = sound_on;
        }
    }
}
