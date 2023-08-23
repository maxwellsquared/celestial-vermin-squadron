using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverRoutine : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    private string currentText = "";
    private bool isReady;
    private bool isClicked = false;
    private bool isLoaded = false;

    public float respawnTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMenu());

        isReady = false;
        Invoke("SetReady", respawnTime); // Prevent click from going to menu

        Invoke("Text1", 0.2f); // THIS IS THE END OF THE GAME
        Invoke("Text2", 3.75f); // YOU HAVE DIED
        Invoke("Text3", 5.5f); // THE CREDITS GO BY
        Invoke("Text3a", 8.0f); // IN GREAT BIG STACKS
        Invoke("Text4", 11.0f); // AND THE WHOLE THING WAS MADE BY MAX
        Invoke("ClickToContinue", 19f); // PLAY AGAIN?
    }

    // Update is called once per frame
    void Update()
    {
        subtitleText.text = currentText;

        if (isReady && Input.anyKeyDown)
        {
            Debug.Log("CLICK!");
            isClicked = true;
        }
    }

    private IEnumerator TypewriterText(string textToAdd)
    {
        for (int i = 0; i < textToAdd.Length; i++)
        {
            currentText += textToAdd[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator LoadMenu()
    {
        // load the main menu in the background

        // there are probably too many yield return nulls here
        yield return null;
        Debug.Log("Starting load coroutine!");

        // set up an async operation to get the main menu in memory (I think)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        // immediately disallow activation
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // if the player has clicked the mouse or pressed any button and progress is >90%...
            if (isClicked && asyncLoad.progress >= 0.9f)
            {
                // allow activation, which sends the player to the main menu
                asyncLoad.allowSceneActivation = true;
            }

            // if you remove this you get an infinite loop lol
            yield return null;
        }

        yield return null;
    }

    private void SetReady()
    {
        isReady = true;
    }

    private void Text1()
    {
        StartCoroutine(TypewriterText("THIS IS THE END OF THE GAME. "));
    }

    private void Text2()
    {
        StartCoroutine(TypewriterText("YOU HAVE DIED.\n"));
    }

    private void Text3()
    {
        StartCoroutine(TypewriterText("THE CREDITS GO BY "));
    }

    private void Text3a()
    {
        StartCoroutine(TypewriterText("IN GREAT BIG STACKS\n"));
    }

    private void Text4()
    {
        StartCoroutine(TypewriterText("AND THE WHOLE THING WAS MADE BY MAX."));
    }

    private void ClickToContinue()
    {
        currentText = "PLAY AGAIN? CLICK ANYWHERE TO CONTINUE.";
    }
}
