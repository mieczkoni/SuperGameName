using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float movementSpeed, range, damage, timer;
    private bool hit = false;
    private PlayerValues playerValues;
    private PlayerShooting playerShooting;

    private void Start()
    {
        playerValues = GameObject.Find("Player").GetComponent<PlayerValues>();
        playerShooting = GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>();
        if (playerShooting.gunType == "Pistol" && playerValues.pistolCriticalShotChance >= 1)
        {
            float rand = Random.Range(0, 101);
            if (rand <= playerValues.pistolCriticalShotChance)
            {
                damage += (damage * 2) + 123;
            }
        }
        else if (playerShooting.gunType == "Minigun" && playerValues.minigunCriticalShotChance >= 1)
        {
            float rand = Random.Range(0, 101);
            if (rand <= playerValues.pistolCriticalShotChance)
            {
                damage += (damage * 2) + 123;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= range)
        {
            Destroy(this.gameObject);
        }
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    public void setValues(float damage, float range, float movementSpeed)
    {
        this.damage = damage;
        this.range = range;
        this.movementSpeed = movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hit == false && other.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.transform.name == "WeakEnemy")
                {
                    other.GetComponent<WeakEnemyAI>().decreaseHealth(damage);
                }
                else if (other.transform.name == "MediumEnemy")
                {
                    other.GetComponent<MediumEnemyAI>().decreaseHealth(damage);
                }
                Destroy(this.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
