using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public GameObject prefLightPulse;

	public float time = 0f;
	public float lightCoolDown = 3f;

	void Start () {
		time = lightCoolDown;
	}

	void Update () {
		if (time < lightCoolDown) {
			time += Time.deltaTime;
			if (time > lightCoolDown) {
				time = lightCoolDown;
			}
		}
	}

	void OnLight (Vector3 point) {
		if (time == lightCoolDown) {
			time = 0f;
			GameObject light = Instantiate (prefLightPulse);
			light.transform.parent = this.transform;
			light.transform.position = new Vector3 (point.x, light.transform.localPosition.y, point.z); 
		}
	}
}
