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
				var newGo = new GameObject ();
				newGo.transform.parent = this.transform;
				newGo.transform.position = this.playerT.transform.position;
				newGo.transform.forward = this.playerT.forward;
				newGo.AddComponent<MovingSoundWave> ();

				this.go.Add (newGo);

			}

		}
		CheckObjPositions ();
	}
	public List<int> toDel=new List<int>();

	public void  CheckObjPositions( ) {


		for ( int i=0; i < go.Count; i++) 
		{
			var waveGO = go [i];
			if (waveGO != null) 
			{
				this.renderers[0].sharedMaterial.SetVector("_Points" + i.ToString(), waveGO.transform.position);
				Vector2 properties = new Vector2(10, 10);
				this.renderers[0].sharedMaterial.SetVector("_Properties" + i.ToString(), properties);

			} else {
				toDel.Add (i);
			}

		}

		if (toDel.Count > 0) {
			foreach (int i in toDel) 
			{
				
				go.RemoveAt (i);
			}
			toDel.Clear ();
		}

	}
}
