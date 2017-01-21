using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool gameInPause = false;

    public Text txt;



    public GameObject menu,button;

    private string playText = "Play";

    private string quitText = "Quit";

    void Awake()
    {
        ManagerSizeText();
    }

    void Update()
    {
        ManagerText();
        if (gameInPause)
            button.GetComponent<Button>().onClick.AddListener(delegate { Play(); });
        else if (gameInPause)
            button.GetComponent<Button>().onClick.AddListener(delegate { Quit(); });
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
            txt.GetComponent<Text>().text = playText;
        else if(gameInPause)
            txt.GetComponent<Text>().text = quitText;       
    }

    private void ManagerSizeText()
    {
        txt.GetComponent<Text>().fontSize = 32;
    }

}
