using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public GameObject weaponsCNB, weaponChooseCanvas, inGameCanvas, startCanvas, pauseCanvas;
    private Button shopButton;
    //public Canvas shopCanvas, weaponChooseCanvas, inGameCanvas, startCanvas, pauseCanvas, shopButton;
    private GameObject player;
    private PlayerValues playerVal;
    private GameController gameController;
    private bool isGameOver, isInGame = false;

	// Use this for initialization
	void Start () {
        weaponsCNB = GameObject.Find("WeaponsCNB");
        weaponChooseCanvas = GameObject.Find("ChooseWeaponCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        startCanvas = GameObject.Find("StartCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");
        gameController = GameObject.Find("Player").GetComponent<GameController>();

        player = GameObject.Find("Player");
        playerVal = player.GetComponent<PlayerValues>();

        player.GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;

        playerVal.UpdateCoinsValue(0);
        HidePauseCanvas();
        HideWeaponsCNB();
        //HideWeaponChooseCanvas();
        HideInGameCanvas();
    }

    public void ShowWeaponsCNB()
    {
        if (isInGame == false)
        {
            weaponsCNB.GetComponent<Canvas>().enabled = true;
            GameObject.Find("WeaponsCNB").GetComponent<CNBScript>().InstantiateWeaponsPrefabs();
        }
    }
    
    public void HideWeaponsCNB()
    {
        weaponsCNB.GetComponent<Canvas>().enabled = false;
    }

    public void ShowSkillsCNB()
    {
        if (weaponsCNB.GetComponent<Canvas>().isActiveAndEnabled)
        {
            HideCNBCanvas();
        }
        if (isInGame == false)
        {
            weaponsCNB.GetComponent<Canvas>().enabled = true;
            GameObject.Find("WeaponsCNB").GetComponent<CNBScript>().InstantiateSkillsPrefabs();
        }
    }

    public void HideCNBCanvas()
    {
        weaponsCNB.GetComponent<Canvas>().enabled = false;
        GameObject.Find("WeaponsCNB").GetComponent<CNBScript>().DestroyPrefabs();
    }

    //public void ShowShopCanvas()
    //{
    //    shopCanvas.GetComponent<Canvas>().enabled = true;
    //    GameObject.Find("WeaponsCNB").GetComponent<CNBScript>().InstantiateWeaponsPrefabs();
    //    //GameObject.Find("ShopCanvas").GetComponent<ShopController>().SetHovers();
    //    //HideWeaponChooseCanvas();
    //    HideShopButton();
    //}

    //public void HideShopCanvas()
    //{
    //    //GameObject.Find("ChooseAndBuyCanvas").GetComponent<CNBScript>().DeleteInstantiatedPrefabs();
    //    shopCanvas.GetComponent<Canvas>().enabled = false;
    //    ShowShopButton();
    //}

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
        isInGame = true;
    }

    public void HideInGameCanvas()
    {
        inGameCanvas.GetComponent<Canvas>().enabled = false;
        isInGame = false;
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
            playerVal.initStartGame();
            isGameOver = false;
        } else
        {
            HideStartCanvas();
            ShowInGameCanvas();
            playerVal.initStartGame();
        }
        gameController.BeginCalculations();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        playerVal.initPauseGame();
        HideInGameCanvas();
        ShowPauseCanvas();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        playerVal.initResumeGame();
        ShowInGameCanvas();
        HidePauseCanvas();
    }

    public void GameOver()
    {
        print("game over");
        isGameOver = true; 
        ShowStartCanvas();
        HideInGameCanvas();
        HidePauseCanvas();
        playerVal.initGameOver();
    }
}
