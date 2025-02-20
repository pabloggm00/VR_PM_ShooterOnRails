using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public Boss boss;
    public GameObject panelMuerte;
    public AudioClip music;

    [HideInInspector]
    public bool onPause;

    private bool canReset;

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
    }

    public void StartBoss()
    {
        boss.Init();
    }

    public void PanelMuerte()
    {
        panelMuerte.SetActive(true);
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
}
