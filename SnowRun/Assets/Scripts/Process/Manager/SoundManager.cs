using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip[] bgms;


    AudioSource[] sources;
    AudioClip currBgm;

    bool bgmChanging = false;

    public bool isSoundOn
    {
        get;
        private set;
    }


    void Awake()
    {
        sources = GameObject.FindObjectsOfType<AudioSource>();

        currBgm = bgms[Random.Range(0, bgms.Length)];
        bgmSource.clip = currBgm;
        bgmSource.Play();

    }

    void Update()
    {
        if (!bgmSource.isPlaying && !bgmChanging)
            StartCoroutine(BgmChange());
    }

    IEnumerator BgmChange()
    {
        bgmChanging = true;
        yield return new WaitForSeconds(5f);

        AudioClip newBgm;
        while(true)
        {
            newBgm = bgms[Random.Range(0, bgms.Length)];
            if (newBgm != currBgm)
                break;
        }
        currBgm = newBgm;
        bgmSource.clip = currBgm;
        bgmSource.Play();
        bgmChanging = false;
    }

    public void SetSoundOn(bool value)
    {
        isSoundOn = value;

        if (isSoundOn)
        {
            for (int i = 0; i < sources.Length; i++)
                sources[i].volume = 1;
        }
        else
        {
            for (int i = 0; i < sources.Length; i++)
                sources[i].volume = 0;
        }
    }
}
