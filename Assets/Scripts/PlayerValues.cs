using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {

    public float playerHealth, playerExperience, actualPlayerHealth;
    public float playerSpeed;
    public int playerLevel;
    public int skillPoints;

    //public string[] playerWeapons = { "Pistol", "Minigun" };
    //public string mainWeapon = "Pistol", secondWeapon, thirdWeapon;


    public bool pistolOwned = true, minigunOwned = false;
    public bool shieldOwned = false;
    public bool granadeOwned = false;
    
    public int pistolDamage;
    public float pistolShotRange;
    public float pistolBulletMovementSpeed;
    public float pistolCriticalShotChance;
    public float pistolBulletsPerSecond;

    public int minigunDamage;
    public float minigunShotRange;
    public float minigunBulletMovementSpeed;
    public float minigunCriticalShotChance;
    public float minigunBulletsPerSecond;

    public float shieldTime;

    public int granadeDamage;
    public float granadeRange;

    public int playerCoins;
    public int distanceRecord;

    private Image healthBanner, experienceBanner;

    private GameObject text;

    private PlayerShooting playerShooting;
    private SkillsController skillsController;

    private float saveTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        GetAllData();
        playerShooting = GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>();
        healthBanner = GameObject.Find("HealthBanner/Mask/GreenBanner").GetComponent<Image>();
        experienceBanner = GameObject.Find("ExperienceBanner/Mask/YellowBanner").GetComponent<Image>();
        skillsController = GetComponent<SkillsController>();
        UpdateExperienceValue(0);
        if (distanceRecord >= 1)
        {
            GameObject.Find("StartCanvas/Record").GetComponent<Text>().text = "YOU ARE " + distanceRecord.ToString() + "m CLOSER TO INFINITY!";
        }
        actualPlayerHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + playerSpeed);
        saveTimer += Time.deltaTime;
        if (saveTimer >= 30)
        {
            SavePlayerData();
            GetComponent<GameController>().SaveGameControllerData();
        }
    }

    public void initStartGame()
    {
        playerHealth = 100;
        healthBanner.fillAmount = playerHealth;
        GameObject.Find("InGameCanvas/PlayerHealth").GetComponent<Text>().text = playerHealth.ToString();
        playerSpeed = 0.05f;
        GetComponent<SpawnEnemies>().enabled = true;
        GetComponent<SpawnEnemies>().spawnTime = 1.0f;
        playerShooting.enabled = true;
        Time.timeScale = 1.0f;
    }

    public void initResumeGame()
    {
        playerSpeed = 0.05f;
        GetComponent<SpawnEnemies>().enabled = true;
        playerShooting.enabled = true;
    }

    public void initPauseGame()
    {
        playerSpeed = 0.0f;
        playerShooting.enabled = false;
        GetComponent<SpawnEnemies>().enabled = false;
    }

    public void initGameOver()
    {
        playerSpeed = 0.0f;
        playerShooting.enabled = false;
        GetComponent<SpawnEnemies>().enabled = false;
    }

    public List<float> GetPistolValues()
    {
        List<float> values = new List<float>();
        values.Add(pistolDamage);
        values.Add(pistolShotRange);
        values.Add(pistolBulletMovementSpeed);
        values.Add(pistolCriticalShotChance);
        values.Add(pistolBulletsPerSecond);
        return values;
    }

    public List<float> GetMinigunValues()
    {
        List<float> values = new List<float>();
        values.Add(minigunDamage);
        values.Add(minigunShotRange);
        values.Add(minigunBulletMovementSpeed);
        values.Add(minigunCriticalShotChance);
        values.Add(minigunBulletsPerSecond);
        return values;
    }

    public List<float> GetGranadeValues()
    {
        List<float> values = new List<float>();
        values.Add(granadeDamage);
        values.Add(granadeRange);
        return values;
    }

    public void UpdateCoinsValue(int addCoins)
    {
        playerCoins += addCoins;
        GameObject.Find("Coins").GetComponent<Text>().text = "$ " + playerCoins.ToString();
    }

    public void UpdateSkillPointsValue(int substractPoints)
    {
        skillPoints -= substractPoints;
    }

    public void UpdateExperienceValue(int addExperience)
    {
        playerExperience += addExperience;

        if (playerExperience >= (playerLevel * 1100))
        {
            playerLevel += 1;
        }
        GameObject.Find("WeaponsCanvas/PlayerLevel").GetComponent<Text>().text = playerLevel.ToString();
        GameObject.Find("InGameCanvas/PlayerExperience").GetComponent<Text>().text = playerExperience.ToString() + "/" + (playerLevel * 1100);
        experienceBanner.fillAmount = ((100 * (playerExperience - ((playerLevel - 1) * 1100))) / 1100) / 100;
    }

    public void DecreaseHealth(int value)
    {
        if (skillsController.shieldActive == false)
        {
            actualPlayerHealth -= value;
            healthBanner.fillAmount = actualPlayerHealth / 100;
            GameObject.Find("InGameCanvas/PlayerHealth").GetComponent<Text>().text = actualPlayerHealth.ToString();
            if (actualPlayerHealth <= 0)
            {
                GameObject.Find("MainCanvas").GetComponent<CanvasController>().GameOver();
                GetComponent<GameController>().PlayerDeath();
                actualPlayerHealth = playerHealth;
            }
        }
    }

    public void UpdateValues(string whatValue, float value)
    {
        switch (whatValue)
        {
            case "pistolDamage":
                pistolDamage += (int)value;
                break;
            case "pistolCriticalShotChance":
                pistolCriticalShotChance += value;
                break;
            case "pistolBPS":
                pistolBulletsPerSecond += value;
                break;
            case "minigunDamage":
                minigunDamage += (int)value;
                break;
            case "minigunCriticalShotChance":
                minigunCriticalShotChance += value;
                break;
            case "minigunBPS":
                minigunBulletsPerSecond += value;
                break;
            case "granadeDamage":
                granadeDamage += (int)value;
                break;
            case "granadeRange":
                granadeRange += (int)value;
                break;
            case "shieldTime":
                shieldTime += value;
                break;
            case "distanceRecord":
                distanceRecord = (int)value;
                break;
            default:
                break;
        }
    }

    public void SetBoolValues(string whatValue, bool value)
    {
        switch (whatValue)
        {
            case "Pistol":
                pistolOwned = value;
                break;
            case "Minigun":
                minigunOwned = value;
                break;
            case "Shield":
                shieldOwned = value;
                break;
            case "Granade":
                granadeOwned = value;
                break;
        }
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetFloat("playerHealth", playerHealth);
        PlayerPrefs.SetFloat("playerExperience", playerExperience);
        PlayerPrefs.SetFloat("pistolShotRange", pistolShotRange);
        PlayerPrefs.SetFloat("pistolBulletMovementSpeed", pistolBulletMovementSpeed);
        PlayerPrefs.SetFloat("pistolCriticalShotChance", pistolCriticalShotChance);
        PlayerPrefs.SetFloat("pistolBulletsPerSecond", pistolBulletsPerSecond);
        PlayerPrefs.SetFloat("minigunShotRange", minigunShotRange);
        PlayerPrefs.SetFloat("minigunBulletMovementSpeed", minigunBulletMovementSpeed);
        PlayerPrefs.SetFloat("minigunCriticalShotChance", minigunCriticalShotChance);
        PlayerPrefs.SetFloat("minigunBulletsPerSecond", minigunBulletsPerSecond);
        PlayerPrefs.SetFloat("shieldTime", shieldTime);
        PlayerPrefs.SetFloat("granadeRange", granadeRange);

        PlayerPrefs.SetInt("playerLevel", playerLevel);
        PlayerPrefs.SetInt("skillPoints", skillPoints);
        PlayerPrefs.SetInt("pistolDamage", pistolDamage);
        PlayerPrefs.SetInt("minigunDamage", minigunDamage);
        PlayerPrefs.SetInt("granadeDamage", granadeDamage);
        PlayerPrefs.SetInt("playerCoins", playerCoins);
        PlayerPrefs.SetInt("distanceRecord", distanceRecord);

        SetBool("pistolOwned", pistolOwned);
        SetBool("minigunOwned", minigunOwned);
        SetBool("shieldOwned", shieldOwned);
        SetBool("granadeOwned", granadeOwned);
        PlayerPrefs.Save();
    }

    private void GetAllData()
    {
        playerHealth = PlayerPrefs.GetFloat("playerHealth", 100);
        playerExperience = PlayerPrefs.GetFloat("playerExperience");
        pistolShotRange = PlayerPrefs.GetFloat("pistolShotRange", 1);
        pistolBulletMovementSpeed = PlayerPrefs.GetFloat("pistolBulletMovementSpeed", 40);
        pistolCriticalShotChance = PlayerPrefs.GetFloat("pistolCriticalShotChance");
        pistolBulletsPerSecond = PlayerPrefs.GetFloat("pistolBulletsPerSecond", 2);
        minigunShotRange = PlayerPrefs.GetFloat("minigunShotRange", 1);
        minigunBulletMovementSpeed = PlayerPrefs.GetFloat("minigunBulletMovementSpeed", 40);
        minigunCriticalShotChance = PlayerPrefs.GetFloat("minigunCriticalShotChance");
        minigunBulletsPerSecond = PlayerPrefs.GetFloat("minigunBulletsPerSecond", 5);
        shieldTime = PlayerPrefs.GetFloat("shieldTime", 2);
        granadeRange = PlayerPrefs.GetFloat("granadeRange", 2);
    
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
        skillPoints = PlayerPrefs.GetInt("skillPoints", 1);
        pistolDamage = PlayerPrefs.GetInt("pistolDamage", 50);
        minigunDamage = PlayerPrefs.GetInt("minigunDamage", 50);
        granadeDamage = PlayerPrefs.GetInt("granadeDamage", 100);
        playerCoins = PlayerPrefs.GetInt("playerCoins", 10000);
        distanceRecord = PlayerPrefs.GetInt("distanceRecord");

        pistolOwned = GetBool("pistolOwned", true);
        minigunOwned = GetBool("minigunOwned", false);
        shieldOwned = GetBool("shieldOwned", false);
        granadeOwned = GetBool("granadeOwned", false);
    }

    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public static bool GetBool(string name, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetBool(name);
        }

        return defaultValue;
    }

}
