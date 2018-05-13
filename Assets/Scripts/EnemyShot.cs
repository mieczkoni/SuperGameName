using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

    public float movementSpeed;
    private float timer = 0.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        timer += Time.deltaTime;
        if (timer > 4.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
