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

	void Awake () {
		// create client instance 
		client = new MqttClient("localhost", 1883, false, null); 

		string clientId = Guid.NewGuid().ToString(); 
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

	void client_MqttMsgReceived(object sender, MqttMsgPublishEventArgs e) { 
		Debug.Log("Received -> " + e.Topic + ": "+ System.Text.Encoding.UTF8.GetString(e.Message));
	} 

	void sendMessage(string topic, string message) {
		client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
	}

	public string room = "asd";
	void OnGUI() {
		if (GUI.Button (new Rect (20,40,500,250), "WARMUP")) {
			Debug.Log("sending...");
			sendMessage ("warmup", "ready");
			Debug.Log("sent");
		}

		if (GUI.Button (new Rect (20,290,500,250), "READY")) {
			Debug.Log("sending...");
			sendMessage (room, "asd");
			Debug.Log("sent");
		}
	}
}