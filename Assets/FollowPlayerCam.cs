using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour {

	private Transform playerTr;
	// Use this for initialization
	void Start () {


		playerTr = Player.Instance.transform;
		this.GetComponent<CameraMove> ().target = this.playerTr;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		/*Vector3 vect=playerTr.position - playerTr.forward * 5f + Vector3.up * 3;
		this.transform.position = Vector3.Lerp (this.transform.position, vect, 0.1f);
		this.transform.LookAt (playerTr.position);
		*/


	}
}
