using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CNBScript : MonoBehaviour
{
    public bool weaponsCNBActive = false, skillsCNBActive = false, secondaryWeaponsCNBActive = false;

    public Sprite redBackground, blueBackground, greenBackground;

    public GameObject buyMinigunPrefab, pistolUpgradePrefab, minigunUpgradePrefab;
    public GameObject shieldPrefab;
    public GameObject buyGranadePrefab, granadePrefab;
    public Sprite pistolSprite, minigunSprite;
    public Sprite shieldSprite;
    public Sprite granadeSprite;

    private GameObject player;
    private PlayerValues playerVal;
    private SecondaryWeaponController secondaryWeaponController;
    private SkillsController skillsController;

    private int actualPistolDamageUpgradeCost, actualPistolCriticUpgradeCost, actualPistolBPSUpgradeCost;
    private int timesPistolDamageUpgraded = 0, timesPistolCriticUpgraded = 0, timesPistolBPSUpgraded = 0;
    private int actualMinigunDamageUpgradeCost, actualMinigunCriticUpgradeCost, actualMinigunBPSUpgradeCost;
    private int timesMinigunDamageUpgraded = 0, timesMinigunCriticUpgraded = 0, timesMinigunBPSUpgraded = 0;
    private int actualShieldTimeUpgradeCost;
    private int timesShieldTimeUpgraded;
    private int actualGranadeDamageUpgradeCost, actualGranadeRangeUpgradeCost;
    private int timesGranadeDamageUpgraded = 0, timesGranadeRangeUpgraded = 0;

    private List<GameObject> instantiatedPrefabs;
    private bool pistolInstantiated = false, minigunInstantiated = false, minigunBuyInstantiated = false;
    private bool pistolButtonsSet = false, minigunButtonsSet = false, minigunBuyButtonsSet = false;
    
    private bool shieldInstantiated = false, shieldBuyInstantiated = false;
    private bool shieldButtonsSet = false, shieldBuyButtonsSet = false;

    private bool granadeInstantiated = false, granadeBuyInstantiated = false;
    private bool granadeButtonsSet = false, granadeBuyButtonsSet = false;

    private Image navigationBarImage;
    private Text cnbDescriptionText, chooseAndBuyText;

    void Start()
    {
        player = GameObject.Find("Player");
        playerVal = player.GetComponent<PlayerValues>();
        instantiatedPrefabs = new List<GameObject>();
        navigationBarImage = GameObject.Find("NavigationBar").GetComponent<Image>();
        cnbDescriptionText = GameObject.Find("NavigationBar/CNBDescription").GetComponent<Text>();
        chooseAndBuyText = GameObject.Find("NavigationBar/ChooseAndBuyText").GetComponent<Text>();
        secondaryWeaponController = player.GetComponent<SecondaryWeaponController>();
        skillsController = player.GetComponent<SkillsController>();
        GetAllData();
    }

    public void DestroyPrefabs()
    {
        if (instantiatedPrefabs.Count > 0)
        {
            foreach (GameObject prefab in instantiatedPrefabs)
            {
                Destroy(prefab);
            }
        }
        pistolInstantiated = false;
        minigunInstantiated = false;
        minigunBuyInstantiated = false;
        shieldInstantiated = false;
        shieldBuyInstantiated = false;
        granadeInstantiated = false;
        granadeBuyInstantiated = false;

        weaponsCNBActive = false;
        skillsCNBActive = false;
        secondaryWeaponsCNBActive = false;

        pistolButtonsSet = false;
        minigunButtonsSet = false;
        minigunBuyButtonsSet = false;
        shieldButtonsSet = false;
        shieldBuyButtonsSet = false;
        granadeButtonsSet = false;
        granadeBuyButtonsSet = false;
    }

    public void InstantiateSecondaryWeaponPrefabs()
    {
        chooseAndBuyText.text = "Manage your secondary weapon!";
        cnbDescriptionText.text = "Choose your secondary weapon that can help you in hard times. Upgrade or buy new weapons by spending your coins. \n * Click the weapon icon to choose it as your secondary weapon.";
        navigationBarImage.sprite = greenBackground;
        skillsCNBActive = false;
        weaponsCNBActive = false;
        secondaryWeaponsCNBActive = true;
        if (playerVal.granadeOwned && !granadeInstantiated)
        {
            GameObject newBar = Instantiate(granadePrefab, new Vector3(0, 85, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Granade";
            instantiatedPrefabs.Add(newBar);
            granadeInstantiated = true;
        } else if (!playerVal.granadeOwned && !granadeBuyInstantiated)
        {
            GameObject newBar = Instantiate(buyGranadePrefab, new Vector3(0, 85, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "BuyGranade";
            instantiatedPrefabs.Add(newBar);
            granadeBuyInstantiated = true;
        }
        UpdateButtonsValues();
        SetOnClickFunctions();
    }

    public void InstantiateSkillsPrefabs()
    {
        chooseAndBuyText.text = "Manage your skills!";
        cnbDescriptionText.text = "Choose skills to use in the battle or buy new ones. You'll need some Skill Points to learn new skill or upgrade it. \n * Click the skill icon to choose it as your main skill. \n Your Skill Points: " + playerVal.skillPoints;
        navigationBarImage.sprite = blueBackground;             
        skillsCNBActive = true;
        weaponsCNBActive = false;
        secondaryWeaponsCNBActive = false;
        if (playerVal.shieldOwned)
        {
            GameObject newBar = Instantiate(shieldPrefab, new Vector3(0, 85, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Shield";
            instantiatedPrefabs.Add(newBar);
            shieldInstantiated = true;
        } else if (!playerVal.shieldOwned)
        {
            GameObject newBar = Instantiate(shieldPrefab, new Vector3(0, 85, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "BuyShield";
            instantiatedPrefabs.Add(newBar);
            shieldBuyInstantiated = true;
        }
        UpdateButtonsValues();
        SetOnClickFunctions();
    }

    public void InstantiateWeaponsPrefabs()
    {
        chooseAndBuyText.text = "Manage your weapons!";
        cnbDescriptionText.text = "You can choose here which weapon you'll need in the battle. Use your coins to buy new weapons or upgrade already owned. \n * Click the weapon icon to choose main weapon.";
        navigationBarImage.sprite = redBackground;
        skillsCNBActive = false;
        weaponsCNBActive = true;
        secondaryWeaponsCNBActive = false;
        if (playerVal.pistolOwned && !pistolInstantiated)
        {
            GameObject newBar = Instantiate(pistolUpgradePrefab, new Vector3(0, 85, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Pistol";
            instantiatedPrefabs.Add(newBar);
            pistolInstantiated = true;
        }
        if (playerVal.minigunOwned && !minigunInstantiated)
        {
            GameObject newBar = Instantiate(minigunUpgradePrefab, new Vector3(0, -150, 0), Quaternion.identity);
            newBar.transform.SetParent(GameObject.Find("ShopContainer/Weapons/ContentContainer").transform, false);
            newBar.transform.name = "Minigun";
            instantiatedPrefabs.Add(newBar);
            minigunInstantiated = true;
        }
        else if (!playerVal.minigunOwned && !minigunBuyInstantiated)
        {
            GameObject newBar = Instantiate(buyMinigunPrefab, new Vector3(0, -150, 0), Quaternion.identity);
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
            if (playerVal.pistolOwned && !pistolButtonsSet)
            {
                GameObject.Find("ContentContainer/Pistol/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { PistolDamageUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/2ndButton").GetComponent<Button>().onClick.AddListener(delegate { PistolCriticalChanceUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/3rdButton").GetComponent<Button>().onClick.AddListener(delegate { PistolBPSUpgrade(); });
                GameObject.Find("ContentContainer/Pistol/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetPistol(); });
                pistolButtonsSet = true;
            }
            if (playerVal.minigunOwned && !minigunButtonsSet)
            {
                GameObject.Find("ContentContainer/Minigun/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunDamageUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/2ndButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunCriticalChanceUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/3rdButton").GetComponent<Button>().onClick.AddListener(delegate { MinigunBPSUpgrade(); });
                GameObject.Find("ContentContainer/Minigun/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetMinigun(); });
                minigunButtonsSet = true;
            }
            else if (!playerVal.minigunOwned && !minigunBuyButtonsSet)
            {
                GameObject.Find("ContentContainer/BuyMinigun/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { BuyMinigun(); });
                minigunBuyButtonsSet = true;
            }
        }
        
        if (skillsCNBActive == true)
        {
            if (playerVal.shieldOwned && !shieldButtonsSet)
            {
                GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { ShieldTimeUpgrade(); });
                GameObject.Find("ContentContainer/Shield/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetShield(); });
                shieldButtonsSet = true;
            }
            else if (!playerVal.shieldOwned && !shieldBuyButtonsSet)
            {
                GameObject.Find("ContentContainer/BuyShield/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { BuyShield(); });
                shieldBuyButtonsSet = true;
            }
        }

        if (secondaryWeaponsCNBActive == true)
        {
            if (playerVal.granadeOwned && !granadeButtonsSet)
            {
                GameObject.Find("ContentContainer/Granade/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { GranadeDamageUpgrade(); });
                GameObject.Find("ContentContainer/Granade/Background/2ndButton").GetComponent<Button>().onClick.AddListener(delegate { GranadeRangeUpgrade(); });
                GameObject.Find("ContentContainer/Granade/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetGranade(); });
                granadeButtonsSet = true;
            }
            else if (!playerVal.granadeOwned && !granadeBuyButtonsSet)
            {
                GameObject.Find("ContentContainer/BuyGranade/Background/1stButton").GetComponent<Button>().onClick.AddListener(delegate { BuyGranade(); });
                GameObject.Find("ContentContainer/BuyGranade/Background/Icon").GetComponent<Button>().onClick.AddListener(delegate { SetGranade(); });
                granadeBuyButtonsSet = true;
            }
        }
    }

    private void UpdateButtonsValues()
    {
        // BRONIE
        if (weaponsCNBActive == true)
        {
            if (playerVal.pistolOwned && pistolInstantiated)
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
            if (playerVal.minigunOwned && minigunInstantiated)
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
            else if (!playerVal.minigunOwned && minigunBuyInstantiated)
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
            if (playerVal.shieldOwned && shieldInstantiated)
            {
                actualShieldTimeUpgradeCost = 1 + (1 * timesShieldTimeUpgraded);
                Button shieldButton1 = GameObject.Find("ContentContainer/Shield/Background/1stButton").GetComponent<Button>();
                shieldButton1.transform.Find("ButtonText").GetComponent<Text>().text = "WORKING TIME";
                shieldButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.shieldTime).ToString("F1");
                shieldButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.shieldTime + 0.5f).ToString("F1");
                shieldButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualShieldTimeUpgradeCost.ToString() + "SP";
                if (actualShieldTimeUpgradeCost > playerVal.skillPoints) { shieldButton1.interactable = false; }
                else { shieldButton1.interactable = true; }
                GameObject.Find("ContentContainer/Shield/Background/Icon").GetComponent<Button>().interactable = true;
            }
            else if (!playerVal.shieldOwned && shieldBuyInstantiated)
            {
                Button shieldButton1 = GameObject.Find("ContentContainer/BuyShield/Background/1stButton").GetComponent<Button>();
                shieldButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = "10 SP";
                if (10 > playerVal.skillPoints) { shieldButton1.interactable = false; }
                else { shieldButton1.interactable = true; }
                GameObject.Find("ContentContainer/BuyShield/Background/Icon").GetComponent<Button>().interactable = false;
            }
        }

        // SECONDARY BRONIE
        if (secondaryWeaponsCNBActive == true)
        {
            if (playerVal.granadeOwned && granadeInstantiated)
            {
                actualGranadeDamageUpgradeCost = 24000 + (1450 * timesGranadeDamageUpgraded);
                actualGranadeRangeUpgradeCost = 56000 + (2680 * timesGranadeRangeUpgraded);
                Button granadeButton1 = GameObject.Find("ContentContainer/Granade/Background/1stButton").GetComponent<Button>();
                granadeButton1.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.granadeDamage).ToString();
                granadeButton1.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.granadeDamage + 50).ToString();
                granadeButton1.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualGranadeDamageUpgradeCost.ToString() + "$";
                if (actualGranadeDamageUpgradeCost > playerVal.playerCoins) { granadeButton1.interactable = false; }
                else { granadeButton1.interactable = true; }
                Button granadeButton2 = GameObject.Find("ContentContainer/Granade/Background/2ndButton").GetComponent<Button>();
                granadeButton2.transform.Find("ButtonActualValue").GetComponent<Text>().text = (playerVal.granadeRange).ToString();
                granadeButton2.transform.Find("ButtonUpgradedValue").GetComponent<Text>().text = (playerVal.granadeRange + 1).ToString();
                granadeButton2.transform.Find("ButtonUpgradePrice").GetComponent<Text>().text = actualGranadeRangeUpgradeCost.ToString() + "$";
                if (actualGranadeRangeUpgradeCost > playerVal.playerCoins) { granadeButton2.interactable = false; } 
                else { granadeButton2.interactable = true; }
                GameObject.Find("ContentContainer/Granade/Background/Icon").GetComponent<Button>().interactable = true;
            }
            else if (!playerVal.granadeOwned && granadeBuyInstantiated)
            {
                Button granadeBuyButton = GameObject.Find("ContentContainer/BuyGranade/Background/1stButton").GetComponent<Button>();
                if (80000 > playerVal.playerCoins) { granadeBuyButton.interactable = false; }
                else { granadeBuyButton.interactable = true; }
                GameObject.Find("ContentContainer/BuyGranade/Background/Icon").GetComponent<Button>().interactable = false;
            }
        }
        SaveCNBData();
    }

    private void BuyGranade()
    {
        playerVal.SetBoolValues("Granade", true);
        playerVal.UpdateCoinsValue(-80000);
        Destroy(instantiatedPrefabs[0]);
        granadeBuyInstantiated = false;
        granadeBuyButtonsSet = false;
        InstantiateSecondaryWeaponPrefabs();
    }

    private void BuyShield()
    {
        playerVal.SetBoolValues("Shield", true);
        playerVal.UpdateSkillPointsValue(10);
        shieldBuyInstantiated = false;
        shieldBuyButtonsSet = false;
        InstantiateSkillsPrefabs();
    }

    private void BuyMinigun()
    {
        playerVal.SetBoolValues("Minigun", true);
        playerVal.UpdateCoinsValue(-15000);
        Destroy(instantiatedPrefabs[1]);
        minigunBuyInstantiated = false;
        minigunBuyButtonsSet = false;
        InstantiateWeaponsPrefabs();
    }

    private void GranadeDamageUpgrade()
    {
        playerVal.UpdateValues("granadeDamage", 50);
        playerVal.UpdateCoinsValue(-actualGranadeDamageUpgradeCost);
        timesGranadeDamageUpgraded += 1;
        UpdateButtonsValues();
    }

    private void GranadeRangeUpgrade()
    {
        playerVal.UpdateValues("granadeRange", 1);
        playerVal.UpdateCoinsValue(-actualGranadeRangeUpgradeCost);
        timesGranadeRangeUpgraded += 1;
        UpdateButtonsValues();
    }

    public void ShieldTimeUpgrade()
    {
        playerVal.UpdateValues("shieldTime", 0.5f);
        playerVal.UpdateSkillPointsValue(actualShieldTimeUpgradeCost);
        timesShieldTimeUpgraded += 1;
        cnbDescriptionText.text = "Choose skills to use in the battle or buy new ones. You'll need some Skill Points to learn new skill or upgrade it. \n * Click the skill icon to choose it as your main skill. \n Your Skill Points: " + playerVal.skillPoints;
        UpdateButtonsValues();
    }

    public void PistolDamageUpgrade()
    {
        playerVal.UpdateValues("pistolDamage", 15);
        playerVal.UpdateCoinsValue(-actualPistolDamageUpgradeCost);
        timesPistolDamageUpgraded += 1;
        UpdateButtonsValues();
    }

    public void PistolCriticalChanceUpgrade()
    {
        playerVal.UpdateValues("pistolCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualPistolCriticUpgradeCost);
        timesPistolCriticUpgraded += 1;
        UpdateButtonsValues();
    }

    public void PistolBPSUpgrade()
    {
        playerVal.UpdateValues("pistolBPS", 0.1f);
        playerVal.UpdateCoinsValue(-actualPistolBPSUpgradeCost);
        timesPistolBPSUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunDamageUpgrade()
    {
        playerVal.UpdateValues("minigunDamage", 20);
        playerVal.UpdateCoinsValue(-actualMinigunDamageUpgradeCost);
        timesMinigunDamageUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunCriticalChanceUpgrade()
    {
        playerVal.UpdateValues("minigunCriticalShotChance", 1);
        playerVal.UpdateCoinsValue(-actualMinigunCriticUpgradeCost);
        timesMinigunCriticUpgraded += 1;
        UpdateButtonsValues();
    }

    public void MinigunBPSUpgrade()
    {
        playerVal.UpdateValues("minigunBPS", 0.1f);
        playerVal.UpdateCoinsValue(-actualMinigunBPSUpgradeCost);
        timesMinigunBPSUpgraded += 1;
        UpdateButtonsValues();
    }

    public void SetGranade()
    {
        GameObject.Find("Background/ThirdWeapon").GetComponent<Image>().sprite = granadeSprite;
        secondaryWeaponController.weaponType = "Granade";        
    }

    public void SetShield()
    {
        GameObject.Find("Background/SecondWeapon").GetComponent<Image>().sprite = shieldSprite;
        skillsController.skillName = "Shield";
    }

    public void SetPistol()
    {
        GameObject.Find("Background/MainWeapon").GetComponent<Image>().sprite = pistolSprite;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().gunType = "Pistol";
    }

    public void SetMinigun()
    {
        GameObject.Find("Background/MainWeapon").GetComponent<Image>().sprite = minigunSprite;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().gunType = "Minigun";
    }

    private void SaveCNBData()
    {
        PlayerPrefs.SetInt("actualPistolDamageUpgradeCost", actualPistolDamageUpgradeCost);
        PlayerPrefs.SetInt("actualPistolCriticUpgradeCost", actualPistolCriticUpgradeCost);
        PlayerPrefs.SetInt("actualPistolBPSUpgradeCost", actualPistolBPSUpgradeCost);
        PlayerPrefs.SetInt("timesPistolDamageUpgraded", timesPistolDamageUpgraded);
        PlayerPrefs.SetInt("timesPistolCriticUpgraded", timesPistolCriticUpgraded);
        PlayerPrefs.SetInt("timesPistolBPSUpgraded", timesPistolBPSUpgraded);
        PlayerPrefs.SetInt("actualMinigunDamageUpgradeCost", actualMinigunDamageUpgradeCost);
        PlayerPrefs.SetInt("actualMinigunCriticUpgradeCost", actualMinigunCriticUpgradeCost);
        PlayerPrefs.SetInt("actualMinigunBPSUpgradeCost", actualMinigunBPSUpgradeCost);
        PlayerPrefs.SetInt("timesMinigunDamageUpgraded", timesMinigunDamageUpgraded);
        PlayerPrefs.SetInt("timesMinigunCriticUpgraded", timesMinigunCriticUpgraded);
        PlayerPrefs.SetInt("timesMinigunBPSUpgraded", timesMinigunBPSUpgraded);
        PlayerPrefs.SetInt("actualShieldTimeUpgradeCost", actualShieldTimeUpgradeCost);
        PlayerPrefs.SetInt("timesShieldTimeUpgraded", timesShieldTimeUpgraded);
        PlayerPrefs.SetInt("actualGranadeDamageUpgradeCost", actualGranadeDamageUpgradeCost);
        PlayerPrefs.SetInt("actualGranadeRangeUpgradeCost", actualGranadeRangeUpgradeCost);
        PlayerPrefs.SetInt("timesGranadeDamageUpgraded", timesGranadeDamageUpgraded);
        PlayerPrefs.SetInt("timesGranadeRangeUpgraded", timesGranadeRangeUpgraded);
        PlayerPrefs.Save();
    }

    private void GetAllData()
    {
        actualPistolDamageUpgradeCost = PlayerPrefs.GetInt("actualPistolDamageUpgradeCost", 100);
        actualPistolCriticUpgradeCost = PlayerPrefs.GetInt("actualPistolCriticUpgradeCost", 1000);
        actualPistolBPSUpgradeCost = PlayerPrefs.GetInt("actualPistolBPSUpgradeCost", 3000);
        timesPistolDamageUpgraded = PlayerPrefs.GetInt("timesPistolDamageUpgraded");
        timesPistolCriticUpgraded = PlayerPrefs.GetInt("timesPistolCriticUpgraded");
        timesPistolBPSUpgraded = PlayerPrefs.GetInt("timesPistolBPSUpgraded");
        actualMinigunDamageUpgradeCost = PlayerPrefs.GetInt("actualMinigunDamageUpgradeCost", 500);
        actualMinigunCriticUpgradeCost = PlayerPrefs.GetInt("actualMinigunCriticUpgradeCost", 5000);
        actualMinigunBPSUpgradeCost = PlayerPrefs.GetInt("actualMinigunBPSUpgradeCost", 15000);
        timesMinigunDamageUpgraded = PlayerPrefs.GetInt("timesMinigunDamageUpgraded");
        timesMinigunCriticUpgraded = PlayerPrefs.GetInt("timesMinigunCriticUpgraded");
        timesMinigunBPSUpgraded = PlayerPrefs.GetInt("timesMinigunBPSUpgraded");
        actualShieldTimeUpgradeCost = PlayerPrefs.GetInt("actualShieldTimeUpgradeCost", 1);
        timesShieldTimeUpgraded = PlayerPrefs.GetInt("timesShieldTimeUpgraded");
        actualGranadeDamageUpgradeCost = PlayerPrefs.GetInt("actualGranadeDamageUpgradeCost", 24000);
        actualGranadeRangeUpgradeCost = PlayerPrefs.GetInt("actualGranadeRangeUpgradeCost", 56000);
        timesGranadeDamageUpgraded = PlayerPrefs.GetInt("timesGranadeDamageUpgraded");
        timesGranadeRangeUpgraded = PlayerPrefs.GetInt("timesGranadeRangeUpgraded");
    }
}