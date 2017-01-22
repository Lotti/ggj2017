using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class IntelliScream : Singleton<IntelliScream> {

	private AudioSource audioS;
	private bool _isScreaming = false;
	private string MicString = "Built-in Microphone";
	int freq = 44100;
    public Image fillBaR;

	public Action OnScream;

	public float rmsRef = 1f;
	public AudioMixer mixer;

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
            fillBaR.fillAmount = 0;

            if ( OnScream != null ) {
				OnScream ();
			}
			StartCoroutine ( _WaitForScream() );
		}
	}

	IEnumerator _WaitForScream () {
		float timeStart = Time.time;
        fillBaR.fillAmount = 0;
        while ( ( Time.time - timeStart ) <= 5f )
        {
            fillBaR.fillAmount = (Time.time - timeStart)/5f;
            yield return null;
		}
		Debug.LogWarning ( "XXX => STRILLOOOOO!!!" );
		_isScreaming = false;
	}

}
