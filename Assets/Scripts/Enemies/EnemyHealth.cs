using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<PlayerValues>().UpdateCoinsValue(15);
        }
    }

    public void decreaseHealth(float value)
    {
        this.health -= value;
    }
}
