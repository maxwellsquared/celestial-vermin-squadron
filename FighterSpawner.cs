using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FighterSpawner : MonoBehaviour
{
    // get player position
    public GameObject playerCart;
    public GameObject enemyPrefab;
    public CinemachineSmoothPath dollyTrack;

    private CinemachineDollyCart playerDollyCart;

    // get dolly track to spawn in

    // set enemy fighter speed
    public float enemySpeed = -20f;

    // set spawn frequency
    public float spawnFrequency = 3f;

    // set max enemies
    public int maxEnemies = 5;
    private int numEnemies = 0;

    // set spawn frequency randomness
    // set distance from player on track to spawn
    public float spawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerDollyCart = playerCart.GetComponent<CinemachineDollyCart>();
        // todo: cool particle effect when teleporting in

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update() { }

    // coroutine to start the spawner
    IEnumerator SpawnEnemies()
    {
        while (numEnemies <= maxEnemies)
        {
            SpawnEnemy();
            numEnemies++;
            Debug.Log("Spawned an enemy!" + numEnemies + " enemies exist now.");
            yield return new WaitForSeconds(spawnFrequency);
            // yield return new WaitForSeconds(2f);
        }
    }

    // function to spawn an enemy
    public void SpawnEnemy()
    {
        Debug.Log("Instantiating enemy");
        GameObject newEnemy = Instantiate(enemyPrefab);
        CinemachineDollyCart enemyDollyCart = newEnemy.GetComponent<CinemachineDollyCart>();
        enemyDollyCart.m_Path = dollyTrack;

        float playerPathPosition = playerDollyCart.m_Position;
        enemyDollyCart.m_Position = dollyTrack.PathLength;
        enemyDollyCart.m_Speed = enemySpeed;
        Debug.Log("Finished instantiating enemy");
    }
}
