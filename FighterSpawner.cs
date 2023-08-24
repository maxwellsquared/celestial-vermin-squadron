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

    public GameManager gameManager;

    private CinemachineDollyCart playerDollyCart;

    // get dolly track to spawn in

    // set enemy fighter speed
    public float enemySpeed = -30f;

    // set spawn frequency
    public float spawnFrequency = 3f;

    // set max enemies
    public int maxEnemies = 25;

    // set spawn frequency randomness
    // set distance from player on track to spawn
    public float spawnDistance = 200f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerDollyCart = playerCart.GetComponent<CinemachineDollyCart>();
        // todo: cool particle effect when teleporting in

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update() { }

    // coroutine to start the spawner
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (gameManager.numEnemies <= maxEnemies)
            {
                SpawnEnemy();
                Debug.Log("Spawned an enemy!" + gameManager.numEnemies + " enemies exist now.");
            }
            if (gameManager.numEnemies - 1 == maxEnemies)
            {
                Debug.Log("!!!TOO MANY ENEMIES!!!");
            }
            yield return new WaitForSeconds(spawnFrequency);
            // yield return new WaitForSeconds(2f);
        }
    }

    // function to spawn an enemy
    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        CinemachineDollyCart enemyDollyCart = newEnemy.GetComponent<CinemachineDollyCart>();
        enemyDollyCart.m_Path = dollyTrack;

        float playerPathPosition = playerDollyCart.m_Position;
        float newPosition = playerPathPosition + spawnDistance;
        Debug.Log("Spawning enemy at " + newPosition);

        if (newPosition > dollyTrack.PathLength)
        {
            newPosition -= dollyTrack.PathLength;

            Debug.Log("ADJUSTING POSITION TO " + newPosition);
        }

        enemyDollyCart.m_Position = newPosition;
        enemyDollyCart.m_Speed = enemySpeed;
        // Debug.Log("Finished instantiating enemy");
    }
}
