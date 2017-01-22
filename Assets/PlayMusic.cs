using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : Singleton<PlayMusic> {

    public AudioSource music;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(_instance);
    }

    // Use this for initialization
    public void BASTAFERMALAMMUSICA()
    {
        music.Stop();
        Destroy(_instance);
    }


}
