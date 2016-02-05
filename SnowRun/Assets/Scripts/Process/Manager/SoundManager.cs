using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;

    public bool isSoundOn
    {
        get;
        private set;
    }
    void Awake()
    {
    }
    void Update()
    {

    }
    public void SetSoundOn(bool value)
    {
        isSoundOn = value;

        if (isSoundOn)
        {
            bgm.volume = 1;
        }
        else
        {
            bgm.volume = 0;
        }
    }
}
