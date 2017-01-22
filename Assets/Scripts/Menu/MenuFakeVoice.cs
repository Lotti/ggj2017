using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuFakeVoice : MonoBehaviour 
{

	float lastTimeUpdate=0;
	float nextDelay=0;
	Tween tweening;
	int lastSign = -1;
	// Update is called once per frame
	void Update () 
	{
		if(lastTimeUpdate == 0 || lastTimeUpdate >= nextDelay){
			lastTimeUpdate=0;
			nextDelay = Random.Range (1f, 5f);
			VoiceLightShader2.Instance.SpawnVoice ( this.transform.forward );

		}
		if (tweening == null || !tweening.IsPlaying ()) 
		{	
			tweening = this.transform.DOMove (this.transform.position + this.transform.right * lastSign * Random.Range (1, 1.5f), Random.Range (4, 5));
			lastSign = -1 * lastSign;
		}
		VoiceLightShader2.Instance.CheckObjPositions ();
		lastTimeUpdate+=Time.deltaTime;
	}
}
