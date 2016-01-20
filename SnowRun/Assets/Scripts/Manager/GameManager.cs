using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    UIManager uiMgr;
    SoundManager soundMgr;
    public int money
    {
        get;
        set;
    }
    public int highScore
    {
        get;
        set;
    }
    public bool isSoundOn
    {
        get;
        set;
    }
    void Awake()
    {
        Screen.SetResolution(450, 800, false);
        uiMgr = GameObject.FindObjectOfType<UIManager>();
        soundMgr = GameObject.FindObjectOfType<SoundManager>();
        isSoundOn = true;
    }
    void Start()
    {
        uiMgr.SetGameState(GameState.Main);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            uiMgr.EscapeProcess();
        }
    }
    public void SetSoundOn(bool value)
    {
        isSoundOn = value;
        soundMgr.SetSoundOn(value);
    }
    void Load()
    {

    }
    void Save()
    {

    }
}
