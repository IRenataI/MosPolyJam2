using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundsRefsSO soundsRefsSO;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in soundsRefsSO.sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    private Sound FindSound(string name)
    {
        Sound sound = Array.Find(soundsRefsSO.sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return null;
        }
        return sound;
    }

    public void PlaySound(string name)
    {
        FindSound(name).source.Play();
    }

    public void StopSound(string name)
    {
        FindSound(name).source.Stop();
    }

    public void StopAllSounds()
    {
        foreach(Sound sound in soundsRefsSO.sounds)
        {
            sound.source.Stop();
        }
    }

    public void ChangeVolume(AudioMixerGroup mixerGroup, float value)
    {
        audioMixer.SetFloat(mixerGroup.name, Mathf.Log10(value) * 20);
    }

    public float ClipLength(string name)
    {
        return FindSound(name).clip.length;
    }
}
