using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoundWave : MonoBehaviour {


	public Vector3 StartPosition;
	public float life = 2;
	private float startTime=0;

	public float speed=10;
	// Use this for initialization
	void Start () {
		startTime = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		startTime += Time.deltaTime;
		if (startTime > life) 
		{
			Destroy (this.gameObject);
		} 
		else 
		{
			this.transform.position = this.transform.position + this.transform.forward * this.speed * Time.deltaTime;
		}
	}

	public float remainingLife
	{
		get{ 
			return (1 - startTime / life);
		}
	}
}
