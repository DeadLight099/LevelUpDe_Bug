using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator anim;
    public bool isCanPress;
    private string levelToLoad;

    private void Start()
    {
        levelToLoad = "";
        if (!PlayerPrefs.HasKey("isLoaded"))
        {
            PlayerPrefs.SetInt("isLoaded", 0);
        }
        Debug.Log(PlayerPrefs.GetInt("isLoaded") == 0);
        if(PlayerPrefs.GetInt("isLoaded") == 0)
        {
            FadeOut();
        }
    }
    private void Update()
    {
        if (!isCanPress)
        {
            Debug.Log("You can`t send request with buttons");
        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                ResetScene();
            }
            if (Input.GetKey(KeyCode.Backspace))
            {
                LoadLevel("MainMenu");
            }
        }

    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
        PlayerPrefs.SetInt("isLoaded", 1);
    }
    public void LoadLevel(string levelName)
    {
        levelToLoad = levelName;
        if(levelToLoad == "MainMenu" || levelToLoad == "FinalComic" || levelToLoad == "Level1" || levelToLoad == "LevelBoss")
        {
            if(MusicManager.instance != null) MusicManager.instance.DestroyMusicManager();
        }
        anim.SetTrigger("FadeIn");
    }
    public void OnFadeComplete()
    {
        PlayerPrefs.SetInt("isLoaded", 0);
        SceneManager.LoadScene(levelToLoad);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("isLoaded", 0);
    }
}
