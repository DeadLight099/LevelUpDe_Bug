using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Music[] _musicClips;
    public static MusicManager instance;
    public bool shouldNotDestroy = true;

    private int _prevMusic;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if(shouldNotDestroy )
        {
            DontDestroyOnLoad(gameObject);
        }


        foreach (Music music in _musicClips)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.gameObject.SetActive(true);
            music.source.clip = music.clip;
            music.source.volume = music.volume;
            music.source.pitch = music.pitch;
        }
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        int randomNum = UnityEngine.Random.Range(0, _musicClips.Length);
        while (randomNum == _prevMusic)
        {
            randomNum = UnityEngine.Random.Range(0, _musicClips.Length);
            //StartCoroutine(PlayMusic());
        }

        Music m = Array.Find(_musicClips, music => music.number == randomNum);
        if (m == null)
        {
            Debug.LogWarning("Wrong sound name!");
            yield break;
        }
        m.source.Play();
        yield return new WaitUntil(() => m.source.isPlaying == false);
        _prevMusic = randomNum;
        m.source.Stop();
        StartCoroutine(PlayMusic());
    }
    public void DestroyMusicManager()
    {
        Destroy(gameObject);
    }
}
