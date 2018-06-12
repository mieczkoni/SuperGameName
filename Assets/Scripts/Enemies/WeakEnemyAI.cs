using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyAI : MonoBehaviour
{
    public float health;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    private float hitTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        hitTimer += Time.deltaTime;
        nav.SetDestination(player.position);
        if (calculateDistance() <= 2)
        {
            if (hitTimer >= 1.0)
            {
                player.GetComponent<PlayerValues>().DecreaseHealth(10);
                hitTimer = 0.0f;
            }
        }
    }

    public void decreaseHealth(float value)
    {
        this.health -= value;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<PlayerValues>().UpdateCoinsValue(15);
            GameObject.Find("Player").GetComponent<PlayerValues>().UpdateExperienceValue(20);
        }
    }

    private float calculateDistance()
    {
        GameObject plejer = GameObject.Find("Player");
        return Mathf.Sqrt(Mathf.Pow(this.transform.position.x - plejer.transform.position.x, 2) + Mathf.Pow(this.transform.position.z - plejer.transform.position.z, 2));
    }
}