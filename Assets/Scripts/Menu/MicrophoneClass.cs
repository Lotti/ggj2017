using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneClass : MonoBehaviour
{
    new AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void GetClip()
    {
        audio.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);

    }

}
