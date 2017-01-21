using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

public class mqtt : MonoBehaviour {

	private MqttClient client;
	private string clientId;
	private string serverHost = "192.168.138.151";
	private int port = 1883;

	class MQTTPackage {
		
	}

	class WarmupPackage: MQTTPackage {
		public string clientId;
		public int playerType;

		public WarmupPackage (string clientId, int playerType) {
			this.clientId = clientId;
			this.playerType = playerType;
		}

		public string ToString() {
			return JsonUtility.ToJson (this);
		}
	}

	class RoomPackage: MQTTPackage {
		public string playerOne;
		public string playerTwo;
		public string topic;

		public RoomPackage (string playerOne, string playerTwo, string topic) {
			this.playerOne = playerOne;
			this.playerTwo = playerTwo;
			this.topic = topic;
		}

		public string ToString() {
			return JsonUtility.ToJson (this);
		}
	}

	void Awake () {
		// create client instance 
		client = new MqttClient(serverHost, port, false, null); 

		clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 	
	}

	void OnEnable() {
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgReceived; 
	}

	void OnDisable() {
		// register to message received 
		client.MqttMsgPublishReceived -= client_MqttMsgReceived; 
	}

	// Use this for initialization
	void Start () {
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "warmup" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }); 
	}

	void client_MqttMsgReceived(object client, MqttMsgPublishEventArgs ev) { 
		string msg = System.Text.Encoding.UTF8.GetString (ev.Message);
		MQTTPackage p;
		try {
			p = JsonUtility.FromJson<RoomPackage> (msg);	
		} catch (Exception e1) {
			Debug.LogError (e1);
			try {
				p = JsonUtility.FromJson<WarmupPackage> (msg);	
			}	
			catch (Exception e2) {
				Debug.LogError (e2);	
				p = null;
			}
		}
		if (p != null) {
			if (p is WarmupPackage) {
				Debug.Log ("Received -> " + ev.Topic + ": "+ System.Text.Encoding.UTF8.GetString(ev.Message));
			} else if (p is RoomPackage) {
				Debug.Log ("Received -> " + ev.Topic + ": "+ System.Text.Encoding.UTF8.GetString(ev.Message));
			}
		}
	} 

	void sendMessage(string topic, string message) {
		client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
	}

	public string room = "asd";
	void OnGUI() {
		if (GUI.Button (new Rect (20,40,250,250), "WARMUP P1")) {
			Debug.Log("sending...");
			sendMessage ("warmup", new WarmupPackage(clientId, 1).ToString());
			Debug.Log("sent");
		}
		if (GUI.Button (new Rect (270,40,250,250), "WARMUP P2")) {
			Debug.Log("sending...");
			sendMessage ("warmup", new WarmupPackage(clientId, 2).ToString());
			Debug.Log("sent");
		}

		if (GUI.Button (new Rect (20,290,500,250), "READY")) {
			Debug.Log("sending...");
			sendMessage (room, "asd");
			Debug.Log("sent");
		}
	}
}