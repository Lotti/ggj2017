using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public float roomSizeFactor = 50f; 
	private float wallHeight = 15f; 
	public Camera TopCamera;
	public GameObject Floor;
	public GameObject East;
	public GameObject West;
	public GameObject North;
	public GameObject South;

	void Awake () {
		TopCamera.transform.localPosition = new Vector3 (0f, 0f, -4f);

		Floor.transform.localPosition = new Vector3 (0f, 0f, 0f);
		East.transform.localPosition = new Vector3 (-roomSizeFactor/2f, 0f, -2f);
		West.transform.localPosition = new Vector3 (roomSizeFactor/2f, 0f, -2f);
		North.transform.localPosition = new Vector3 (0f, -roomSizeFactor/2f, -2f);
		South.transform.localPosition = new Vector3 (0f, +roomSizeFactor/2f, -2f);

		Floor.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
		East.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
		West.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
		North.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
		South.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);

		Floor.transform.localScale = new Vector3 (roomSizeFactor, 1f, roomSizeFactor);
		East.transform.localScale = new Vector3 (1f, wallHeight, roomSizeFactor + 1f);
		West.transform.localScale = new Vector3 (1f, wallHeight, roomSizeFactor + 1f);
		North.transform.localScale = new Vector3 (roomSizeFactor - 1f, wallHeight, 1f);
		South.transform.localScale = new Vector3 (roomSizeFactor - 1f, wallHeight, 1f);

	}
}
