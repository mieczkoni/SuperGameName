using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    int actualDistanceWalked;
    float startGamePoint;
    private bool walking = false;
    GameObject metersWalkedText, recordText;


	// Use this for initialization
	void Start () {
        metersWalkedText = GameObject.Find("InGameCanvas/MetersWalked");
        recordText = GameObject.Find("StartCanvas/Record");
	}
	
	// Update is called once per frame
	void Update () {
        if (walking)
        {
            actualDistanceWalked = (int)(transform.position.z - startGamePoint) / 2;
            metersWalkedText.GetComponent<Text>().text = actualDistanceWalked.ToString() + "m";
        }
	}

    public void BeginCalculations()
    {
        walking = true;
        startGamePoint = transform.position.z;
    }

    public void PlayerDeath()
    {
        walking = false;
        UpdateRecord();
    }

    public void UpdateRecord()
    {
        int endResult = actualDistanceWalked;
        if (endResult >= GetComponent<PlayerValues>().distanceRecord)
        {
            GetComponent<PlayerValues>().UpdateValues("distanceRecord", endResult);
            recordText.GetComponent<Text>().text = "YOU ARE " + endResult.ToString() + "m CLOSER TO INFINITY!";
        }
    }

}
