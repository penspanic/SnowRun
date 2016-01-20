using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;

    void Awake()
    {
    }
    void Update()
    {

    }
    public void SetSoundOn(bool value)
    {
        if(value)
        {
            bgm.volume = 1;
        }
        else
        {
            bgm.volume = 0;
        }
    }
}
