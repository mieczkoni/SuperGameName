using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health;

    public void decreaseHealth(float value)
    {
        this.health -= value;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<PlayerValues>().UpdateCoinsValue(15);
        }
    }
}
