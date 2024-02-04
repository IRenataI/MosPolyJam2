using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundsRefsSO", menuName = "Scriptable Objects/SoundsRefsSO", order = 1)]
public class SoundsRefsSO : ScriptableObject
{
    public Sound[] sounds;

    public Sound FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
