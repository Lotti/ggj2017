using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool gameInPause = false;

    public GameObject play;

    public GameObject menu;

    private string playText = "Play";

    private string quitText = "Quit";

    void Awake()
    {
        ManagerSizeText();
    }
    
    private void Play()
    {
        menu.SetActive(false);
        gameInPause = false;
    }

    private void Quit()
    {
        menu.SetActive(true);
        gameInPause = true;
    }

    private void ManagerText()
    {
        if (!gameInPause)
            play.GetComponent<Text>().text = playText;
        else if(gameInPause)
            play.GetComponent<Text>().text = quitText;       
    }

    private void ManagerSizeText()
    {
        play.GetComponent<Text>().fontSize = 32;
    }
}
