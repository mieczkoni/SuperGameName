﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    private GameObject pistolParent, minigunParent;
    private PlayerValues playerVal;

    private int actualPistolDamageUpgradeCost, actualPistolBulletsUpgradeCost, actualPistolReloadingUpgradeCost;
    private int timesPistolDamageUpgraded = 0, timesPistolBulletsUpgraded = 0, timesPistolReloadingUpgraded = 0;
    private int actualMinigunDamageUpgradeCost, actualMinigunBulletsUpgradeCost, actualMinigunReloadingUpgradeCost;
    private int timesMinigunDamageUpgraded = 0, timesMinigunBulletsUpgraded = 0, timesMinigunReloadingUpgraded = 0;

    private GameObject pistolDamageHover, pistolBulletsHover, pistolReloadingHover;
    private GameObject minigunDamageHover, minigunBulletsHover, minigunReloadingHover;

    // Use this for initialization
    void Start () {
        pistolParent = GameObject.Find("PistolUpgrade");
        minigunParent = GameObject.Find("MinigunUpgrade");
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();

        pistolDamageHover = GameObject.Find("PistolUpgrade/Background/DamageUpgradeHover");
        pistolBulletsHover = GameObject.Find("PistolUpgrade/Background/BulletUpgradeHover");
        pistolReloadingHover = GameObject.Find("PistolUpgrade/Background/ReloadingUpgradeHover");
        minigunDamageHover = GameObject.Find("MinigunUpgrade/Background/DamageUpgradeHover");
        minigunBulletsHover = GameObject.Find("MinigunUpgrade/Background/BulletUpgradeHover");
        minigunReloadingHover = GameObject.Find("MinigunUpgrade/Background/ReloadingUpgradeHover");

        GameObject weaponChoose = GameObject.Find("ChooseWeapon");
        weaponChoose.SetActive(false);
        print(weaponChoose.activeSelf);

        UpdateValues();
        UpdateCoinsValue(0);
        SetHovers();
    }

    public void UpdateCoinsValue(int addCoins)
    {
        GameObject.Find("Coins").GetComponent<Text>().text = "$ " + (playerVal.playerCoins + addCoins).ToString();
    }

    public void UpdateValues()
    {
        actualPistolDamageUpgradeCost = 100 + (50 * timesPistolDamageUpgraded);
        actualPistolBulletsUpgradeCost = 500 + (100 * timesPistolBulletsUpgraded);
        actualPistolReloadingUpgradeCost = 1000 + (500 * timesPistolReloadingUpgraded);

        actualMinigunDamageUpgradeCost = 500 + (100 * timesMinigunDamageUpgraded);
        actualMinigunBulletsUpgradeCost = 1000 + (500 * timesMinigunBulletsUpgraded);
        actualMinigunReloadingUpgradeCost = 3000 + (1000 * timesMinigunReloadingUpgraded);

        pistolParent.transform.Find("Background/DamageUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.pistolDamage.ToString();
        pistolParent.transform.Find("Background/DamageUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.pistolDamage + 15).ToString();
        pistolParent.transform.Find("Background/DamageUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolDamageUpgradeCost.ToString();

        pistolParent.transform.Find("Background/BulletUpgrade/ActualBulletsValue").GetComponent<Text>().text = playerVal.pistolBullets.ToString();
        pistolParent.transform.Find("Background/BulletUpgrade/UpgradedBulletsValue").GetComponent<Text>().text = (playerVal.pistolBullets + 1).ToString();
        pistolParent.transform.Find("Background/BulletUpgrade/BulletsUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolBulletsUpgradeCost.ToString();

        pistolParent.transform.Find("Background/ReloadingUpgrade/ActualReloadingValue").GetComponent<Text>().text = decimal.Round((decimal)playerVal.pistolReloadingTime, 1).ToString() + "s";
        pistolParent.transform.Find("Background/ReloadingUpgrade/UpgradedReloadingValue").GetComponent<Text>().text = decimal.Round((decimal)(playerVal.pistolReloadingTime - 0.1f), 1).ToString() +"s";
        pistolParent.transform.Find("Background/ReloadingUpgrade/ReloadingUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolReloadingUpgradeCost.ToString();
        
        minigunParent.transform.Find("Background/DamageUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.minigunDamage.ToString();
        minigunParent.transform.Find("Background/DamageUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.minigunDamage + 20).ToString();
        minigunParent.transform.Find("Background/DamageUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunDamageUpgradeCost.ToString();

        minigunParent.transform.Find("Background/BulletUpgrade/ActualBulletsValue").GetComponent<Text>().text = playerVal.minigunBullets.ToString();
        minigunParent.transform.Find("Background/BulletUpgrade/UpgradedBulletsValue").GetComponent<Text>().text = (playerVal.minigunBullets + 2).ToString();
        minigunParent.transform.Find("Background/BulletUpgrade/BulletsUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunBulletsUpgradeCost.ToString();

        minigunParent.transform.Find("Background/ReloadingUpgrade/ActualReloadingValue").GetComponent<Text>().text = decimal.Round((decimal)playerVal.minigunReloadingTime, 1).ToString() + "s";
        minigunParent.transform.Find("Background/ReloadingUpgrade/UpgradedReloadingValue").GetComponent<Text>().text = decimal.Round((decimal)(playerVal.minigunReloadingTime - 0.1f), 1).ToString() + "s";
        minigunParent.transform.Find("Background/ReloadingUpgrade/ReloadingUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunReloadingUpgradeCost.ToString();
    }

    private void SetHovers()
    {
        float x;
        x = (actualPistolDamageUpgradeCost > playerVal.playerCoins) ? SetHoverActive(pistolDamageHover, true) : SetHoverActive(pistolDamageHover, false);
        x = (actualPistolBulletsUpgradeCost > playerVal.playerCoins) ? SetHoverActive(pistolBulletsHover, true) : SetHoverActive(pistolBulletsHover, false);
        x = (actualPistolReloadingUpgradeCost > playerVal.playerCoins) ? SetHoverActive(pistolReloadingHover, true) : SetHoverActive(pistolReloadingHover, false);

        x = (actualMinigunDamageUpgradeCost > playerVal.playerCoins) ? SetHoverActive(minigunDamageHover, true) : SetHoverActive(minigunDamageHover, false);
        x = (actualMinigunBulletsUpgradeCost > playerVal.playerCoins) ? SetHoverActive(minigunBulletsHover, true) : SetHoverActive(minigunBulletsHover, false);
        x = (actualMinigunReloadingUpgradeCost > playerVal.playerCoins) ? SetHoverActive(minigunReloadingHover, true) : SetHoverActive(minigunReloadingHover, false);

        if (pistolDamageHover.activeSelf)
        {
            print("NO JEST");
        } else
        {
            print("NO NIE JEST");
        }
    }

    private float SetHoverActive(GameObject hover, bool state)
    {
        hover.SetActive(state);
        return 0.0f;
    }

    public void PistolDamageUpgrade()
    {
        if (!pistolDamageHover.activeSelf)
        {
            playerVal.pistolDamage += 15;
            playerVal.playerCoins -= actualPistolDamageUpgradeCost;
            timesPistolDamageUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }

    public void PistolBulletsUpgrade()
    {
        if (!pistolBulletsHover.activeSelf)
        {
            playerVal.pistolBullets += 1;
            playerVal.playerCoins -= actualPistolBulletsUpgradeCost;
            timesPistolBulletsUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }

    public void PistolReloadingUpgrade()
    {
        if (!pistolReloadingHover.activeSelf)
        {
            playerVal.pistolReloadingTime -= 0.1f;
            playerVal.playerCoins -= actualPistolReloadingUpgradeCost;
            timesPistolReloadingUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }

    public void MinigunDamageUpgrade()
    {
        if (!minigunDamageHover.activeSelf)
        {
            playerVal.minigunDamage += 20;
            playerVal.playerCoins -= actualMinigunDamageUpgradeCost;
            timesMinigunDamageUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }

    public void MinigunBulletsUpgrade()
    {
        if (!minigunBulletsHover.activeSelf)
        {
            playerVal.minigunBullets += 2;
            playerVal.playerCoins -= actualMinigunBulletsUpgradeCost;
            timesMinigunBulletsUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }

    public void MinigunReloadingUpgrade()
    {
        if (!minigunReloadingHover.activeSelf)
        {
            playerVal.minigunReloadingTime -= 0.1f;
            playerVal.playerCoins -= actualMinigunReloadingUpgradeCost;
            timesMinigunReloadingUpgraded += 1;
            UpdateValues();
            UpdateCoinsValue(0);
            SetHovers();
        }
    }
}
