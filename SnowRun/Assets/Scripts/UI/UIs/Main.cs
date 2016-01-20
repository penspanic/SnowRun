using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour,IUserInterface
{
    UIManager uiMgr;
    GameManager gameMgr;

    Button sound;
    Sprite sound_on;
    Sprite sound_off;
    void Awake()
    {
        uiMgr = GameObject.FindObjectOfType<UIManager>();
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        sound   = GameObject.Find("Sound").GetComponent<Button>();

        sound_on  = Resources.Load<Sprite>("UI/sound_on");
        sound_off = Resources.Load<Sprite>("UI/sound_off");
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
    public void OnPlayButtonDown()
    {
        uiMgr.SetGameState(GameState.Shop);
    }
    public void OnLankingButtonDown()
    {

    }
    public void OnSoundButtonDown()
    {
        if(gameMgr.isSoundOn)
        {
            gameMgr.SetSoundOn(false);
            sound.image.sprite = sound_off;
        }
        else
        {
            gameMgr.SetSoundOn(true);
            sound.image.sprite = sound_on;
        }
    }
}
