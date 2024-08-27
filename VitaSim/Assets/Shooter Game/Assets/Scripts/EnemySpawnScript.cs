﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject enemy;
    float randx, randy;
    Vector2 spwanLocation;
    public float spawnRate = 1f;
    float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randx=Random.Range(-8, 8);
            randy=Random.Range(-5, 5);
            spwanLocation=new Vector2(randx, randy);
            Instantiate(enemy, spwanLocation, Quaternion.identity);
        }
    }
}
