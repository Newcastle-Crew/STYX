#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
#endregion

public class SettingsMenu : MonoBehaviour
{
    #region Mixers 'n' Sliders
    public AudioMixer musicMixer;   // The mixer that controls music volume.
    public AudioMixer SFXMixer;     // The mixer that controls sound effect volume.

    public Slider musicSlider; // The slider that controls music volume.
    public Slider SFXSlider; // The slider that controls sound effect volume.
    #endregion

    private bool musicMuted = false;
    private bool sfxMuted = false;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f); // Gets the float value of musicVolume, or uses .5f if it isn't found.
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f); // Ditto, SFX Volume

        if (musicSlider.value > 0.01f) musicMuted = false;
        if (SFXSlider.value > 0.01f) sfxMuted = false;
    }

    public void SetMusicLevel(float mSliderValue)
    {
        musicMixer.SetFloat("musicVolume", Mathf.Log10(mSliderValue) * 20);

        PlayerPrefs.SetFloat("musicVolume", mSliderValue);
    }

    public void SetSFXLevel(float xSliderValue)
    {
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(xSliderValue) * 20);

        PlayerPrefs.SetFloat("SFXVolume", xSliderValue);
    }

    public void MuteMusic()
    {
        if (musicMuted == false)
        {
            PlayerPrefs.SetFloat("musicVolume", 0.01f);
            musicSlider.value = 0.01f;
            musicMuted = true;
        }
        else
        {
            musicMuted = false;
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            musicSlider.value = 0.5f;
        }
    }

    public void MuteSFX()
    {
        if (sfxMuted == false)
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.01f);
            SFXSlider.value = 0.01f;
            sfxMuted = true;
        }
        else
        {
            sfxMuted = false;
            PlayerPrefs.SetFloat("SFXVolume", 0.5f);
            SFXSlider.value = 0.5f;
        }
    }
}