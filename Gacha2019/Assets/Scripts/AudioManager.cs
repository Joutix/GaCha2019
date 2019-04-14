using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtilities.DesignPatterns;

public class AudioManager : Singleton<AudioManager>
{
    public float s_playSFX = 1f;
    public float s_playMusic = 1f;
}
