using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponController : MonoBehaviour {

    public Sprite pistolIcon, minigunIcon;
    public Sprite pistolBWIcon, minigunBWIcon;
    private PlayerValues playerVal;
    private PlayerShooting playerShot;
    private GameObject pistol, minigun, changingWeapon;
    private CanvasController controller;

    private bool isPistolActive = true, isMinigunActive = true;

    private string changingWeaponName;

	// Use this for initialization
	void Start () {
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();
        playerShot = GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>();
        pistol = GameObject.Find("ChooseWeaponCanvas/Container/Weapons/Pistol");
        minigun = GameObject.Find("ChooseWeaponCanvas/Container/Weapons/Minigun");
        controller = GameObject.Find("MainCanvas").GetComponent<CanvasController>();

        Refresh();
    }

    void Refresh()
    {
        string[] playerWeapons = { playerVal.mainWeapon, playerVal.secondWeapon, playerVal.thirdWeapon };
        foreach (string weapon in playerWeapons)
        {
            switch (weapon)
            {
                case "Pistol":
                    isPistolActive = false;
                    break;
                case "Minigun":
                    isMinigunActive = false;
                    break;
            }
        }

        switch (playerVal.mainWeapon)
        {
            case "Pistol":
                playerShot.gunType = "Pistol";
                break;
            case "Minigun":
                playerShot.gunType = "Minigun";
                break;
        }

        if (!isPistolActive)
        {
            pistol.GetComponent<Image>().sprite = pistolBWIcon;
        } else
        {
            pistol.GetComponent<Image>().sprite = pistolIcon;
        }

        if (!isMinigunActive)
        {
            minigun.GetComponent<Image>().sprite = minigunBWIcon;
        } else
        {
            minigun.GetComponent<Image>().sprite = minigunIcon;
        }

        //print("1: " + playerVal.mainWeapon);
        //print("2: " + playerVal.secondWeapon);
        //print("3: " + playerVal.thirdWeapon);
    }

    public void ChangeMinigun()
    {
        if (isMinigunActive)
        {
            changingWeapon.GetComponent<Image>().sprite = minigunIcon;
            playerVal.SetStringValues(changingWeaponName, "Minigun");
            isMinigunActive = false;
            isPistolActive = true;
            controller.HideWeaponChooseCanvas();
            Refresh();
        }
    }

    public void ChangePistol()
    {
        if (isPistolActive)
        {
            changingWeapon.GetComponent<Image>().sprite = pistolIcon;
            playerVal.SetStringValues(changingWeaponName, "Pistol");
            isPistolActive = false;
            isMinigunActive = true;
            controller.HideWeaponChooseCanvas();
            Refresh();
        }
    }

    public void MainWeaponShow()
    {
        if (controller.inGameCanvas.GetComponent<Canvas>().enabled == false)
        {
            controller.ShowWeaponChooseCanvas();
            changingWeapon = GameObject.Find("Background/MainWeapon");
            changingWeaponName = "mainWeapon";
        } else
        {
            playerShot.gunType = playerVal.mainWeapon;
        }
    }

    public void SecondWeaponShow()
    {
        if (controller.inGameCanvas.GetComponent<Canvas>().enabled == false)
        {
            changingWeapon = GameObject.Find("Background/SecondWeapon");
            changingWeaponName = "secondWeapon";
            controller.ShowWeaponChooseCanvas();
        } else
        {
            playerShot.gunType = playerVal.secondWeapon;
        }
    }

    public void ThirdWeaponShow()
    {
        if (controller.inGameCanvas.GetComponent<Canvas>().enabled == false)
        {
            controller.ShowWeaponChooseCanvas();
            changingWeapon = GameObject.Find("Background/ThirdWeapon");
            changingWeaponName = "thirdWeapon";
        } else
        {
            playerShot.gunType = playerVal.thirdWeapon;
        }
    }
}
