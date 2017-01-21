using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLightShader2 : MonoBehaviour {

	public List<GameObject> go;

	public Transform playerT;

	public List<Renderer> renderers;
	void Start()
	{
		this.renderers = new List<Renderer> ();
		this.renderers.AddRange( this.GetComponents<Renderer> ());
		this.renderers.AddRange (this.GetComponentsInChildren<Renderer> ());

	}

	public void  Update()
	{
		if (Input.GetKeyUp (KeyCode.Space) || (Input.GetMouseButtonUp(0)) ) 
		{
			if (this.go.Count < 4) 
			{
				var newGo = new GameObject ( );
				var msw=newGo.AddComponent<MovingSoundWave> ();
				newGo.transform.parent = this.transform;
				newGo.transform.position = this.playerT.transform.position;
				newGo.transform.forward = this.playerT.forward;
				msw.StartPosition=this.playerT.transform.position;
				this.go.Add (newGo);
			}

		}

		CheckObjPositions ();
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

		this.renderers[0].sharedMaterial.SetInt("_Points_Length" , go.Count);
		if (go.Count > 0) {
			
			var vPos = new Vector4[20];
			var vStartPos = new Vector4[20];
			var properties = new Vector4[20];

			for ( int i=0; i < go.Count; i++) 
			{
				var waveGO = go[i];

				vPos[i] = waveGO.transform.position;
				vStartPos [i] = waveGO.GetComponent<MovingSoundWave> ().StartPosition;
				properties[i] = new Vector2(2f, waveGO.GetComponent<MovingSoundWave>().remainingLife );

			}
			//Debug.Log (vPos.Count);

			this.renderers[0].sharedMaterial.SetVectorArray("_Points", vPos);
			this.renderers[0].sharedMaterial.SetVectorArray("_StartPoints", vStartPos);
			this.renderers[0].sharedMaterial.SetVectorArray("_Properties", properties);
		}


	}
}
