using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoom : MonoBehaviour {

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z >= transform.position.z + 23)
        {
            //Debug.Log("NO JEST DALEJ");
            //Debug.Log(player.transform.position.z);
            Destroy(this.gameObject);
        }
	}
}
