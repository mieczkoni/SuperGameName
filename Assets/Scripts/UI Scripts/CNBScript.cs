using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CNBScript : MonoBehaviour
{
    private bool weaponsCNBActive = false, skillsCNBActive = false;
    
    public GameObject buyMinigunPrefab, pistolUpgradePrefab, minigunUpgradePrefab;
    public GameObject shieldPrefab;
    public Sprite pistolSprite, minigunSprite;

    private PlayerValues playerVal;
    private CanvasController canvasController;

    private int actualPistolDamageUpgradeCost, actualPistolCriticUpgradeCost, actualPistolBPSUpgradeCost;
    private int timesPistolDamageUpgraded = 0, timesPistolCriticUpgraded = 0, timesPistolBPSUpgraded = 0;
    private int actualMinigunDamageUpgradeCost, actualMinigunCriticUpgradeCost, actualMinigunBPSUpgradeCost;
    private int timesMinigunDamageUpgraded = 0, timesMinigunCriticUpgraded = 0, timesMinigunBPSUpgraded = 0;
    private int actualShieldTimeUpgradeCost;
    private int timesShieldTimeUpgraded;

    private List<GameObject> instantiatedPrefabs;
    private bool pistolInstantiated = false, minigunInstantiated = false, minigunBuyInstantiated = false;
    private bool pistolButtonsSet = false, minigunButtonsSet = false, minigunBuyButtonsSet = false;

    private bool shieldInstantiated = false;
    private bool shieldButtonsSet = false;

    private Text pistolDamageText1, pistolDamageText2, pistolDamageText3;

    void Start()
    {
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();
        canvasController = GameObject.Find("MainCanvas").GetComponent<CanvasController>();
        instantiatedPrefabs = new List<GameObject>();
    }

    public void DestroyPrefabs()
    {
        if (instantiatedPrefabs.Count > 0)
        {
            foreach (GameObject prefab in instantiatedPrefabs)
            {
                //print("DESTROY " + prefab.transform.name);
                Destroy(prefab);
            }
        }
        pistolInstantiated = false;
        minigunInstantiated = false;
        minigunBuyInstantiated = false;
        shieldInstantiated = false;
        weaponsCNBActive = false;
        skillsCNBActive = false;
        pistolButtonsSet = false;
        minigunButtonsSet = false;
        minigunBuyButtonsSet = false;
        shieldButtonsSet = false;
    }

    public void InstantiateSkillsPrefabs()
    {
        print("ROBING SKILSÓW");
        skillsCNBActive = true;
        weaponsCNBActive = false;
        if (playerVal.skillsOwned[0] == true || playerVal.skillsOwned[0] == false)
        {
            GameObject newBar = Instantiate(shieldPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Shield";
            instantiatedPrefabs.Add(newBar);
            shieldInstantiated = true;
        }
    }

    public void InstantiateWeaponsPrefabs()
    {
        print("ROBING WEAPONSÓW");
        skillsCNBActive = false;
        weaponsCNBActive = true;
        print(playerVal.weaponsOwned[0] + " " + playerVal.weaponsOwned[1]);
        if (playerVal.weaponsOwned[0] == true && pistolInstantiated == false)
        {
            GameObject newBar = Instantiate(pistolUpgradePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Pistol";
            instantiatedPrefabs.Add(newBar);
            pistolInstantiated = true;
        }
        if (playerVal.weaponsOwned[1] == true && minigunInstantiated == false)
        {
            GameObject newBar = Instantiate(minigunUpgradePrefab, new Vector3(0, -235, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Minigun";
            instantiatedPrefabs.Add(newBar);
            minigunInstantiated = true;
        }
        else if (playerVal.weaponsOwned[1] == false && minigunBuyInstantiated == false)
        {
            GameObject newBar = Instantiate(buyMinigunPrefab, new Vector3(0, -235, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "BuyMinigun";
            instantiatedPrefabs.Add(newBar);
            minigunBuyInstantiated = true;
        }
        UpdateButtonsValues();
        SetOnClickFunctions();
    }

    private void SetOnClickFunctions()
    {
        if (weaponsCNBActive == true)
        {
            if (playerVal.weaponsOwned[0] == true && pistolButtonsSet == false)
            {
                GameObject.Find("ContentContainer/Pistol/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { PistolDamageUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/2ndButton").GetComponent<Button>().onClick.AddListener(delegate { PistolCriticalChanceUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/3rdButton").GetComponent<Button>().onClick.AddListener(delegate { PistolBPSUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetPistol(); });
                pistolButtonsSet = true;
            }
            if (playerVal.weaponsOwned[1] == true && minigunButtonsSet == false)
            {
                GameObject.Find("ContentContainer/Minigun/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunDamageUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/2ndButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunCriticalChanceUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/3rdButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunBPSUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetMinigun(); });
                minigunButtonsSet = true;
            }
            else if (playerVal.weaponsOwned[1] == false && minigunBuyButtonsSet == false)
            {
                GameObject.Find("ContentContainer/BuyMinigun/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { BuyMinigun(); });
                minigunBuyButtonsSet = true;
            }
        }
        
        if (skillsCNBActive == true)
        {
            if (playerVal.skillsOwned[0] == true && shieldButtonsSet == false)
            {
                GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { ShieldTimeUpgrade(); });
                GameObject.Find("ContentContainer/Shield/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetShield(); });
                shieldButtonsSet = true;
            }
            else if (playerVal.skillsOwned[0] == false && shieldButtonsSet == false)
            {
                GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { BuyShield(); });
                shieldButtonsSet = true;
            }
        }
    }

    private void UpdateButtonsValues()
    {
        // BRONIE
        if (weaponsCNBActive == true)
        {
            if (playerVal.weaponsOwned[0] == true && pistolInstantiated == true)
            {
                actualPistolDamageUpgradeCost = 100 + (50 * timesPistolDamageUpgraded);
                actualPistolCriticUpgradeCost = 1000 + (80 * timesPistolCriticUpgraded);
                actualPistolBPSUpgradeCost = 3000 + (250 * timesPistolBPSUpgraded);
                Button pistolButton1 = GameObject.Find("ContentContainer/Pistol/Background/1stButton").GetComponent<Button>();
                pistolButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = playerVal.pistolDamage.ToString();
                pistolButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.pistolDamage + 15).ToString();
                pistolButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolDamageUpgradeCost.ToString() + "$";
                if (actualPistolDamageUpgradeCost > playerVal.playerCoins) { pistolButton1.interactable = false; }
                else { pistolButton1.interactable = true; }
                Button pistolButton2 = GameObject.Find("ContentContainer/Pistol/Background/2ndButton").GetComponent<Button>();
                pistolButton2.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.pistolCriticalShotChance).ToString() + "%";
                pistolButton2.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)playerVal.pistolCriticalShotChance + 1).ToString() + "%";
                pistolButton2.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolCriticUpgradeCost.ToString() + "$";
                if (actualPistolCriticUpgradeCost > playerVal.playerCoins) { pistolButton2.interactable = false; }
                else { pistolButton2.interactable = true; }
                Button pistolButton3 = GameObject.Find("ContentContainer/Pistol/Background/3rdButton").GetComponent<Button>();
                pistolButton3.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.pistolBulletsPerSecond).ToString("F1");
                pistolButton3.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((playerVal.pistolBulletsPerSecond + 0.1f)).ToString("F1");
                pistolButton3.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualPistolBPSUpgradeCost.ToString() + "$";
                if (actualPistolBPSUpgradeCost > playerVal.playerCoins) { pistolButton3.interactable = false; }
                else { pistolButton3.interactable = true; }
                GameObject.Find("ContentContainer/Pistol/Background/Icon").GetComponent<Button>().interactable = true;

            }
            if (playerVal.weaponsOwned[1] == true && minigunInstantiated == true)
            {
                actualMinigunDamageUpgradeCost = 500 + (100 * timesMinigunDamageUpgraded);
                actualMinigunCriticUpgradeCost = 5000 + (160 * timesMinigunCriticUpgraded);
                actualMinigunBPSUpgradeCost = 15000 + (1250 * timesMinigunBPSUpgraded);
                Button minigunButton1 = GameObject.Find("ContentContainer/Minigun/Background/1stButton").GetComponent<Button>();
                minigunButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = playerVal.minigunDamage.ToString();
                minigunButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.minigunDamage + 15).ToString();
                minigunButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunDamageUpgradeCost.ToString() + "$";
                if (actualMinigunDamageUpgradeCost > playerVal.playerCoins) { minigunButton1.interactable = false; }
                else { minigunButton1.interactable = true; }
                Button minigunButton2 = GameObject.Find("ContentContainer/Minigun/Background/2ndButton").GetComponent<Button>();
                minigunButton2.transform.Find("ButtonActualValue").GetComponent<Text>().text = ((int)playerVal.minigunCriticalShotChance).ToString() + "%";
                minigunButton2.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((int)playerVal.minigunCriticalShotChance + 1).ToString() + "%";
                minigunButton2.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunCriticUpgradeCost.ToString() + "$";
                Button minigunButton3 = GameObject.Find("ContentContainer/Minigun/Background/3rdButton").GetComponent<Button>();
                if (actualMinigunCriticUpgradeCost > playerVal.playerCoins) { minigunButton2.interactable = false; }
                else { minigunButton2.interactable = true; }
                minigunButton3.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.minigunBulletsPerSecond).ToString("F1");
                minigunButton3.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = ((playerVal.minigunBulletsPerSecond + 0.1f)).ToString("F1");
                minigunButton3.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualMinigunBPSUpgradeCost.ToString() + "$";
                if (actualMinigunBPSUpgradeCost > playerVal.playerCoins) { minigunButton3.interactable = false; }
                else { minigunButton3.interactable = true; }
                GameObject.Find("ContentContainer/Minigun/Background/Icon").GetComponent<Button>().interactable = true;
            }
            else if (playerVal.weaponsOwned[1] == false && minigunBuyInstantiated == true)
            {
                Button minigunButton11 = GameObject.Find("ContentContainer/BuyMinigun/Background/1stButton").GetComponent<Button>();
                if (15000 > playerVal.playerCoins) { minigunButton11.interactable = false; }
                else { minigunButton11.interactable = true; }
                GameObject.Find("ContentContainer/BuyMinigun/Background/Icon").GetComponent<Button>().interactable = false;
            }
        }

        // SKILSY
        if (skillsCNBActive == true)
        {
            if (playerVal.skillsOwned[0] == true && shieldInstantiated == true)
            {
                actualShieldTimeUpgradeCost = 10000 + (10000 * timesShieldTimeUpgraded);
                Button shieldButton1 = GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>();
                shieldButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.shieldTime).ToString("F1");
                shieldButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.shieldTime + 0.5f).ToString("F1");
                shieldButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualShieldTimeUpgradeCost.ToString() + "$";
                if (actualShieldTimeUpgradeCost > playerVal.playerCoins) { shieldButton1.interactable = false; }
                else { shieldButton1.interactable = true; }
                GameObject.Find("ContentContainer/Icon/Background/1stButton").GetComponent<Button>().interactable = true;
            }
            else if (playerVal.skillsOwned[0] == false && shieldInstantiated == true)
            {
                Button shieldButton1 = GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>();
                if (1000000 > playerVal.playerCoins) { shieldButton1.interactable = false; }
                else { shieldButton1.interactable = true; }
                GameObject.Find("ContentContainer/Icon/Background/1stButton").GetComponent<Button>().interactable = false;
            }
        }
        
    }

    private void BuyShield()
    {
        print("BUY SHIELD");
    }

    private void BuyMinigun()
    {
        playerVal.SetBoolValues("Minigun", true);
        Destroy(instantiatedPrefabs[1]);
        minigunBuyInstantiated = false;
        InstantiateWeaponsPrefabs();
    }

    public void ShieldTimeUpgrade()
    {
        print("UPGRADE SHIELD TIME");
    }

    public void PistolDamageUpgrade()
    {
        print("UPGRADE PISTOL DAMAGE");
        playerVal.UpdateValues("pistolDamage", 15);
        playerVal.UpdateCoinsValue(-actualPistolDamageUpgradeCost);
        timesPistolDamageUpgraded += 1;
        UpdateButtonsValues();
    }

    public void PistolCriticalChanceUpgrade()
    {
        print("UPGRADE PISTOL CRITICAL SHOT");
        playerVal.UpdateValues("pistolCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualPistolCriticUpgradeCost);
        timesPistolCriticUpgraded += 1;
        UpdateButtonsValues();
    }

    public void PistolBPSUpgrade()
    {
        print("UPGRADE PISTOL BPS");
        playerVal.UpdateValues("pistolBPS", 0.1f);
        playerVal.UpdateCoinsValue(-actualPistolBPSUpgradeCost);
        timesPistolBPSUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunDamageUpgrade()
    {
        print("UPGRADE MINIGUN DAMAGE");
        playerVal.UpdateValues("minigunDamage", 20);
        playerVal.UpdateCoinsValue(-actualMinigunDamageUpgradeCost);
        timesMinigunDamageUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunCriticalChanceUpgrade()
    {
        print("UPGRADE MINIGUN CRITICAL SHOT");
        playerVal.UpdateValues("minigunCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualMinigunCriticUpgradeCost);
        timesMinigunCriticUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunBPSUpgrade()
    {
        print("UPGRADE MINIGUN BPS");
        playerVal.UpdateValues("minigunBPS", 0.1f);
        playerVal.UpdateCoinsValue(-actualMinigunBPSUpgradeCost);
        timesMinigunBPSUpgraded += 1;
        UpdateButtonsValues();
    }

    public void SetShield()
    {
        print("SET SHIELD");
    }

    public void SetPistol()
    {
        print("SET PISTOL");
        GameObject.Find("Background/MainWeapon").GetComponent<Image>().sprite = pistolSprite;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().gunType = "Pistol";
    }

    public void SetMinigun()
    {
        print("SET MINIGUN");
        GameObject.Find("Background/MainWeapon").GetComponent<Image>().sprite = minigunSprite;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().gunType = "Minigun";
    }
}