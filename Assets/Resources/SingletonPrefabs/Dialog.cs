using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public Image img;
    public Text txt;

    // Use this for initialization

    void Awake()
    {
        //string myText=
        //JsonUtility.FromJson<>();
    }

    void Update()
    {

    }

    void ShowDialogStep(Image imgtoShow, string textToShow)
    {
        img = imgtoShow;
        txt.text = textToShow;
    }


}

