using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public GameObject shopCanvas, weaponChooseCanvas, inGameCanvas, startCanvas, pauseCanvas, shopButton;
    private bool isGameOver;

	// Use this for initialization
	void Start () {
        shopCanvas = GameObject.Find("ShopCanvas");
        weaponChooseCanvas = GameObject.Find("ChooseWeaponCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        startCanvas = GameObject.Find("StartCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");
        shopButton = GameObject.Find("ShopButtonCanvas");

        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;

        HideShopCanvas();
        HidePauseCanvas();
        HideWeaponChooseCanvas();
        HideInGameCanvas();
    }

    public void ShowShopButton()
    {
        shopButton.GetComponent<Canvas>().enabled = true;
    }

    public void HideShopButton()
    {
        shopButton.GetComponent<Canvas>().enabled = false;
    }

    public void ShowShopCanvas()
    {
        shopCanvas.GetComponent<Canvas>().enabled = true;
        HideShopButton();
    }

    public void HideShopCanvas()
    {
        shopCanvas.GetComponent<Canvas>().enabled = false;
        ShowShopButton();
    }

    public void ShowWeaponChooseCanvas()
    {
        weaponChooseCanvas.GetComponent<Canvas>().enabled = true;
        HideShopButton();
    }

    public void HideWeaponChooseCanvas()
    {
        weaponChooseCanvas.GetComponent<Canvas>().enabled = false;
        ShowShopButton();
    }

    public void ShowInGameCanvas()
    {
        inGameCanvas.GetComponent<Canvas>().enabled = true;
        HideShopButton();
    }

    public void HideInGameCanvas()
    {
        inGameCanvas.GetComponent<Canvas>().enabled = false;
        ShowShopButton();
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


    public void StartGame()
    {
        if (isGameOver)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            HideStartCanvas();
            ShowInGameCanvas();
            GameObject.Find("Player").GetComponent<PlayerValues>().playerHealth = 100;
            GameObject.Find("Player").GetComponent<PlayerValues>().playerSpeed = 0.05f;
            isGameOver = false;
        } else
        {
            HideStartCanvas();
            ShowInGameCanvas();
            GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().SetBulletText();            
        }
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = true;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        //GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerValues>().playerSpeed = 0f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;
        HideInGameCanvas();
        ShowPauseCanvas();
        HideShopButton();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("Player").GetComponent<PlayerValues>().playerSpeed = 0.05f;
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = true;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = true;
        ShowInGameCanvas();
        HidePauseCanvas();
    }

    public void GameOver()
    {
        isGameOver = true; 
        ShowStartCanvas();
        HideInGameCanvas();
        GameObject.Find("Player").GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerValues>().playerSpeed = 0.0f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;
    }
}
