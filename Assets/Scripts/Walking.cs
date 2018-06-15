using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        rigidbody.velocity = transform.forward * Time.deltaTime;
    }
}
