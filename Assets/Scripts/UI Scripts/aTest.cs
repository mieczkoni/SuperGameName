using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aTest : MonoBehaviour
{
    public GameObject buyMinigunPrefab, pistolUpgradePrefab, minigunUpgradePrefab;

    private PlayerValues playerVal;
    private CanvasController canvasController;

    private int actualPistolDamageUpgradeCost, actualPistolCriticUpgradeCost, actualPistolBPSUpgradeCost;
    private int timesPistolDamageUpgraded = 0, timesPistolCriticUpgraded = 0, timesPistolBPSUpgraded = 0;
    private int actualMinigunDamageUpgradeCost, actualMinigunCriticUpgradeCost, actualMinigunBPSUpgradeCost;
    private int timesMinigunDamageUpgraded = 0, timesMinigunCriticUpgraded = 0, timesMinigunBPSUpgraded = 0;

    private List<GameObject> instantiatedPrefabs;
    private bool pistolInstantiated = false, minigunInstantiated = false, minigunBuyInstantiated = false;

    private Text pistolDamageText1, pistolDamageText2, pistolDamageText3;

    void Start()
    {
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();
        canvasController = GameObject.Find("MainCanvas").GetComponent<CanvasController>();
        instantiatedPrefabs = new List<GameObject>();
        UpdateUpgradeCosts();
    }

    public void InstantiateWeaponsPrefabs()
    {
        print(playerVal.weaponsOwned[0] + " " + playerVal.weaponsOwned[1]);
        if (playerVal.weaponsOwned[0] == true && pistolInstantiated == false)
        {
            GameObject newBar = Instantiate(pistolUpgradePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Pistol";
            UpdateButtonsValues(newBar.transform.name);
            instantiatedPrefabs.Add(newBar);
            pistolInstantiated = true;
        }
        if (playerVal.weaponsOwned[1] == true && minigunInstantiated == false)
        {
            GameObject newBar = Instantiate(minigunUpgradePrefab, new Vector3(0, -235, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Minigun";
            UpdateButtonsValues(newBar.transform.name);
            instantiatedPrefabs.Add(newBar);
            minigunInstantiated = true;
        }
        else if (playerVal.weaponsOwned[1] == false && minigunBuyInstantiated == false)
        {
            GameObject newBar = Instantiate(buyMinigunPrefab, new Vector3(0, -235, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "BuyMinigun";
            UpdateButtonsValues(newBar.transform.name);
            instantiatedPrefabs.Add(newBar);
            minigunBuyInstantiated = true;
        }
    }

    private void UpdateButtonsValues(string whichButton)
    {
       switch (whichButton)
        {
            case "Pistol":
                Button pistolButton1 = GameObject.Find("ContentContainer/Pistol/Background/1stButton").GetComponent<Button>();
                pistolButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = playerVal.pistolDamage.ToString();
                pistolButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.pistolDamage + 15).ToString();
                pistolButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolDamageUpgradeCost.ToString();
                if (actualPistolDamageUpgradeCost > playerVal.playerCoins) { pistolButton1.interactable = false; }
                else { pistolButton1.interactable = true; }
                Button pistolButton2 = GameObject.Find("ContentContainer/Pistol/Background/2ndButton").GetComponent<Button>();
                pistolButton2.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.pistolCriticalShotChance).ToString();
                pistolButton2.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)playerVal.pistolCriticalShotChance + 1).ToString();
                pistolButton2.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolCriticUpgradeCost.ToString();
                if (actualPistolCriticUpgradeCost > playerVal.playerCoins) { pistolButton2.interactable = false; }
                else { pistolButton2.interactable = true; }
                Button pistolButton3 = GameObject.Find("ContentContainer/Pistol/Background/3rdButton").GetComponent<Button>();
                pistolButton3.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.pistolBulletsPerSecond).ToString();
                pistolButton3.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)(playerVal.pistolBulletsPerSecond + 0.1f)).ToString();
                pistolButton3.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolBPSUpgradeCost.ToString();
                if (actualPistolBPSUpgradeCost > playerVal.playerCoins) { pistolButton3.interactable = false; }
                else { pistolButton3.interactable = true; }
                break;
            case "Minigun":
                Button minigunButton1 = GameObject.Find("ContentContainer/Minigun/Background/1stButton").GetComponent<Button>();
                minigunButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = playerVal.minigunDamage.ToString();
                minigunButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.minigunDamage + 15).ToString();
                minigunButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunDamageUpgradeCost.ToString();
                if (actualMinigunDamageUpgradeCost > playerVal.playerCoins) { minigunButton1.interactable = false; }
                else { minigunButton1.interactable = true; }
                Button minigunButton2 = GameObject.Find("ContentContainer/Minigun/Background/2ndButton").GetComponent<Button>();
                minigunButton2.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.minigunCriticalShotChance).ToString();
                minigunButton2.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)playerVal.minigunCriticalShotChance + 1).ToString();
                minigunButton2.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunCriticUpgradeCost.ToString();
                Button minigunButton3 = GameObject.Find("ContentContainer/Minigun/Background/3rdButton").GetComponent<Button>();
                if (actualMinigunCriticUpgradeCost > playerVal.playerCoins) { minigunButton2.interactable = false; }
                else { minigunButton2.interactable = true; }
                minigunButton3.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.minigunBulletsPerSecond).ToString();
                minigunButton3.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)(playerVal.minigunBulletsPerSecond + 0.1f)).ToString();
                minigunButton3.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunBPSUpgradeCost.ToString();
                if (actualMinigunBPSUpgradeCost > playerVal.playerCoins) { minigunButton3.interactable = false; }
                else { minigunButton3.interactable = true; }
                break;
            case "BuyMinigun":
                Button minigunButton11 = GameObject.Find("ontentContainer/Minigun/Background/1stButton").GetComponent<Button>();
                if (15000 > playerVal.playerCoins) { minigunButton11.interactable = false; }
                else { minigunButton11.interactable = true; }
                break;
        }
    }

    void BuyMinigun()
    {
        playerVal.SetBoolValues("Minigun", true);
        Destroy(instantiatedPrefabs[1]);
        minigunBuyInstantiated = false;
        InstantiateWeaponsPrefabs();
    }

    void UpdateUpgradeCosts()
    {
        actualPistolDamageUpgradeCost = 100 + (50 * timesPistolDamageUpgraded);
        actualPistolCriticUpgradeCost = 1000 + (80 * timesPistolCriticUpgraded);
        actualPistolBPSUpgradeCost = 3000 + (250 * timesPistolBPSUpgraded);
        actualMinigunDamageUpgradeCost = 500 + (100 * timesMinigunDamageUpgraded);
        actualMinigunCriticUpgradeCost = 5000 + (160 * timesMinigunCriticUpgraded);
        actualMinigunBPSUpgradeCost = 15000 + (1250 * timesMinigunBPSUpgraded);
    }

    public void PistolDamageUpgrade()
    {
        print("UPGRADE PISTOL DAMAGE");
        playerVal.UpdateValues("pistolDamage", 15);
        playerVal.UpdateCoinsValue(-actualPistolDamageUpgradeCost);
        timesPistolDamageUpgraded += 1;
        UpdateUpgradeCosts();
    }

    public void PistolCriticalChanceUpgrade()
    {
        print("UPGRADE PISTOL CRITICAL SHOT");
        playerVal.UpdateValues("pistolCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualPistolCriticUpgradeCost);
        timesPistolCriticUpgraded += 1;
        UpdateUpgradeCosts();
    }

    public void PistolBPSUpgrade()
    {
        print("UPGRADE PISTOL BPS");
        playerVal.UpdateValues("pistolBPS", 0.1f);
        playerVal.UpdateCoinsValue(-actualPistolBPSUpgradeCost);
        timesPistolBPSUpgraded += 1;
        UpdateUpgradeCosts();
    }

    public void MinigunDamageUpgrade()
    {
        print("UPGRADE MINIGUN DAMAGE");
        playerVal.UpdateValues("minigunDamage", 20);
        playerVal.UpdateCoinsValue(-actualMinigunDamageUpgradeCost);
        timesMinigunDamageUpgraded += 1;
        UpdateUpgradeCosts();
    }

    public void MinigunCriticalChanceUpgrade()
    {
        print("UPGRADE MINIGUN CRITICAL SHOT");
        playerVal.UpdateValues("minigunCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualMinigunCriticUpgradeCost);
        timesMinigunCriticUpgraded += 1;
        UpdateUpgradeCosts();
    }

    public void MinigunBPSUpgrade()
    {
        print("UPGRADE MINIGUN BPS");
        playerVal.UpdateValues("minigunBPS", 20);
        playerVal.UpdateCoinsValue(-actualMinigunBPSUpgradeCost);
        timesMinigunBPSUpgraded += 1;
        UpdateUpgradeCosts();
    }
}