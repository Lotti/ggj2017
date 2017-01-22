using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLightShader2 : Singleton<VoiceLightShader2> {

	public List<MovingSoundWave> go;

	public GameObject heroParent;
	public GameObject heroIndicator;
	public GameObject viewerParent;
	public Transform playerT;


	public List<Material> materials;

	public List<Renderer> renderers;

	public Action<Transform> OnAllucco;

	void Start()
	{
		this.renderers = new List<Renderer> ();
		this.renderers.AddRange( this.GetComponents<Renderer> ());
		this.renderers.AddRange (this.GetComponentsInChildren<Renderer> ());

		this.materials = new List<Material> ();

		foreach (var r in this.renderers) 
		{
			if (!materials.Contains (r.sharedMaterial)) 
			{
				materials.Add (r.sharedMaterial);
			}
		}

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

	public void Update()
	{
		if (Networking.Instance.PlayerType == 1) 
		{
			#if UNITY_EDITOR
			if (Input.GetKeyUp (KeyCode.Space) || (Input.GetMouseButtonUp(0)) ) 
			{
				this.SpawnVoid (this.playerT.forward);
			}
			#endif
			CheckObjPositions ();
		}

	}

    public void SpawnVoice()
    {
        this.SpawnVoid(this.playerT.forward);
        Debug.Log("Spawn voice");
    }

	public void SpawnVoid( Vector3 direction ){
	
		if (this.go.Count < 15)
		{
			var newGo = new GameObject ();
			var msw=newGo.AddComponent<MovingSoundWave> ();
			newGo.transform.parent = this.transform;
			newGo.transform.position = this.playerT.transform.position;
			newGo.transform.forward = direction;
			msw.StartPosition=this.playerT.transform.position;
			this.go.Add (msw);

			if (OnAllucco != null) 
			{
				OnAllucco (this.playerT);
			}
		}
	}

	public bool isThisPositionOk(Vector3 raycastTarget)
	{
		return this.go.Count > 0;
	}

	public List<int> toDel=new List<int>();

	public void  CheckObjPositions( ) 
	{
		for ( int i=0; i < go.Count; i++) 
		{
			var waveGO = go [i];
			if (waveGO == null) 
			{
				toDel.Add (i);
			}
		}

		if (toDel.Count > 0) 
		{
			foreach (int i in toDel) 
			{
				go.RemoveAt (i);
			}
			toDel.Clear ();
		}

		foreach (var m in this.materials) {
			m.SetInt("_Points_Length" , go.Count);
		}
		
		if (go.Count > 0) {
			
			var vPos = new Vector4[20];
			var vStartPos = new Vector4[20];
			var properties = new Vector4[20];

			for ( int i=0; i < go.Count; i++) 
			{
				var waveGO = go[i];

				vPos[i] = waveGO.transform.position;
				vStartPos [i] = waveGO.StartPosition;
				properties[i] = new Vector2(waveGO.radius, waveGO.remainingLife );

			}

			foreach (var m in this.materials) 
			{
				m.SetVectorArray("_Points", vPos);
				m.SetVectorArray("_StartPoints", vStartPos);
				m.SetVectorArray("_Properties", properties);
			}

		}


	}
}
