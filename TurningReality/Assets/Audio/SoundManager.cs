using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips;

    protected AudioSource Source { get; private set; }
    protected AudioClip RandomClip { get { return audioClips[Random.Range(0, audioClips.Count - 1)]; } }

    public virtual void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    public void PlayRandomSoundClip()
    {
        Source.clip = RandomClip;
        Source.Play();
    }

    public void PlayRandomOneShotEffect()
    {
        Source.PlayOneShot(RandomClip);
    }
}
