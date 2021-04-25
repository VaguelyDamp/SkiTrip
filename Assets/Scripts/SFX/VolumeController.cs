using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.GetFloat("MusicVolumeFirstSet") == 0)
        {
            SetMusicVolume(.7f);
            PlayerPrefs.SetFloat("MusicVolumeFirstSet", 1);
        }
        else
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.GetFloat("SFXVolumeFirstSet") == 0)
        {
            SetSFXVolume(.7f);
            PlayerPrefs.SetFloat("SFXVolumeFirstSet", 1);
        }
        else
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    public void SetMusicVolume (float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        FMODUnity.RuntimeManager
            .StudioSystem.setParameterByName("MusicVolume", volume);
    }

    public void SetSFXVolume (float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        FMODUnity.RuntimeManager
            .StudioSystem.setParameterByName("SFXVolume", volume);
    }
}
