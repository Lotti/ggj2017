using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GameObject menu, phoneButton, vrButton, panel, pin, inputField, code;
	public enum SceneType { PHONE, VR };

	void Awake()
    {
        //phoneButton.GetComponent<Button>().onClick.AddListener(delegate { Play(SceneType.PHONE); });
        //vrButton.GetComponent<Button>().onClick.AddListener(delegate { Play(SceneType.VR); });
    }

    

    public void Player1Start() {
		panel.SetActive(true);
		inputField.SetActive(false);
		pin.SetActive(true);
		code.GetComponent<Text>().text = Networking.Instance.playAsPlayer1();
    }

    public void Player2Start() {
		panel.gameObject.SetActive(true);
		inputField.SetActive(true);
		pin.SetActive(false);
    }

	public void ConfirmPin() {
		Networking.Instance.playAsPlayer2(inputField.GetComponent<InputField>().text);	
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
