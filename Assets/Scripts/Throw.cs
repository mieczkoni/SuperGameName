using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public float movementSpeed, range, damage;
    public Vector3 destination, originalPlayerPosition;
    private Vector3 forward;
    private float originalDistance, actualDistance;

    private void Start()
    {
        //originalDistance = calculateDistance(destination, originalPlayerPosition);
        //print("ORIGINAL DISTANCE: " + originalDistance);

        transform.forward = new Vector3(transform.forward.x, transform.forward.y + 5f, transform.forward.z);
       
    }

    // Update is called once per frame
    void Update () {
        //print("GRANADE POSITION: " + transform.position);
        //transform.forward = new Vector3(transform.forward.x, transform.forward.y + 0.2f, transform.forward.z);
        print("Forward: " + transform.forward);
        GetComponent<Rigidbody>().AddForce(transform.forward * 100);

        //transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //actualDistance = calculateDistance(transform.position, destination);
        //print("ACTUAL DISTANCE: " + actualDistance);
        //if (actualDistance >= originalDistance / 2)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        //}
        //else
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        //}
    }

    private float calculateDistance(Vector3 vector1, Vector3 vector2)
    {
        //return Mathf.Sqrt(Mathf.Pow(this.transform.position.x - originalPlayerPosition.x, 2) + Mathf.Pow(this.transform.position.z - originalPlayerPosition.z, 2));
        return Mathf.Sqrt(Mathf.Pow(vector1.x - vector2.x, 2) + Mathf.Pow(vector1.z - vector2.z, 2));
    }
}
