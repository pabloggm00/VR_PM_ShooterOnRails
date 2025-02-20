using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public Boss boss;
    public GameObject panelMuerte;
    public GameObject panelWin;
    public AudioClip music;

    [HideInInspector]
    public bool onPause;

    [HideInInspector]
    public bool onWin;

    private bool canReset;
    private bool canExit;

    private void Awake()
    {
        instance = this;
    }



    private void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void StartBoss()
    {
        boss.Init();
    }

    public void PanelMuerte()
    {
        panelMuerte.SetActive(true);
        AudioManager.instance.PauseAllSFX();
        canReset = true;
    }

    public void OnPause()
    {
        AudioManager.instance.PauseAllSFX();
        onPause = true;
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        AudioManager.instance.ResumeAllSFX();
        onPause = false;
        Time.timeScale = 1f;
    }

    public void Win()
    {
        AudioManager.instance.PauseAllSFX();
        panelWin.SetActive(true);
        canExit = true;
        onWin = true;
    }
}
