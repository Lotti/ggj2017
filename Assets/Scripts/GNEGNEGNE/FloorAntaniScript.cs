using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloorAntaniScript : MonoBehaviour {

	public float MaxTime = 10f;
	private GameObject exitGate = null;

	// Use this for initialization
	void Start() {
		if (this.transform.childCount == 1 && this.GetComponentInChildren<ExitGateScript>()) {
			exitGate = this.transform.GetChild(0).gameObject;
		}

		if (exitGate == null) {
			float boolean = Random.Range (0, 4f);

			if (boolean > 3) {
				float delay = Random.Range (0, MaxTime);
				float duration = Random.Range (2, 3f);
				float amount = Random.Range (0.5f, 1f);

				var tween = this.transform.DOMove (this.transform.position + this.transform.up * amount, duration).SetDelay (delay);
				tween.SetLoops (-1, LoopType.Yoyo);

			} else {
				Destroy (this);
			}
		}
	}
}