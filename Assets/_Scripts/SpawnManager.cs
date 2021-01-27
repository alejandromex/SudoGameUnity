using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject[] powersUp;
    float spawnPosX, spawnPosZ;
    private float spawnRange = 9;
    private bool CoroutineIsRunning = false;
    void Start()
    {
       
      //  Instantiate(enemyPrefab, SetRandomNumber(), enemyPrefab.transform.rotation);
    }

    void Update()
    {
        
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            SpawnEnemies();
        }

        if(GameObject.FindGameObjectsWithTag("PowerUp").Length == 0 && !CoroutineIsRunning)
        {
            CoroutineIsRunning = true;
            StartCoroutine(SpawnPowerUp());
        }
    }


    private void SpawnEnemies()
    {
        for (int i = 0; i < GlobalStats.spawnEnemiesLevel; i++)
        {
            Instantiate(enemyPrefab, SetRandomNumber(), enemyPrefab.transform.rotation);
        }
        GlobalStats.spawnEnemiesLevel++;
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(20);
        Instantiate(powersUp[Random.Range(0,powersUp.Length)], SetRandomNumber(), enemyPrefab.transform.rotation);
        CoroutineIsRunning = false;
    }

    /// <summary>
    /// Get a new position for the enemy
    /// </summary>
    /// <returns>new randomly vector3</returns>
    public Vector3 SetRandomNumber()
    {
        spawnPosX = Random.Range(-spawnRange, spawnRange);
        spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
