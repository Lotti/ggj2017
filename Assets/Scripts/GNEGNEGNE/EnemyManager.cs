using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {

	public GameObject stampino;

	private float lastTimeUpdate=0;

	void Start()
	{
		//Check multiminchia
	}

	// Update is called once per frame
	void Update () 
	{
		if (lastTimeUpdate == 0 || lastTimeUpdate >= 10f) 
		{
			lastTimeUpdate = 0;
			var gngng= GameObject.Instantiate (this.stampino);
			gngng.transform.parent = this.transform;
			gngng.transform.position = Player.Instance.transform.position + 
				Player.Instance.transform.right*Random.Range(-5f,5f) + 
				Player.Instance.transform.forward * 5;
			
		}
		lastTimeUpdate += Time.deltaTime;
	}
}
