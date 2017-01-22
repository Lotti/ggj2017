using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {

	public GameObject stampino;

	private float lastTimeUpdate=0;
	private List<Transform> enemies = new List<Transform> ();
	// Update is called once per frame
	void Update () 
	{
		if (Networking.Instance.PlayerType == 1) 
		{
			if (lastTimeUpdate == 0 || lastTimeUpdate >= 10f) {
				lastTimeUpdate = 0;

				Spawn ();
			}
			lastTimeUpdate += Time.deltaTime;
		} 
		else if(Networking.IsInstanced())
		{
			while (enemies.Count < Networking.Instance.enemiesTransform.Count) 
			{
				Spawn ();
			}

			while (enemies.Count > Networking.Instance.enemiesTransform.Count) 
			{
				var todestroy = enemies [enemies.Count - 1];
				enemies.RemoveAt (enemies.Count - 1);
				Destroy (todestroy.gameObject);
			}

			for (int i = 0; i < Networking.Instance.enemiesTransform.Count; i++) 
			{
				enemies [i].transform.position = Networking.Instance.enemiesTransform [i].position;
				enemies [i].transform.rotation = Networking.Instance.enemiesTransform [i].rotation;
			}

		}

	}

	void Spawn()
	{
		var gngng = GameObject.Instantiate (this.stampino);
		gngng.transform.parent = this.transform;
		gngng.transform.position = Player.Instance.transform.position +
			Player.Instance.transform.right * Random.Range (-5f, 5f) +
			Player.Instance.transform.forward * 5;
		enemies.Add (gngng.transform);
	}
}
