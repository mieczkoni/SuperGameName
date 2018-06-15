using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaptoPlayScript : MonoBehaviour {

    private Text myText;
    private float lerpStrength = 1f;
    private bool increasing = false;
    public GameObject weaponChoose, tapToPlay;

    private void Start()
    {
        weaponChoose = GameObject.Find("ChooseWeapon");
        tapToPlay = GameObject.Find("TapToPlay");
        myText = tapToPlay.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!increasing)
        {
            lerpStrength -= 0.02f;
            if (lerpStrength <= 0.1f)
                increasing = true;
        }
        else
        {
            lerpStrength += 0.02f;
            if (lerpStrength >= 0.9f)
                increasing = false;
        }
        if (tapToPlay != null)
        {
            Color lerpColor = myText.color;
            lerpColor.a = Mathf.Lerp(1, 0, lerpStrength);
            myText.color = lerpColor;
        } 
    }
}
