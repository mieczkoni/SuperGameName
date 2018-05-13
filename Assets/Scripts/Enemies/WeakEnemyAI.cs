﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyAI : MonoBehaviour
{

    Transform player;
    UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
