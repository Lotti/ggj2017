using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GameObject menu, phoneButton, vrButton;
	public enum SceneType { PHONE, VR };

	void Awake()
    {
        //phoneButton.GetComponent<Button>().onClick.AddListener(delegate { Play(SceneType.PHONE); });
        //vrButton.GetComponent<Button>().onClick.AddListener(delegate { Play(SceneType.VR); });
       
    }

    

    public void Player1Start() {

    }

    public void Player2Start() {


    }

	/*private void Play(SceneType plrType)
    {
        menu.SetActive(false);
		switch (plrType) {
			case SceneType.PHONE:
				// carica scena telefono
				break;
			case SceneType.VR:
				// carica scena vr
				break;
		}
		Debug.Log ("scena " + plrType + " caricata");

    }*/
}
