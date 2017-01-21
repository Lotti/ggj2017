using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class mqttTest : MonoBehaviour {
	private MqttClient client;
	// Use this for initialization
	void Start () {
		// create client instance 
		client = new MqttClient("localhost",1883 , false , null ); 
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "hello/world/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		Debug.Log("Received -> " + e.Topic + ": "+ System.Text.Encoding.UTF8.GetString(e.Message)  );
	} 

	void OnGUI(){
		if ( GUI.Button (new Rect (20,40,500,500), "Level 1")) {
			Debug.Log("sending...");
			client.Publish("hello/world/valerio123213", System.Text.Encoding.UTF8.GetBytes(UnityEngine.SystemInfo.deviceModel), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
	}
	// Update is called once per frame
	void Update () {



	}
}
