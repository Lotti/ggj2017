using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour {

	public float letterPause = 0.2f;

	string message;
	Text textComp;

	public bool isWriting{ get; private set; }

	void Awake(){
		textComp = GetComponent<Text>();
		textComp.text = "";
	}

	// Use this for initialization
	public void StartType ( string msg ) {
		textComp.text = "";
		message = msg;
		isWriting = true;
		StartCoroutine( TypeText ());
	}
	public void ForceFinishWrite(){
		if (this.isWriting) {
			this.isWriting = false;
			textComp.text = this.message;
		}
	}
	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) 
		{
			textComp.text += letter;
			if (this.isWriting) {
				yield return 0;
				yield return new WaitForSeconds (letterPause);
			}
		}
		isWriting = false;
	}
}