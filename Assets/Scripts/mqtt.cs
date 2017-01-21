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

	abstract class MQTTPackage {
		abstract public string ToString();
	};

	class RoomPackage: MQTTPackage {
		public int counter = 0;

		public RoomPackage(int counter) {
			this.counter = counter;
		}

		public override string ToString() {
			return JsonUtility.ToJson(this);
		}

		public static RoomPackage fromDictionary(Dictionary<string, object> d) {
			return new RoomPackage();
		}
	}

	void Awake () {
		// create client instance
		client = new MqttClient(serverHost, port, false, null); 

		clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
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
			Dictionary<string, object> d = (Dictionary<string, object>) MiniJSon.Json.Deserialize (msg);
			RoomPackage rp = RoomPackage.fromDictionary(d);
			// TODO aggiornare i dati di gioco
		}
		catch (Exception e) {
			Debug.LogError(e);
		}
	} 

	// Use this for initialization
	void Start () {

	}
		
	private float time = 0f;
	private float timeHello = 0.2f;
	void Update() {
		if (time >= timeHello) {
			time = 0f;
			// TODO Invia i dati di gioco
			sendMessage(this.topic, new RoomPackage().ToString());
		}
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

	void OnGUI() {
		if (GUI.Button (new Rect (20,40,250,250), "WARMUP P1")) {
			string topic = this.playAsPlayer1();
			Debug.Log("topic", topic);
		}
		if (GUI.Button (new Rect (270,40,250,250), "WARMUP P2")) {
			this.playAsPlayer2();
		}
	}
}