using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLightShader : MonoBehaviour {

	public List<GameObject> go;

	public Transform playerT;

	public List<Renderer> renderers;
	void Start()
	{
		this.renderers = new List<Renderer> ();
		this.renderers.AddRange( this.GetComponents<Renderer> ());
		this.renderers.AddRange (this.GetComponentsInChildren<Renderer> ());

	}

	float EffectTime;
	public void  Update(){
		
		if(EffectTime>0)
		{
			if(EffectTime < 450 && EffectTime > 400){
				this.renderers[0].sharedMaterial.SetVector("_ShieldColor", new Vector4(0.7f, 1, 1, 0) );
			}        

			EffectTime-=Time.deltaTime * 1000;

			this.renderers[0].sharedMaterial.SetFloat("_EffectTime", EffectTime);
		}

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
			if (waveGO != null) {
				//this.GetComponent<Renderer>().sharedMaterial.SetVector("_ShieldColor", new Vector4(01f, 1, 1, 0f));

				this.renderers [0].sharedMaterial.SetVector ("_Position", transform.InverseTransformPoint (waveGO.transform.position));

				EffectTime = 500;
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
