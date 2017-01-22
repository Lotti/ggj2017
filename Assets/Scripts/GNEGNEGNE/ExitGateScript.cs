using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitGateScript : MonoBehaviour {

	private Transform child = default(Transform);

	void Awake() {
		if (transform.childCount == 1) {
			child = transform.GetChild (0);
		}
	}

	// Use this for initialization
	void Start() {
		if (child != default(Transform)) {
			Vector3 startScale = new Vector3(0.5f, 0.025f, 5f);
			Vector3 endScale = new Vector3(-0.5f, 0.025f, -5f);
			float duration = 5f;

			child.localScale = startScale;
			var tween = child.DOScale(endScale, duration);
			tween.SetEase(Ease.InQuint);
			tween.SetLoops(-1, LoopType.Restart);
		}
	}

	void OnTriggerEnter(Collider g) {
		Player p = g.gameObject.GetComponent<Player>();
		if (p != null) {
			p.gameOver(true);
		}
	}
}