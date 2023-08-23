using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    private bool level1Loaded = false;
    private bool level2Loaded = false;

    List<string> allScenes = new List<string> { "Level1", "Level2" };

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(LoadLevel1());
        // StartCoroutine(LoadLevel2());
        loadingText.text = "";
    }

    // Update is called once per frame
    void Update() { }

    public void StartLevel1()
    {
        Debug.Log("Clicked Level 1");
        // SetMyScene("Level1");
        StartCoroutine(LoadLevel1());
        loadingText.text = "Loading Level 1...";
    }

    public void StartLevel2()
    {
        Debug.Log("Clicked Level 2");
        // SetMyScene("Level2");
        StartCoroutine(LoadLevel2());

        loadingText.text = "Loading Level 1...";
    }

    public IEnumerator LoadLevel1()
    {
        // load level 1 asynchronously

        AsyncOperation asyncLoad1 = SceneManager.LoadSceneAsync("Level1");
        // asyncLoad1.allowSceneActivation = false;
        // while (asyncLoad1.progress < 0.9f)
        // {
        //     Debug.Log("progress: " + asyncLoad1.progress);
        //     yield return null;
        // }
        // if (asyncLoad1.progress >= 0.9f)
        // {
        //     Debug.Log("Done loading Level 1!");
        //     level1Loaded = true;
        // }
        yield return null;
    }

    public IEnumerator LoadLevel2()
    {
        // load level 2 asynchronously
        AsyncOperation asyncLoad2 = SceneManager.LoadSceneAsync("Level2");
        // asyncLoad2.allowSceneActivation = false;

        // while (asyncLoad2.progress < 0.9f)
        // {
        //     // Debug.Log("lv2 progress: " + (asyncLoad2.progress * 100) + "%");
        //     yield return null;
        // }
        // if (asyncLoad2.progress >= 0.9f)
        // {
        //     Debug.Log("Level 2 done!");
        //     level2Loaded = true;
        // }
        yield return null;
    }

    // public void SetMyScene(string sceneName)
    // {
    //     // unload all other scenes
    //     foreach (string scene in allScenes)
    //     {
    //         if (scene != sceneName)
    //         {
    //             Debug.Log("Unloading " + scene);
    //             StartCoroutine(UnloadScene(scene));
    //         }
    //     }

    //     // set the active scene
    //     SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

    //     // unload menu
    //     StartCoroutine(UnloadScene("MainMenu"));
    // }

    public IEnumerator UnloadScene(string sceneName)
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneName);
        yield return unloadScene;
    }
}
