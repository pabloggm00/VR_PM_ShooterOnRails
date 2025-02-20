using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioClip music;


    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sFXSlider;


    private void Start()
    {
        if (AudioManager.instance.altavozMusica.GetComponent<AudioSource>() == null)
        {
            AudioManager.instance.PlayMusic(music);
        }


        SetStartVolumes();
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Application.Quit();
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
