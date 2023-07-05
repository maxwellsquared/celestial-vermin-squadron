using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public Splosion splosionPrefab;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateEnemies(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update() { }

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

    private void OnDestroy()
    {
        gameManager.UpdateEnemies(-1);
    }
}
