using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField]
    Sound[] sounds;
    [SerializeField]
    AudioClip[] backgroundMusic;
    [SerializeField]
    AudioSource music;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clips[0];
            s.Source.loop = s.loop;
        }
    }

    public void Update()
    {
        PlayMusic();
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.Source.isPlaying == false)
        {
            s.Source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.Source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
            s.Source.Play();
        }
    }

    private void PlayMusic()
    {
        if (music.isPlaying == false)
        {
            music.clip = backgroundMusic[UnityEngine.Random.Range(0, backgroundMusic.Length)];
            music.Play();
        }
    }

}
