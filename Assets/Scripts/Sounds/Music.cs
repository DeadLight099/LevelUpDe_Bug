using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Music
{
    public int number;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(.1f, 3f)]
    public float pitch = 1;

    [HideInInspector]
    public bool playOnAwake = true;
    [HideInInspector]
    public bool loop = true;
    [HideInInspector]
    public AudioSource source;
}
