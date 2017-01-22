﻿using UnityEngine;
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

	public Action<RoomPackage> incomingData;

	void Awake () {
		// create client instance
		client = new MqttClient(serverHost, port, false, null); 

		clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		DontDestroyOnLoad (this.gameObject);
	}

	private float time = 0f;
	private float timeHello = 0.2f;
	private bool isGameRunning = false;
	public bool IsGameRunning { get { return this.isGameRunning; } set { this.time = 0f; this.isGameRunning = value; } }
	void Update() {
		if (isGameRunning) {
			time += Time.deltaTime;
			if (time >= timeHello) {
				time = 0f;
				this.sendData();
			}
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

				incomingData(rp);
			}
		}
		catch (Exception e) {
			Debug.LogError(e);
		}
	} 

	void sendData() {
		// Invia i dati di gioco
		List<Vector3> enemiesPosition = new List<Vector3>();
		List<Vector3> enemiesEuler = new List<Vector3>();
		foreach(Transform t in EnemyManager.Instance.enemies) {
			enemiesPosition.Add(t.position);
			enemiesEuler.Add(t.eulerAngles);
		}
		Transform p = Player.Instance.transform;
		RoomPackage rp = new RoomPackage(++msgCounter, p.position, p.eulerAngles, enemiesPosition, enemiesEuler);
		sendMessage(this.topic, rp.ToString());
	}
		
	void sendMessage(string topic, string message) {
		client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
	}

	public string playAsPlayer1() {
		playerType = 1;
		msgCounter = 0;
		string code = UnityEngine.Random.Range(1111,9999).ToString();
		this.topic = topicPrefix+code;
		client.Subscribe(new string[] { this.topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
		return code;
	}

	public void playAsPlayer2(string topic) {
		playerType = 2;
		msgCounter = 0;
		this.topic = topic;
		client.Subscribe(new string[] { this.topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

		UnityEngine.SceneManagement.SceneManager.LoadScene ("testShader2");

	}
}