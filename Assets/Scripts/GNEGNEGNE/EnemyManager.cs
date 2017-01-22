using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {

	public GameObject stampino;

	private float lastTimeUpdate=0;
	public List<Transform> enemies = new List<Transform> ();
	// Update is called once per frame

	void OnEnable() {
		if(Networking.IsInstanced())
			Networking.Instance.incomingData += applyIncomingData;
	}

	void OnDisable() {
		
		if(Networking.IsInstanced())
			Networking.Instance.incomingData -= applyIncomingData; 
	}

	void applyIncomingData(RoomPackage rp) {
		while (enemies.Count < rp.enemiesPosition.Count) 
		{
			Spawn ();
		}

		while (enemies.Count > rp.enemiesPosition.Count) 
		{
			var todestroy = enemies[enemies.Count - 1];
			enemies.RemoveAt(enemies.Count - 1);
			Destroy(todestroy.gameObject);
		}

		for (int i = 0; i < rp.enemiesPosition.Count; i++) {
			enemies[i].transform.position = rp.enemiesPosition [i];
		}
		for (int i = 0; i < rp.enemiesEuler.Count; i++) {
			enemies[i].transform.eulerAngles = rp.enemiesEuler[i];
		}
	}

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
