using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;

    private bool onPause;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;

    private void Start()
    {
       SetStartVolumes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (onPause)
            {
                Resume();
            }
            else
            {
                Pausa();
            }
          
        }
    }

    public void Pausa()
    {
        onPause = true;
        menuPausa.SetActive(true);
        GameplayController.instance.OnPause();
    }

    public void Resume()
    {
        onPause = false;
        menuPausa.SetActive(false);
        GameplayController.instance.OnResume();
    }

    public void Salir()
    {
        GameplayController.instance.OnResume();
        AudioManager.instance.CleanAllSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void SetStartVolumes()
    {
        masterSlider.value = AudioManager.instance.masterVolume;
        musicSlider.value = AudioManager.instance.musicVolume;
        sFXSlider.value = AudioManager.instance.sFXVolume;
    }

    public void OnChangeMasterVolume()
    {
        AudioManager.instance.SetMasterVolume(masterSlider.value);
    }

    public void OnChangeMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void OnChangeSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sFXSlider.value);
    }
}
