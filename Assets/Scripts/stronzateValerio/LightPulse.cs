using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour {

	static float intensityFactor = 5f;
	static float angleFactor = 100f;
	static float ttl = 3f;
	float time = 0f;
	Light l;

	void Awake () {
		l = this.GetComponent<Light> ();
	}
		
	// Use this for initialization
	void Start () {
		l.range = 10f;
		l.intensity = 3f;
		l.spotAngle = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		l.intensity -= intensityFactor * Time.deltaTime;
		l.spotAngle += angleFactor * Time.deltaTime;

		time += Time.deltaTime;
		if (time >= ttl) {
			Destroy (this.gameObject);
		}
	}
}
