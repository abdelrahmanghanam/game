using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
        
    }

    public void Play(string clipName,bool checkPlaying)
    {
        if (pauseMenuScript.gameIsPaused == false)
        {
            Sound sound = Array.Find(sounds, s => s.name == clipName);
            if (sound == null)
            {
                Debug.LogWarning("no sound has this name");
                return;
            }
            if (checkPlaying)
            {
                if (!sound.audioSource.isPlaying)
                {
                    sound.audioSource.Play();
                }
                else
                {
                    return;
                }
            }
            else
            {
                sound.audioSource.Play();
            }
        }
    }
}
