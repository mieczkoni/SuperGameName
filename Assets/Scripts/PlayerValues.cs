using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {

    public float playerHealth, playerExperience;
    public float playerSpeed;
    public int playerLevel;

    public string[] playerWeapons = { "Pistol", "Minigun" };
    public string mainWeapon = "Pistol", secondWeapon, thirdWeapon;

    // [0] - pistol, [1] - minigun
    public bool[] weaponsOwned = { true, false };

    // [0] - shield
    public bool[] skillsOwned = { false };

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
    public float granadeThrowRange;

    public int playerCoins;
    public int distanceRecord;

    private Image healthBanner, experienceBanner;

    private GameObject text;

    private PlayerShooting playerShooting;
    // Use this for initialization
    void Start()
    {
        playerShooting = GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>();
        healthBanner = GameObject.Find("HealthBanner/Mask/GreenBanner").GetComponent<Image>();
        experienceBanner = GameObject.Find("ExperienceBanner/Mask/YellowBanner").GetComponent<Image>();
        playerHealth = 100;
        UpdateExperienceValue(0);
        playerLevel = 0;
        if (distanceRecord >= 1)
        {
            GameObject.Find("StartCanvas/Record").GetComponent<Text>().text = "YOU ARE " + distanceRecord.ToString() + "m CLOSER TO INFINITY!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + playerSpeed);
    }

    public void initStartGame()
    {
        playerHealth = 100;
        healthBanner.fillAmount = playerHealth;
        GameObject.Find("InGameCanvas/PlayerHealth").GetComponent<Text>().text = playerHealth.ToString();
        playerSpeed = 0.05f;
        GetComponent<SpawnEnemies>().enabled = true;
        playerShooting.enabled = true;
        print("STARTIN GAME");
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
        values.Add(granadeThrowRange);
        return values;
    }

    public void UpdateCoinsValue(int addCoins)
    {
        print("Add " + addCoins + " coins.");
        playerCoins += addCoins;
        GameObject.Find("Coins").GetComponent<Text>().text = "$ " + playerCoins.ToString();
        print("Actual coins value: " + playerCoins);
    }

    public void UpdateExperienceValue(int addExperience)
    {
        playerExperience += addExperience;
        if (playerExperience >= playerLevel * 1000 + (1000 * (playerLevel / 10)))
        {
            playerLevel += 1;
        }
        GameObject.Find("InGameCanvas/PlayerExperience").GetComponent<Text>().text = playerExperience.ToString() + "/" + (playerLevel * 1000 + (1000 * (playerLevel / 10)).ToString());
        float expForPreviousLevel = (playerLevel - 1) * 1000 + (1000 * ((playerLevel - 1) / 10));
        float expForNextLevel = playerLevel * 1000 + (1000 * (playerLevel / 10));
        float expToNextLevel = expForNextLevel - expForPreviousLevel;
        experienceBanner.fillAmount = (((playerExperience - expForPreviousLevel) * 100) / expToNextLevel) / 100;
    }

    public void DecreaseHealth(int value)
    {
        playerHealth -= value;
        healthBanner.fillAmount = playerHealth / 100;
        GameObject.Find("InGameCanvas/PlayerHealth").GetComponent<Text>().text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            GameObject.Find("MainCanvas").GetComponent<CanvasController>().GameOver();
            GetComponent<GameController>().PlayerDeath();
            playerHealth = 100;
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
            case "distanceRecord":
                distanceRecord = (int)value;
                break;
            default:
                break;
        }
    }

    public void SetStringValues(string whatValue, string value)
    {
        switch (whatValue)
        {
            case "mainWeapon":
                mainWeapon = value;
                break;
            case "secondWeapon":
                secondWeapon = value;
                break;
            case "thirdWeapon":
                thirdWeapon = value;
                break;
        }
    }

    public void SetBoolValues(string whatValue, bool value)
    {
        switch (whatValue)
        {
            case "Pistol":
                weaponsOwned[0] = value;
                break;
            case "Minigun":
                weaponsOwned[1] = value;
                break;
        }
    }
}
