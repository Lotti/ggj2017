using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoundWave : MonoBehaviour {


	public Vector3 StartPosition;
	public float life = 5;
	private float startTime=0;

	public float speed=9;

	public float radius=3f;
	// Use this for initialization
	void Start () 
	{
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

	public bool IsThisPointOk(Vector3 point)
	{
		if (this.transform == null)
			return false;
		
		var dir = this.transform.forward;
		var max = (this.transform.position - this.StartPosition).sqrMagnitude;
		var min = max - this.radius * this.radius;
		var p = (point - this.StartPosition).sqrMagnitude;
		Debug.LogError( max+" "+min +" "+p);
		Debug.DrawLine (this.StartPosition, this.transform.position, Color.red);
		Debug.DrawLine (this.StartPosition, point, Color.green);
		Debug.DrawLine (this.StartPosition, point - dir*this.radius, Color.yellow);
		if ( p>min && p < max) 
		{
			return true;
		}
		return false;
	}

	public float remainingLife
	{
		get{ 
			return (1 - startTime / life);
		}
	}
}
