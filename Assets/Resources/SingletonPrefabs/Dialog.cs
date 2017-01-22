using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class Container
{
    public string imageUlr;
    public string textUrl;
}

[Serializable]
public class JsonContainer
{
    public List<Container> myContainer = new List<Container>();
}

public class Dialog : MonoBehaviour {

    public Image img;
    public Text txt;
    JsonContainer myJsonText;

    public GameObject dialog;

    public int listCount = 0;

    void Awake()
    {
        myJsonText = new JsonContainer();

        myJsonText = JsonUtility.FromJson<JsonContainer>(Resources.Load<TextAsset>("GameText").ToString());

        listCount = 0;

        dialog.SetActive(false);

        StartCoroutine("StartAll");
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dialog.SetActive(true);
            if (listCount < myJsonText.myContainer.Count)
            {
                ShowDialogStep(myJsonText.myContainer[listCount].imageUlr, myJsonText.myContainer[listCount].textUrl);
                listCount++;
            }
            else
            {
                //CAMBIA LIVeLLO
                SceneManager.LoadScene("StartMenu");
            }
            
           
        }
    }

    IEnumerator StartAll()
    {
        yield return new WaitForSeconds(1f);

        ShowDialogStep(myJsonText.myContainer[listCount].imageUlr, myJsonText.myContainer[listCount].textUrl);
        if (listCount < myJsonText.myContainer.Count - 1)
        {
            listCount++;
            dialog.SetActive(true);
        }
    }

    void ShowDialogStep(string imgtoShow, string textToShow)
    {
        img.sprite = (Sprite)Resources.Load<Sprite>("Img/"+imgtoShow);
        txt.text = textToShow;
    }
}

