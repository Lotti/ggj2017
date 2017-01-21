using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
/*
	private string startPhoneText = "Play as phone player";
	private string startVRText = "Play as VR player";
	enum PlayerType { PHONE, VR };

	public enum InitMode { MAIN_MENU, GAME_MENU };
	public InitMode setup; 
    public GameObject menu, phoneButton, vrButton, leaveButton;
	public int TextSize = 32;

    void Awake()
    {
		ManagerText();
    }

    private void ManagerText()
    {
		// rendi visibile solo 
		switch (setup) {
			case InitMode.MAIN_MENU:
				leaveButton.SetActive (false);
				break;
			case InitMode.GAME_MENU:
				phoneButton.SetActive (false);
				vrButton.SetActive (false);
				leaveButton.SetActive (true);
				break;
		};

		// hookup eventi
		phoneButton.GetComponent<Button>().onClick.AddListener(delegate{ 
			Play(PlayerType.PHONE); 
		});

		vrButton.GetComponent<Button> ().onClick.AddListener (delegate {
			Play (PlayerType.VR);
		});

		leaveButton.GetComponent<Button> ().onClick.AddListener (delegate {
			menu.SetActive(false);
			Debug.Log("ritorno al menu principale");
		});
*/
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

	private void Play(PlayerType plrType){
		menu.SetActive (false);

		switch (plrType) {
			case PlayerType.PHONE:
				// apri scena per il telefono
				Debug.Log("PHONE");
				break;
			case PlayerType.VR:
				// apri scena per il VR
				Debug.Log("VR");
				break;
		}

		Debug.Log("menu disabilitato, scena avviata");
	}
}
