using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    private float timer = 0.0f;
    public GameObject enemy1;
    public GameObject enemy2;
    public float spawnTime = 1f;

    public int gameLevel;
    private bool randomSpawn = true;

	// Update is called once per frame
	void Update () {
        if (randomSpawn == true)
        {
            timer += Time.deltaTime;
            if (spawnTime >= 0.4f)
            {
                spawnTime -= 0.0005f;
            }
            if (timer > spawnTime)
            {
                RandomSpawning();
                timer = 0.0f;
            }
        }
    }

    public void StopAndResetSpawning()
    {
        spawnTime = 1f;
        randomSpawn = false;
    }

    public void StartRandomSpawning()
    {
        randomSpawn = true;
    }

    public void RandomSpawning()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) { SpawnObject(0); }
        else { SpawnObject(1); }
    }

    public void SpawnObject(int x, int optZ = 25)
    {
        if (x == 0)
        {
            GameObject newEnemy = Instantiate(enemy1, new Vector3(Random.Range(-4f, 4f), 0.8f, transform.position.z + optZ), Quaternion.identity);
            newEnemy.transform.name = "WeakEnemy";
            newEnemy.transform.Rotate(0, 180, 0);
            newEnemy.GetComponent<WeakEnemyAI>().SetEnemyValues(100 * (gameLevel + 1), 10 * (gameLevel +1));
        } else
        {
            GameObject newEnemy = Instantiate(enemy1, new Vector3(Random.Range(-4f, 4f), 0.8f, transform.position.z + 30), Quaternion.identity);
            newEnemy.transform.name = "WeakEnemy";
            newEnemy.transform.Rotate(0, 180, 0);
            newEnemy.GetComponent<WeakEnemyAI>().SetEnemyValues(100 * (gameLevel + 1), 10 * (gameLevel + 1));
        }
    }
}
