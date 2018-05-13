using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewRoom : MonoBehaviour {

    public GameObject wallObject;
    public GameObject floorObject;
    GameObject previousFloorObject;
    GameObject player;
    bool newFloorMade = false;

    // Use this for initialization
    void Start () {
        previousFloorObject = GameObject.Find("Floor");
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log((int) player.transform.position.z % 43);
        if ((int) player.transform.position.z % 43 == 3.0f && newFloorMade == false)
        {
            //Debug.Log("TERAZ");
            createNewObjects();
        } else if ((int) player.transform.position.z % 43 == 4.0f && newFloorMade == true)
        {
            newFloorMade = false;
        }
	}

    void createNewObjects()
    {
        GameObject newFloor = Instantiate(floorObject, new Vector3(previousFloorObject.transform.position.x, -0.25f, previousFloorObject.transform.position.z + 40), Quaternion.identity);
        Instantiate(wallObject, new Vector3(5, 3f, previousFloorObject.transform.position.z + 40), Quaternion.identity);
        Instantiate(wallObject, new Vector3(-5, 3f, previousFloorObject.transform.position.z + 40), Quaternion.identity);
        previousFloorObject = newFloor;
        newFloorMade = true;
    }
}
