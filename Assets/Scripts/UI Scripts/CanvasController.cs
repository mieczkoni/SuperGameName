using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    private GameObject shopCanvas, weaponChooseCanvas, inGameCanvas, startCanvas, pauseCanvas, gameOverCanvas, shopButton;

	// Use this for initialization
	void Start () {
        shopCanvas = GameObject.Find("ShopCanvas");
        weaponChooseCanvas = GameObject.Find("ChooseWeaponCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        startCanvas = GameObject.Find("StartCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");
        gameOverCanvas = GameObject.Find("GameOverCanvas");

        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;

        HideShopCanvas();
        HidePauseCanvas();
        HideWeaponChooseCanvas();
        HideInGameCanvas();
        HideGameOverCanvas();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowShopCanvas()
    {
        shopCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideShopCanvas()
    {
        shopCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ShowWeaponChooseCanvas()
    {
        weaponChooseCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideWeaponChooseCanvas()
    {
        weaponChooseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ShowInGameCanvas()
    {
        inGameCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideInGameCanvas()
    {
        inGameCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ShowStartCanvas()
    {
        startCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideStartCanvas()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ShowPauseCanvas()
    {
        pauseCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HidePauseCanvas()
    {
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void ShowGameOverCanvas()
    {
        gameOverCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideGameOverCanvas()
    {
        gameOverCanvas.GetComponent<Canvas>().enabled = false;
    }


    public void StartGame()
    {
        HideStartCanvas();
        ShowInGameCanvas();
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = true;
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        //GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerWalking>().playerSpeed = 0f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;
        HideInGameCanvas();
        ShowPauseCanvas();
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("Player").GetComponent<PlayerWalking>().playerSpeed = 0.05f;
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = true;
        ShowInGameCanvas();
        HidePauseCanvas();
    }

    public void GameOver()
    {
        ShowGameOverCanvas();
        HideInGameCanvas();
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerWalking>().playerSpeed = 0.0f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;
    }

    public void StartOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        HideGameOverCanvas();
        ShowInGameCanvas();
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerWalking>().playerSpeed = 0.05f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = true;
    }
}
