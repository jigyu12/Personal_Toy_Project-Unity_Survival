using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;

    public void Start()
    {
        MasterVolumeChange(masterVolumeSlider.value);
        SfxVolumeChange(sfxVolumeSlider.value);
    }

    public void MasterVolumeChange(float volume)
    {
        volume = volume == 0 ? -80f : Mathf.Log10(volume) * 20;
        masterMixer.SetFloat("musicVol", volume);
    }    
    public void SfxVolumeChange(float volume)
    {
        volume = volume == 0 ? -80f : Mathf.Log10(volume) * 20;
        masterMixer.SetFloat("sfxVol", volume);
    }

    public void OnOff(bool isOn)
    {
        if (!isOn)
        {
            MasterVolumeChange(0f);
            SfxVolumeChange(0f);
        }
        else
        {
            MasterVolumeChange(masterVolumeSlider.value);
            SfxVolumeChange(sfxVolumeSlider.value);
        }
    }
}
