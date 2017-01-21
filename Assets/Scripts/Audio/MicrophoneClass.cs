using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneClass : MonoBehaviour
{
	private F1_MicStreamer streamer;

    void Awake()
    {
		streamer = GetComponent<F1_MicStreamer> ();
		streamer.floatsInDelegate = _cb_Streamer;
    }

	void _cb_Streamer( float[] samples ){
		foreach ( var s in samples ) {
			Debug.Log ( s );
		}
	}

    void GetClip()
    {
        

    }

}
