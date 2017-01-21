using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoundWave : MonoBehaviour {

	public float life = 2;
	private float startTime=0;

	public float speed=3;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + life) {
			Destroy (this.gameObject);
		} else {
			this.transform.position = this.transform.position + this.transform.forward * this.speed * Time.deltaTime;
		}
	}
}
