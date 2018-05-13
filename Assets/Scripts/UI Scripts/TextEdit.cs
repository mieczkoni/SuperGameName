using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEdit : MonoBehaviour {

    private Text myText;

	public void changeText(string newText)
    {
        myText = GetComponent<Text>();
        myText.text = newText;       
    }
}
