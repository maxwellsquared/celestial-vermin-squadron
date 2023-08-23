using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public Splosion splosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");

        gameManager.UpdateEnemies(1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Splosion splosion = Instantiate(
                splosionPrefab,
                this.transform.position,
                this.transform.rotation
            );
            // targetSplodeSound.PlayOneShot(targetSplodeSound.clip, soundVolume);
            gameManager.UpdateScore(1);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update() { }

    private void OnDestroy()
    {
        gameManager.UpdateEnemies(-1);
    }
}
