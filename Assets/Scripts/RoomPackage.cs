using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoomPackage {
	public int counter = 0;
	public Vector3 heroPosition = default(Vector3);
	public Vector3 heroEuler = default(Vector3);
	public List<Vector3> enemiesPosition = default(List<Vector3>);
	public List<Vector3> enemiesEuler = default(List<Vector3>);
	public int gameOver = 0;

	public RoomPackage(int counter, Vector3 heroPosition, Vector3 heroEuler, 
		List<Vector3> enemiesPosition, List<Vector3> enemiesEuler, int gameOver) {
		this.counter = counter;
		this.heroPosition = heroPosition;
		this.heroEuler = heroEuler;
		this.enemiesPosition = enemiesPosition;
		this.enemiesEuler = enemiesEuler;
		this.gameOver = gameOver;
	}

	public string ToJSON() {
		return this.ToString();
	}

	public override string ToString() {
		List<List<object>> enemiesPositionList = new List<List<object>>();
		foreach (Vector3 e in this.enemiesPosition) {
			enemiesPositionList.Add(e.ToList());
		}
		List<List<object>> enemiesEulerList = new List<List<object>>();
		foreach (Vector3 e in this.enemiesEuler) {
			enemiesEulerList.Add(e.ToList());
		}

		return MiniJSon.Json.Serialize(new Dictionary<string, object>() {
			{"counter", this.counter},
			{"heroPosition", this.heroPosition.ToList()},
			{"heroEuler", this.heroEuler.ToList()},
			{"enemiesPosition", enemiesPositionList},
			{"enemiesEuler", enemiesEulerList},
			{"gameOver", gameOver},
		});
	}

	public static RoomPackage fromJSON(string json) {
		Dictionary<string, object> d = (Dictionary<string, object>) MiniJSon.Json.Deserialize(json);
		if (d.ContainsKey("counter") &&
			d.ContainsKey("heroPosition") && d.ContainsKey("heroEuler") && 
			d.ContainsKey("enemiesPosition") && d.ContainsKey("enemiesEuler") && d.ContainsKey("gameOver")) {

			int counter = Convert.ToInt32(d["counter"]);
			int gameOver = Convert.ToInt32(d["gameOver"]);
			Vector3 heroPosition = new Vector3().FromList((List<object>) d["heroPosition"]);
			Vector3 heroEuler = new Vector3().FromList((List<object>) d["heroEuler"]);

			List<Vector3> enemiesPosition = new List<Vector3>();
			foreach (List<object> e in (List<object>) d["enemiesPosition"]) {
				enemiesPosition.Add(new Vector3().FromList(e));
			}

			List<Vector3> enemiesEuler = new List<Vector3>();
			foreach (List<object> e in (List<object>) d["enemiesEuler"]) {
				enemiesEuler.Add(new Vector3().FromList(e));
			}

			return new RoomPackage(counter, heroPosition, heroEuler, enemiesPosition, enemiesEuler, gameOver);	
		} else {
			return null;
		}
	}
}