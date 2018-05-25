using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaptoPlayScript : MonoBehaviour {

    private Text myText;
    private float lerpStrength = 1f;
    private bool increasing = false;
    private GameObject shopButton;
    public GameObject weaponChoose;

    private void Start()
    {
        weaponChoose = GameObject.Find("ChooseWeapon");
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
        if (GameObject.Find("TapToPlay") != null)
        {
            myText = GameObject.Find("TapToPlay").GetComponent<Text>();
            Color lerpColor = myText.color;
            lerpColor.a = Mathf.Lerp(1, 0, lerpStrength);
            myText.color = lerpColor;
        } 
        
    }
}
