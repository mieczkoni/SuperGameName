using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float movementSpeed, range, damage;

    // Update is called once per frame
    void Update()
    {
        if (calculateDistance() >= range)
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

    private float calculateDistance()
    {
        GameObject plejer = GameObject.Find("Player");
        return Mathf.Sqrt(Mathf.Pow(this.transform.position.x - plejer.transform.position.x, 2) + Mathf.Pow(this.transform.position.z - plejer.transform.position.z, 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.transform.name == "WeakEnemy")
            {
                other.GetComponent<WeakEnemyAI>().decreaseHealth(damage);
            } else if (other.transform.name == "MediumEnemy")
            {
                other.GetComponent<MediumEnemyAI>().decreaseHealth(damage);
            }
            Destroy(this.gameObject);
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
