using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.Lerp (this.transform.position, Player.Instance.transform.position, 0.005f);
	}
}
