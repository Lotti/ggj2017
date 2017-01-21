using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gnenemy : MonoBehaviour 
{

	public List<GameObject> Children;

	public float speed;
	// Use this for initialization
	void Start () 
	{
		float i = 0;
		foreach (var ch in this.Children) 
		{
			var tw=ch.transform.DOScale (Vector3.one*0.5f, 1).SetEase (Ease.OutBounce).SetDelay(i);

			tw.SetLoops (-1, LoopType.Yoyo);

			i += 0.1f;

		}

		VoiceLightShader2.Instance.OnAllucco += this.OnAllucco;

	}

	Vector3 target=Vector3.zero;

	void OnAllucco(Transform t)
	{
		target = t.position;
	}

	void Update()
	{
		if (target != Vector3.zero) 
		{
			var direction = (target - this.transform.position ).normalized;
			this.transform.position += direction * speed * Time.deltaTime;
			this.transform.forward = direction;
		}

	}

	void OnDestroy()
	{
		if (VoiceLightShader2.IsInstanced ()) 
		{
			VoiceLightShader2.Instance.OnAllucco -= this.OnAllucco;
		}
	}

}
