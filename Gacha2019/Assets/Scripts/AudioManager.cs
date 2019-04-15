using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtilities.DesignPatterns;

public class AudioManager : Singleton<AudioManager>
{
    // Between 0 and 100
    public int s_SFXVolume = 100;
    public int s_MusicVolume = 100;

    // Sends a notification to Wwize to change the SFX volume
    public void UpdateSFXVolume(int volume)
    {
        s_SFXVolume = volume;
        AkSoundEngine.SetRTPCValue("SFX_Volume", volume);
        Debug.Log("Changed SFX volume to " + volume);
    }

    // Sends a notification to Wwize to change the Music volume
    public void UpdateMusicVolume(int volume)
    {
        s_MusicVolume = volume;
        AkSoundEngine.SetRTPCValue("Music_Volume", volume);
        Debug.Log("Changed Music volume to " + volume);
    }
}
