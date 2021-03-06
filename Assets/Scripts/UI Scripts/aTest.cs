﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aTest : MonoBehaviour
{
    public GameObject wallPrefab, floorPrefab;
    Transform previousFloorCoordinates;
    private float timer;

    private void Start()
    {
        previousFloorCoordinates = GameObject.Find("Floor").transform;
        CreateNewArea();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 7.0f)
        {
            CreateNewArea();
            timer = 0.0f;
        }
    }

    void CreateNewArea()
    {
        GameObject newFloor = Instantiate(floorPrefab, new Vector3(previousFloorCoordinates.position.x, -0.25f, previousFloorCoordinates.position.z + 40), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(5, 3f, previousFloorCoordinates.position.z + 40), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(-5, 3f, previousFloorCoordinates.position.z + 40), Quaternion.identity);
        previousFloorCoordinates = newFloor.transform;
    }
}