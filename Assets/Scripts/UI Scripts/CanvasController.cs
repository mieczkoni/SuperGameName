using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public GameObject weaponsCNB, weaponChooseCanvas, inGameCanvas, startCanvas, pauseCanvas;
    private Button shopButton;
    private GameObject player;
    private PlayerValues playerVal;
    private GameController gameController;
    public bool isGameOver, isInGame = false;

    private SecondaryWeaponController secondaryWeaponController;
    private SkillsController skillsController;

	void Start () {
        weaponsCNB = GameObject.Find("WeaponsCNB");
        weaponChooseCanvas = GameObject.Find("ChooseWeaponCanvas");
        inGameCanvas = GameObject.Find("InGameCanvas");
        startCanvas = GameObject.Find("StartCanvas");
        pauseCanvas = GameObject.Find("PauseCanvas");
        gameController = GameObject.Find("Player").GetComponent<GameController>();

        player = GameObject.Find("Player");
        playerVal = player.GetComponent<PlayerValues>();
        secondaryWeaponController = player.GetComponent<SecondaryWeaponController>();
        skillsController = player.GetComponent<SkillsController>();

        player.GetComponent<SpawnEnemies>().enabled = false;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;

        playerVal.UpdateCoinsValue(0);
        HidePauseCanvas();
        HideCNBCanvas();
        HideInGameCanvas();
    }

    public void ShowWeaponsCNB()
    {
        if (isInGame == false)
        {
            if (weaponsCNB.GetComponent<CNBScript>().weaponsCNBActive == false)
            {
                if (weaponsCNB.GetComponent<Canvas>().isActiveAndEnabled)
                {
                    HideCNBCanvas();
                }
                weaponsCNB.GetComponent<Canvas>().enabled = true;
                weaponsCNB.GetComponent<CNBScript>().InstantiateWeaponsPrefabs();
            }
        }
    }

    public void ShowSecondaryWeaponsCNB()
    {
        if (isInGame == false)
        {
            if (weaponsCNB.GetComponent<CNBScript>().secondaryWeaponsCNBActive == false)
            {
                if (weaponsCNB.GetComponent<Canvas>().isActiveAndEnabled)
                {
                    HideCNBCanvas();
                }
                weaponsCNB.GetComponent<Canvas>().enabled = true;
                weaponsCNB.GetComponent<CNBScript>().InstantiateSecondaryWeaponPrefabs();
            }
        } else if (isInGame == true)
        {
            secondaryWeaponController.ThrowSomething();
        }
    }

    public void ShowSkillsCNB()
    {
        if (isInGame == false)
        {
            if (weaponsCNB.GetComponent<CNBScript>().skillsCNBActive == false)
            {
                if (weaponsCNB.GetComponent<Canvas>().isActiveAndEnabled)
                {
                    HideCNBCanvas();
                }
                weaponsCNB.GetComponent<Canvas>().enabled = true;
                weaponsCNB.GetComponent<CNBScript>().InstantiateSkillsPrefabs();
            }
        } else if (isInGame == true)
        {
            skillsController.UseSkill();
        }
    }

    public void HideCNBCanvas()
    {
        weaponsCNB.GetComponent<Canvas>().enabled = false;
        weaponsCNB.GetComponent<CNBScript>().DestroyPrefabs();
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
        isGameOver = true; 
        ShowStartCanvas();
        HideInGameCanvas();
        HidePauseCanvas();
        playerVal.initGameOver();
    }

    public void Surrender()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        isGameOver = true;
        ShowStartCanvas();
        HideInGameCanvas();
        HidePauseCanvas();
        playerVal.initGameOver();
    }
}
