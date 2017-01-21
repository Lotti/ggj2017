using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject menu, phoneButton, vrButton;
	public enum PlayerType { PHONE, VR };

	void Awake()
    {
		phoneButton.GetComponent<Button>().onClick.AddListener(delegate { Play(PlayerType.PHONE); });
		vrButton.GetComponent<Button>().onClick.AddListener(delegate { Play(PlayerType.VR); });
    }

	private void Play(PlayerType plrType)
    {
        menu.SetActive(false);
		switch (plrType) {
			case PlayerType.PHONE:
				// carica scena telefono
				break;
			case PlayerType.VR:
				// carica scena vr
				break;
		}
		Debug.Log ("scena " + plrType + " caricata");
    }
}
