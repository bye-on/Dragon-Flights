using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    
    void SpawnEnemy() {
        int randomEnemy = Random.Range(0, 3);
        int randomPoint = Random.Range(0, 5);
        Instantiate(enemyObjs[randomEnemy], 
            spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay) {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }
}
