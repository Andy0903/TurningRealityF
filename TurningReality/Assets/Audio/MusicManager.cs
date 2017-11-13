using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SoundManager
{
    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (!Source.isPlaying)
        {
            PlayRandomSoundClip();
        }
    }
}
