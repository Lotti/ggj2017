using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

	public GameObject heroParent;
	public GameObject heroIndicator;
	public GameObject viewerParent;
	public Transform playerT;

	// Use this for initialization
	void Start () 
	{
		if (Networking.IsInstanced ()) {
			if (Networking.Instance.PlayerType == 1) 
			{
				Networking.Instance.gameStart();

				heroParent.gameObject.SetActive (true);
				viewerParent.SetActive (false);
			} 
			else 
			{
				heroIndicator.SetActive (true);	
				heroParent.gameObject.SetActive (false);
				playerT.gameObject.SetActive (false);

			}
		}
	}

}
