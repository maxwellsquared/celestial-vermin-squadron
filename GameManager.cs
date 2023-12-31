using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public int numEnemies;
    public int livesLeft;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        score = 0;
        scoreText.text = "SCORE: " + score;

        livesLeft = 3;
        livesText.text = "LIVES: ";
        for (int i = 0; i < livesLeft; i++)
        {
            livesText.text += "X";
        }
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

    public void SubtractLife()
    {
        livesLeft -= 1;
        livesText.text = "LIVES: ";
        for (int i = 0; i < livesLeft; i++)
        {
            livesText.text += "X";
        }
        if (livesLeft < 1)
        {
            livesText.text = "";
            Invoke("NoMoreLives", 2f);
            // transition to GAME OVER scene
            // which transitions to main menu
        }
    }

    public void NoMoreLives()
    {
        StartCoroutine(loadGameOver());
    }

    public IEnumerator loadGameOver()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOver");
        yield return null;
    }
}
