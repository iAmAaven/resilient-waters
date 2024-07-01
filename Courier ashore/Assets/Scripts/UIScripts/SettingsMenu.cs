using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider masterSlider;

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Start()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(masterVolume) * 20);
        audioMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);

        masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, masterVolume);
        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolume);
        sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, sfxVolume);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}