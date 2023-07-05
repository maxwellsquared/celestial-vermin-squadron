using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public int numEnemies;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 6;
        score = 0;
        scoreText.text = "SCORE: " + score;
    }

    // Update is called once per frame
    void Update() { }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score + " ENEMIES: " + numEnemies;
    }

    public void UpdateEnemies(int enemiesToAdd)
    {
        numEnemies += enemiesToAdd;
        scoreText.text = "SCORE: " + score + " ENEMIES: " + numEnemies;
    }
}
