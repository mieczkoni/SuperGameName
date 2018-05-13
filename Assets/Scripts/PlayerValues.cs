using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}
