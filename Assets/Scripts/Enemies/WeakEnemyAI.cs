using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyAI : MonoBehaviour
{
    public float health, damage;
    Transform player;
    PlayerValues playerValues;
    UnityEngine.AI.NavMeshAgent nav;
    private float hitTimer = 0.0f, calculateTimer = 0.0f;
    private bool playerInRange = false;

    private CanvasController canvasController;

    // Use this for initialization
    void Start()
    {
        canvasController = GameObject.Find("MainCanvas").GetComponent<CanvasController>();
        player = GameObject.Find("Player").transform;
        playerValues = player.GetComponent<PlayerValues>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
        if (!playerInRange)
        {
            calculateTimer += Time.deltaTime;
            if (calculateTimer >= 1.0f)
            {
                if (calculateDistance() <= 2)
                {
                    playerInRange = true;
                }
            }
        }
        else if (playerInRange && !canvasController.isGameOver)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= 1.0)
            {
                playerValues.DecreaseHealth((int) damage);
                hitTimer = 0.0f;
            }
        }
    }

    public void SetEnemyValues(int health, float damage)
    {
        this.health = health;
        this.damage = damage;
    }

    public void decreaseHealth(float value)
    {
        this.health -= value;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            playerValues.UpdateCoinsValue(15);
            playerValues.UpdateExperienceValue(25);
        }
    }

    private float calculateDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(this.transform.position.x - player.transform.position.x, 2) + Mathf.Pow(this.transform.position.z - player.transform.position.z, 2));
    }
}