//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NoiR_CCC

public class ObstacleSpawner : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject pudding1;
    public GameObject pudding2;
    public float minInterval = 1f;

    private void Start()
    {
        StartCoroutine(SpawnSmallObstacles());
        StartCoroutine(SpawnLargeObstacles());
        StartCoroutine(SpawnPudding());
        StartCoroutine(SpawnPudding2());
    }

    public void spawnSmallObstacle()
    {
        if (Random.Range(0, 10) > 3)
        {
            Instantiate(obstacle1, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void spawnLargeObstacle()
    {
        if (Random.Range(0, 10) > 7)
        {
            Instantiate(obstacle2, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void spawnPudding()
    {
        if (Random.Range(0,10) > 2)
        {
            Instantiate(pudding1, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void spawnPudding2()
    {
        if (Random.Range(0, 10) > 6)
        {
            Instantiate(pudding2, spawnPoint.position + new Vector3(0f, 6f, 0f), spawnPoint.rotation);
        }
    }

    private IEnumerator SpawnSmallObstacles()
    {
        while (true)
        {
            spawnSmallObstacle();
            yield return new WaitForSeconds(Random.Range(minInterval,2f));
        }      
    }

    private IEnumerator SpawnLargeObstacles()
    {
        while (true)
        {
            spawnLargeObstacle();
            yield return new WaitForSeconds(Random.Range(minInterval, 3f));
        }

    }

    private IEnumerator SpawnPudding()
    {
        while (true)
        {
            spawnPudding();
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }


    private IEnumerator SpawnPudding2()
    {
        while (true)
        {
            spawnPudding2();
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}
