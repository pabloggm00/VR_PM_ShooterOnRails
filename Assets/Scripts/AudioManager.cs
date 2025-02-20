using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Altavoces")]
    public GameObject altavozMusica;
    public GameObject altavozSFX;

    [Header("Mixer")]
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroupMusic;
    public AudioMixerGroup audioMixerGroupSFX;

    [Header("Volumes")]
    public float masterVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float sFXVolume = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //Master
            if (!PlayerPrefs.HasKey("MasterVolume"))
            {
                PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            }
            else
            {
                masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            }

            //Music
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            }
            else
            {
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            }

            //SFX
            if (!PlayerPrefs.HasKey("SFXVolume"))
            {
                PlayerPrefs.SetFloat("SFXVolume", sFXVolume);
            }
            else
            {
                sFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            }

            audioMixer.SetFloat("MasterVolume", masterVolume);
            audioMixer.SetFloat("MusicVolume", musicVolume);
            audioMixer.SetFloat("SFXVolume", sFXVolume);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMusic(AudioClip clip)
    {
        AudioSource musicSource;

        if (altavozMusica.GetComponent<AudioSource>() == null)
        {
            musicSource = altavozMusica.AddComponent<AudioSource>();
        }
        else
        {
            musicSource = GetComponent<AudioSource>();
        }

        musicSource.clip = clip;
        musicSource.outputAudioMixerGroup = audioMixerGroupMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, bool loop)
    {
        CleanUpSFX();

        AudioSource sfxSource;

        sfxSource = altavozSFX.AddComponent<AudioSource>();
        sfxSource.clip = clip;

        if (loop == true)
        {
            sfxSource.loop = loop;
            sfxSource.Play();
        }
        else
        {
            sfxSource.PlayOneShot(clip);
        }


        sfxSource.outputAudioMixerGroup = audioMixerGroupSFX;
      
    }

    public void PauseAllSFX()
    {
        CleanUpSFX();

        AudioSource[] sources = altavozSFX.GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            source.Pause();
        }
    }

    public void ResumeAllSFX()
    {
        AudioSource[] sources = altavozSFX.GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            source.UnPause();
        }
    }

    public void CleanAllSFX()
    {
        AudioSource[] sources = altavozSFX.GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            Destroy(source);
            
        }
    }

    public void CleanUpSFX()
    {
        AudioSource[] sources = altavozSFX.GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            if (!source.isPlaying && !source.loop)
            {
                Destroy(source);
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 30);
        masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 30);
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 30);
        sFXVolume = volume;
    }

}
