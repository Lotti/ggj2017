using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneClass : MonoBehaviour {

	private AudioSource audioS;
	private bool _isScreaming = false;
	private string MicString = "Built-in Microphone";
	int freq = 44100;

	public static event Action OnScream;

	public float rmsRef = 1f;

	void Awake () {
		audioS = GetComponent<AudioSource> ();

	}

	void Start () {
		audioS.clip = Microphone.Start ( MicString, true, 10, freq );
		audioS.loop = true;
		_isScreaming = false;
		while ( !( Microphone.GetPosition ( MicString ) > 0 ) ) {
		}
		audioS.Play ();
	}

	void Update () {
		float[] spectrum = new float[1024];
		audioS.GetOutputData ( spectrum, 0 );
		float m = 0;
		foreach ( var s in spectrum ) {
			m += s * s;
		}
		var rms = Mathf.Sqrt ( ( float )m / ( float )spectrum.Length );
		var dB = 20 * Mathf.Log ( rms / rmsRef, 10f );
		if ( dB >= -5f && !_isScreaming ) {
			_isScreaming = true;
			Debug.LogWarning ( "STRILLOOOOO!!!" );
			if ( OnScream != null ) {
				OnScream ();
			}
			StartCoroutine ( _WaitForScream() );
		}
	}

	IEnumerator _WaitForScream () {
		float timeStart = Time.time;
		while ( ( Time.time - timeStart ) <= 2f ) {
			yield return null;
		}
		Debug.LogWarning ( "XXX => STRILLOOOOO!!!" );
		_isScreaming = false;
	}

}
