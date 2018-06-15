using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    int actualDistanceWalked;
    float startGamePoint;
    private bool walking = false;
    GameObject recordText;
    Text metersWalkedText;

    private int difficultyLevel = 0;
    private float waitTimer = 0.0f;
    private bool waiting = false;
    private GameObject player;
    private PlayerValues playerVal;
    private SpawnEnemies spawnEnemies;
    
    void Start () {
        difficultyLevel = PlayerPrefs.GetInt("difficultyLevel", 0);
        player = GameObject.Find("Player");
        playerVal = player.GetComponent<PlayerValues>();
        spawnEnemies = player.GetComponent<SpawnEnemies>();
        metersWalkedText = GameObject.Find("InGameCanvas/MetersWalked").GetComponent<Text>();
        recordText = GameObject.Find("StartCanvas/Record");
        GameObject.Find("WeaponsCanvas/DifficultyLevel").GetComponent<Text>().text = difficultyLevel.ToString();
    }
	
	void Update () {
        if (walking)
        {
            actualDistanceWalked = (int)(transform.position.z - startGamePoint) / 2;
            metersWalkedText.text = ((difficultyLevel * 100) + actualDistanceWalked).ToString() + "m";
            if (actualDistanceWalked % 100 == 50)
            {
                GameLevelFinal();
                walking = false;
            }
        }
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= 4.0f)
            {
                waiting = false;
                waitTimer = 0.0f;
            }
        }
	}

    public void GameLevelFinal()
    {
        playerVal.playerSpeed = 0.0f;
        spawnEnemies.gameLevel = difficultyLevel;
        spawnEnemies.StopAndResetSpawning();
        StartCoroutine(MyMethod());
    }

    public void InitNextLevel()
    {
        spawnEnemies.StartRandomSpawning();
    }

    public void BeginCalculations()
    {
        walking = true;
        startGamePoint = transform.position.z;
    }

    public void PlayerDeath()
    {
        PlayerPrefs.SetInt("difficultyLevel", difficultyLevel);
        PlayerPrefs.Save();
        walking = false;
        UpdateRecord();
    }

    public void UpdateRecord()
    {
        int endResult = actualDistanceWalked;
        if (endResult >= playerVal.distanceRecord)
        {
            playerVal.UpdateValues("distanceRecord", endResult);
            recordText.GetComponent<Text>().text = "YOU ARE " + endResult.ToString() + "m CLOSER TO INFINITY!";
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i <= 15; i++)
        {
            int rand = Random.Range(20, 27);
            spawnEnemies.SpawnObject(0, rand);
        }
    }

    IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(5);
        SpawnObjects();
        yield return new WaitForSeconds(5);
        SpawnObjects();
        yield return new WaitForSeconds(5);
        SpawnObjects();
        yield return new WaitForSeconds(15);
        spawnEnemies.gameLevel += 1;
        InitNextLevel();
        GameObject.Find("WeaponsCanvas/DifficultyLevel").GetComponent<Text>().text = difficultyLevel.ToString();
    }

    public void SaveGameControllerData()
    {
        PlayerPrefs.SetInt("difficultyLevel", 0);
        PlayerPrefs.Save();
    }
}
