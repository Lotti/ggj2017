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

	RoomPackage _roomPackageToCheck=null;

	void applyIncomingData( RoomPackage rp ) {
		this._roomPackageToCheck = rp;

	}

	void Update () 
	{
		if (Networking.Instance.PlayerType == 1) {
			if (lastTimeUpdate == 0 || lastTimeUpdate >= 10f) {
				lastTimeUpdate = 0;

				Spawn ();
			}
			lastTimeUpdate += Time.deltaTime;
		} 
		else if(_roomPackageToCheck!=null)
		{
			while (enemies.Count < _roomPackageToCheck.enemiesPosition.Count) 
			{
				Spawn ();
			}

			while (enemies.Count > _roomPackageToCheck.enemiesPosition.Count) 
			{
				var todestroy = enemies[enemies.Count - 1];
				enemies.RemoveAt(enemies.Count - 1);
				Destroy(todestroy.gameObject);
			}

			for (int i = 0; i < _roomPackageToCheck.enemiesPosition.Count; i++) {
				enemies[i].transform.position = Vector3.Lerp(enemies[i].transform.position, _roomPackageToCheck.enemiesPosition [i], 0.01f);
			}
			for (int i = 0; i < _roomPackageToCheck.enemiesEuler.Count; i++) {
				enemies[i].transform.eulerAngles = Vector3.Lerp(enemies[i].transform.eulerAngles, _roomPackageToCheck.enemiesEuler[i], 0.01f);
			}

			_roomPackageToCheck = null;
		}
	}

	void Spawn()
	{
		var gngng = GameObject.Instantiate (this.stampino);
		gngng.transform.parent = this.transform;

		gngng.transform.position = Player.Instance.transform.position +
			Player.Instance.transform.right * Random.Range (-5f, 5f) +
			Player.Instance.transform.forward * Random.Range (-5f, 5f);
		
		enemies.Add (gngng.transform);
	}
}
