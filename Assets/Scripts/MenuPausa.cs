using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;

    public GameObject pointerHUD;

    private bool onPause;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;

    [Header("SFX")]
    public AudioClip button;

    private void Start()
    {
       SetStartVolumes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameplayController.instance.onWin)
        {

            if (onPause)
            {
                Resume(true);
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
        pointerHUD.SetActive(false);
        Cursor.visible = true;
    }

    public void Resume(bool escape)
    {

        if (!escape)
        {
            AudioManager.instance.PlaySFX(button, false);
        }

        onPause = false;
        menuPausa.SetActive(false);
        GameplayController.instance.OnResume();
        pointerHUD.SetActive(true);
        Cursor.visible = false;
    }

    public void Salir()
    {
        AudioManager.instance.PlaySFX(button, false);
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
