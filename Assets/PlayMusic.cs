using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : Singleton<PlayMusic> {

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(_instance);
    }

	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
