using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public float volume;
    public AudioMixerGroup mixerGroup;
    public AudioClip clip;


    [HideInInspector]
    public AudioSource source;
}
