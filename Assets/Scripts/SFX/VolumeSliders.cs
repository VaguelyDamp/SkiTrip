using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private VolumeController volumeController;

    void Start()
    {
        volumeController = GameObject.Find("UI")
            .GetComponent<VolumeController>();
        
        
        if (PlayerPrefs.GetFloat("MusicVolumeFirstSet") == 0)
        {
            InitMusicSlider(.7f);
            SetMusicVolume(.7f);
            PlayerPrefs.SetFloat("MusicVolumeFirstSet", 1);
        }
        else
        {
            InitMusicSlider(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.GetFloat("SFXVolumeFirstSet") == 0)
        {
            InitSFXSlider(.7f);
            SetSFXVolume(.7f);
            PlayerPrefs.SetFloat("SFXVolumeFirstSet", 1);
        }
        else
        {
            InitSFXSlider(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    void InitMusicSlider (float musicVolume)
    {
        musicSlider.value = musicVolume;
    }

    void InitSFXSlider (float sfxVolume)
    {
        sfxSlider.value = sfxVolume;
    }

    public void SetMusicVolume (float volume)
    {
        volumeController.SetMusicVolume(volume);
    }

    public void SetSFXVolume (float volume)
    {
        volumeController.SetSFXVolume(volume);
    }
}
