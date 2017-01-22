using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using MiniJSon;

public class Networking : Singleton<Networking> {

	private MqttClient client;
	private string clientId;
	private string serverHost = "iot.eclipse.org";
	private int port = 1883;
	private int playerType = 0;
	private string topicPrefix = "gg17MEGA-";
	private string topic = default(string);
	private int msgCounter = 0;
	public int PlayerType { get { return playerType; } }
	public string Topic { get { return topic; } }

	public Transform heroTransform;
	public Transform[] enemiesTransform;

	abstract class MQTTPackage {
		abstract public string ToString();
	};

	class RoomPackage: MQTTPackage {
		public int counter = 0;
		public Vector3 heroPosition = default(Vector3);
		public Vector3 heroEuler = default(Vector3);
		public List<Vector3> enemiesPosition = default(List<Vector3>);
		public List<Vector3> enemiesEuler = default(List<Vector3>);

		public RoomPackage(int counter, Vector3 heroPosition, Vector3 heroEuler, 
			List<Vector3> enemiesPosition, List<Vector3> enemiesEuler) {
			this.counter = counter;
			this.heroPosition = heroPosition;
			this.heroEuler = heroEuler;
			this.enemiesPosition = enemiesPosition;
			this.enemiesEuler = enemiesEuler;
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
			});
		}

		public static RoomPackage fromJSON(string json) {
			Dictionary<string, object> d = (Dictionary<string, object>) MiniJSon.Json.Deserialize(json);
			if (d.ContainsKey("counter") &&
				d.ContainsKey("heroPosition") && d.ContainsKey("heroEuler") && 
				d.ContainsKey("enemiesPosition") && d.ContainsKey("enemiesEuler")) {

				int counter = Convert.ToInt32(d["counter"]);
				Vector3 heroPosition = new Vector3().FromList((List<object>) d["heroPosition"]);
				Vector3 heroEuler = new Vector3().FromList((List<object>) d["heroEuler"]);

				List<Vector3> enemiesPosition = new List<Vector3>();
				foreach (List<object> e in (List<List<object>>) d["enemiesPosition"]) {
					enemiesPosition.Add(new Vector3().FromList(e));
				}

				List<Vector3> enemiesEuler = new List<Vector3>();
				foreach (List<object> e in (List<List<object>>) d["enemiesEuler"]) {
					enemiesEuler.Add(new Vector3().FromList(e));
				}

				return new RoomPackage(counter, heroPosition, heroEuler, enemiesPosition, enemiesEuler);	
			} else {
				return null;
			}
		}
	}

	void Awake () {
		// create client instance
		client = new MqttClient(serverHost, port, false, null); 

		clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
	}

	private float time = 0f;
	private float timeHello = 0.2f;
	void Update() {
		if (time >= timeHello) {
			time = 0f;
			sendData();
		}
	}

	void OnEnable() {
		client.MqttMsgPublishReceived += client_MsgReceived; 
	}

	void OnDisable() {
		client.MqttMsgPublishReceived -= client_MsgReceived; 
	}

	void client_MsgReceived(object c, MqttMsgPublishEventArgs ev) { 
		string msg = System.Text.Encoding.UTF8.GetString (ev.Message);
		Debug.Log ("Received -> " + ev.Topic + ": "+ msg);

		try {
			RoomPackage rp = RoomPackage.fromJSON(msg);
			// Aggiorna i dati di gioco
			if (rp.counter > msgCounter) {
				msgCounter = rp.counter;

				heroTransform.position = rp.heroPosition;
				heroTransform.eulerAngles = rp.heroEuler;

				for(int i = 0; i < rp.enemiesPosition.Count; i++) {
					enemiesTransform[i].position = rp.enemiesPosition[i];
					enemiesTransform[i].eulerAngles = rp.enemiesEuler[i];
					// TODO eliminare i nemici in eccesso rispetto ai dati in arrivo
					// TODO aggiungere i nemici assenti rispetto ai dati in arrivo
				}

			}
		}
		catch (Exception e) {
			Debug.LogError(e);
		}
	} 

	void SendData() {
		// Invia i dati di gioco
		List<Vector3> enemiesPosition = new List<Vector3>();
		List<Vector3> enemiesEuler = new List<Vector3>();
		foreach(Transform t in enemiesTransform) {
			enemiesPosition.Add(t.position);
			enemiesEuler.Add(t.eulerAngles);
		}
		RoomPackage rp = new RoomPackage(++msgCounter, heroTransform.position, heroTransform.eulerAngles, enemiesPosition, enemiesEuler);
		sendMessage(this.topic, rp.ToString());
	}
		
	void sendMessage(string topic, string message) {
		client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
	}

	public string playAsPlayer1() {
		playerType = 1;
		msgCounter = 0;
		this.topic = topicPrefix+UnityEngine.Random.Range(1111,9999).ToString();
		client.Subscribe(new string[] { this.topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
		return topic;
	}

	public void playAsPlayer2(string topic) {
		playerType = 2;
		msgCounter = 0;
		this.topic = topic;
		client.Subscribe(new string[] { this.topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
	}
}