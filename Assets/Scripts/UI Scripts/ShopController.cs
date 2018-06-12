using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    private GameObject pistolParent, minigunParent;
    private PlayerValues playerVal;

    private int actualPistolDamageUpgradeCost, actualPistolCriticUpgradeCost, actualPistolBPSUpgradeCost;
    private int timesPistolDamageUpgraded = 0, timesPistolCriticUpgraded = 0, timesPistolBPSUpgraded = 0;
    private int actualMinigunDamageUpgradeCost, actualMinigunCriticUpgradeCost, actualMinigunBPSUpgradeCost;
    private int timesMinigunDamageUpgraded = 0, timesMinigunCriticUpgraded = 0, timesMinigunBPSUpgraded = 0;

    private GameObject pistolDamageHover, pistolCriticHover, pistolBPSHover;
    private GameObject minigunDamageHover, minigunCriticHover, minigunBPSHover;

    private Button pistolDamageUpgradeButton, pistolCriticUpgradeButton, pistolBPSUpgradeButton;
    private Button minigunDamageUpgradeButton, minigunCriticUpgradeButton, minigunBPSUpgradeButton;

    void Start () {
        pistolParent = GameObject.Find("PistolUpgrade");
        minigunParent = GameObject.Find("MinigunUpgrade");
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();

        pistolDamageHover = GameObject.Find("PistolUpgrade/Background/DamageUpgradeHover");
        pistolCriticHover = GameObject.Find("PistolUpgrade/Background/CriticalShotUpgradeHover");
        pistolBPSHover = GameObject.Find("PistolUpgrade/Background/BPSUpgradeHover");
        minigunDamageHover = GameObject.Find("MinigunUpgrade/Background/DamageUpgradeHover");
        minigunCriticHover = GameObject.Find("MinigunUpgrade/Background/CriticalShotUpgradeHover");
        minigunBPSHover = GameObject.Find("MinigunUpgrade/Background/BPSUpgradeHover");

        pistolDamageUpgradeButton = GameObject.Find("PistolUpgrade/Background/DamageUpgrade").GetComponent<Button>();
        pistolCriticUpgradeButton = GameObject.Find("PistolUpgrade/Background/CriticalShotUpgrade").GetComponent<Button>();
        pistolBPSUpgradeButton = GameObject.Find("PistolUpgrade/Background/BPSUpgrade").GetComponent<Button>();
        minigunDamageUpgradeButton = GameObject.Find("MinigunUpgrade/Background/DamageUpgrade").GetComponent<Button>();
        minigunCriticUpgradeButton = GameObject.Find("MinigunUpgrade/Background/CriticalShotUpgrade").GetComponent<Button>();
        minigunBPSUpgradeButton = GameObject.Find("MinigunUpgrade/Background/BPSUpgrade").GetComponent<Button>();

        UpdateValues();
        SetHovers();
        playerVal.UpdateCoinsValue(0);
    }

    public void UpdateValues()
    {
        actualPistolDamageUpgradeCost = 100 + (50 * timesPistolDamageUpgraded);
        actualPistolCriticUpgradeCost = 1000 + (80 * timesPistolCriticUpgraded);
        actualPistolBPSUpgradeCost = 3000 + (250 * timesPistolBPSUpgraded);

        actualMinigunDamageUpgradeCost = 500 + (100 * timesMinigunDamageUpgraded);
        actualMinigunCriticUpgradeCost = 5000 + (160 * timesMinigunCriticUpgraded);
        actualMinigunBPSUpgradeCost = 15000 + (1250 * timesMinigunBPSUpgraded);

        pistolParent.transform.Find("Background/DamageUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.pistolDamage.ToString();
        pistolParent.transform.Find("Background/DamageUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.pistolDamage + 15).ToString();
        pistolParent.transform.Find("Background/DamageUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolDamageUpgradeCost.ToString();

        pistolParent.transform.Find("Background/CriticalShotUpgrade/ActualDamageValue").GetComponent<Text>().text = (playerVal.pistolCriticalShotChance * 100).ToString() + "%";
        pistolParent.transform.Find("Background/CriticalShotUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.pistolCriticalShotChance * 100 + 1).ToString() + "%";
        pistolParent.transform.Find("Background/CriticalShotUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolCriticUpgradeCost.ToString();

        pistolParent.transform.Find("Background/BPSUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.pistolBulletsPerSecond.ToString("F1");
        pistolParent.transform.Find("Background/BPSUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.pistolBulletsPerSecond + 0.1).ToString("F1");
        pistolParent.transform.Find("Background/BPSUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualPistolBPSUpgradeCost.ToString();


        minigunParent.transform.Find("Background/DamageUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.minigunDamage.ToString();
        minigunParent.transform.Find("Background/DamageUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.minigunDamage + 20).ToString();
        minigunParent.transform.Find("Background/DamageUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunDamageUpgradeCost.ToString();

        minigunParent.transform.Find("Background/CriticalShotUpgrade/ActualDamageValue").GetComponent<Text>().text = (playerVal.minigunCriticalShotChance * 100).ToString() + "%";
        minigunParent.transform.Find("Background/CriticalShotUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.minigunCriticalShotChance * 100 + 1).ToString() + "%";
        minigunParent.transform.Find("Background/CriticalShotUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunCriticUpgradeCost.ToString();

        minigunParent.transform.Find("Background/BPSUpgrade/ActualDamageValue").GetComponent<Text>().text = playerVal.minigunBulletsPerSecond.ToString("F1");
        minigunParent.transform.Find("Background/BPSUpgrade/UpgradedDamageValue").GetComponent<Text>().text = (playerVal.minigunBulletsPerSecond + 0.1).ToString("F1");
        minigunParent.transform.Find("Background/BPSUpgrade/DamageUpgradePrice").GetComponent<Text>().text = "$ " + actualMinigunBPSUpgradeCost.ToString();
    }

    public void SetHovers()
    {
        float x, y;
        x = (actualPistolDamageUpgradeCost > playerVal.playerCoins) ? SetActive(pistolDamageHover, pistolDamageUpgradeButton, true) : SetActive(pistolDamageHover, pistolDamageUpgradeButton, false);
        x = (actualPistolCriticUpgradeCost > playerVal.playerCoins) ? SetActive(pistolCriticHover, pistolCriticUpgradeButton, true) : SetActive(pistolCriticHover, pistolCriticUpgradeButton, false);
        x = (actualPistolBPSUpgradeCost > playerVal.playerCoins) ? SetActive(pistolBPSHover, pistolBPSUpgradeButton, true) : SetActive(pistolBPSHover, pistolBPSUpgradeButton, false);

        x = (actualMinigunDamageUpgradeCost > playerVal.playerCoins) ? SetActive(minigunDamageHover, minigunDamageUpgradeButton, true) : SetActive(minigunDamageHover, minigunDamageUpgradeButton, false);
        x = (actualMinigunCriticUpgradeCost > playerVal.playerCoins) ? SetActive(minigunCriticHover, minigunCriticUpgradeButton, true) : SetActive(minigunCriticHover, minigunCriticUpgradeButton, false);
        x = (actualMinigunBPSUpgradeCost > playerVal.playerCoins) ? SetActive(minigunBPSHover, minigunBPSUpgradeButton, true) : SetActive(minigunBPSHover, minigunBPSUpgradeButton, false);
        y = x;
        x = y;
        //print("Hovers Set");
    }

    private float SetActive(GameObject hover, Button button, bool state)
    {
        hover.SetActive(state);
        button.interactable = !state;
        return 0.0f;
    }

    public void PistolDamageUpgrade()
    {
        playerVal.UpdateValues("pistolDamage", 15);
        playerVal.playerCoins -= actualPistolDamageUpgradeCost;
        timesPistolDamageUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }

    public void PistolCriticalChanceUpgrade()
    {
        playerVal.UpdateValues("pistolCriticalShotChance", 1);
        playerVal.playerCoins -= actualPistolCriticUpgradeCost;
        timesPistolCriticUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }

    public void PistolBPSUpgrade()
    {
        playerVal.UpdateValues("pistolBPS", 0.1f);
        playerVal.playerCoins -= actualPistolBPSUpgradeCost;
        timesPistolBPSUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }

    public void MinigunDamageUpgrade()
    {
        playerVal.UpdateValues("minigunDamage", 20);
        playerVal.playerCoins -= actualMinigunDamageUpgradeCost;
        timesMinigunDamageUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }

    public void MinigunCriticalChanceUpgrade()
    {
        playerVal.UpdateValues("minigunCriticalShotChance", 1);
        playerVal.playerCoins -= actualMinigunCriticUpgradeCost;
        timesMinigunCriticUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }

    public void MinigunBPSUpgrade()
    {
        playerVal.UpdateValues("minigunBPS", 20);
        playerVal.playerCoins -= actualMinigunBPSUpgradeCost;
        timesMinigunBPSUpgraded += 1;
        UpdateValues();
        playerVal.UpdateCoinsValue(0);
        SetHovers();
    }
}
