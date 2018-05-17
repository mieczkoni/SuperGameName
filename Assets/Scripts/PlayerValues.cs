using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {

    public int playerHealth;

    public int pistolDamage;
    public int pistolBullets;
    public float pistolShotFrequency;
    public float pistolShotRange;
    public float pistolBulletMovementSpeed;
    public float pistolReloadingTime;
    
    public int minigunDamage;
    public int minigunBullets;
    public float minigunShotFrequency;
    public float minigunShotRange;
    public float minigunBulletMovementSpeed;
    public float minigunReloadingTime;

    public int granadeDamage;
    public float granadeThrowRange;

    public int playerCoins;

    private GameObject text;
    // Use this for initialization
    void Start()
    {
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            GameObject.Find("MainCanvas").GetComponent<CanvasController>().GameOver();
            playerHealth = 100;
        }
        GameObject.Find("InGameCanvas/PlayerHealth").GetComponent<Text>().text = playerHealth.ToString();
    }

    public List<float> GetPistolValues()
    {
        List<float> values = new List<float>();
        values.Add(pistolDamage);
        values.Add(pistolShotRange);
        values.Add(pistolBulletMovementSpeed);
        values.Add(pistolShotFrequency);
        values.Add(pistolBullets);
        values.Add(pistolReloadingTime);
        return values;
    }

    public List<float> GetMinigunValues()
    {
        List<float> values = new List<float>();
        values.Add(minigunDamage);
        values.Add(minigunShotRange);
        values.Add(minigunBulletMovementSpeed);
        values.Add(minigunShotFrequency);
        values.Add(minigunBullets);
        values.Add(minigunReloadingTime);
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
        playerCoins += addCoins;
        GameObject.Find("Coins").GetComponent<Text>().text = "$ " + playerCoins.ToString();
    }

    public void DecreaseHealth(int value)
    {
        this.playerHealth -= value;
    }

    public void UpdateValues(string whatValue, float value)
    {
        switch (whatValue)
        {
            case "pistolDamage":
                pistolDamage += (int)value;
                break;
            case "pistolBullets":
                pistolBullets += (int)value;
                break;
            case "pistolReloadingTime":
                pistolReloadingTime -= value;
                break;
            case "minigunDamage":
                minigunDamage += (int)value;
                break;
            case "minigunBullets":
                minigunBullets += (int)value;
                break;
            case "minigunReloadingTime":
                minigunReloadingTime -= value;
                break;
            default:
                break;
        }
    }
}
