using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Target targetPrefab;
    public GameManager gameManager;
    public Transform playerLocation;
    public float spawnDelay = 4f;
    public float minDist = 10f;
    float nextTimeToSpawn;
    private int targetsSpawned;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeToSpawn = 4 + Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        targetsSpawned = gameManager.numEnemies;
        CheckSpawn();
    }

    void FixedUpdate() { }

    void CheckSpawn()
    {
        float dist = Vector3.Distance(playerLocation.position, transform.position);
        if (dist >= minDist)
        {
            if (nextTimeToSpawn <= 0)
            {
                if (targetsSpawned < 50)
                {
                    SpawnTarget();
                }
                nextTimeToSpawn = spawnDelay + Random.Range(0, 3);
            }
            else
            {
                nextTimeToSpawn -= Time.deltaTime;
            }
        }
    }

    void SpawnTarget()
    {
        float newX = transform.position.x + Random.Range(-6, 6);
        float newY = transform.position.y + Random.Range(-6, 6);
        Vector3 newPos = new Vector3(newX, newY, transform.position.z);
        Instantiate(targetPrefab, newPos, transform.rotation);
    }
}
