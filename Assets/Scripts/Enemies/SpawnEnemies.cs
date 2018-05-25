using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    private float timer = 0.0f;
    public GameObject enemy1;
    public GameObject enemy2;
    private float spawnTime = 1f;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (spawnTime >= 0.4f)
        {
            //print(spawnTime);
            spawnTime -= 0.0005f;
        }
        //print("DELTA: " + Time.deltaTime);
        if (timer > spawnTime)
        {
            SpawnObject();
            timer = 0.0f;
        }
    }

    void SpawnObject()
    {
        //print("DUpa: " + enemies.Length);
        //print(Random.Range(0, enemies.Length - 1));
        int rand = Random.Range(0, 2);
        //enemy1 = GameObject.Find("WeakEnemy");
        //enemy2 = GameObject.Find("MediumEnemy");
        if (rand == 0)
        {
            GameObject newEnemy = Instantiate(enemy1, new Vector3(Random.Range(-4.0f, 4.0f), 0.8f, transform.position.z + 30), Quaternion.identity);
            newEnemy.transform.Rotate(0, 180, 0);
        } else
        {
            GameObject newEnemy = Instantiate(enemy1, new Vector3(Random.Range(-4.0f, 4.0f), 0.8f, transform.position.z + 30), Quaternion.identity);
            newEnemy.transform.Rotate(0, 180, 0);
        }
        //GameObject newBullet = Instantiate(enemy, new Vector3(Random.Range(-4.0f, 4.0f), 0.8f, transform.position.z + 30), Quaternion.identity);
    }
}
