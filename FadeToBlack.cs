using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FadeToBlack : MonoBehaviour
{
    public float fadeSpeed = 1.0f;
    private float fadeProgress = 0.0f;
    private bool hasClicked = false;

    void Awake()
    {
        // set FadeToBlack color to solid black
        GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        Invoke("MoveAway", 2.0f);
    }

    // Update is called once per frame
    void Update() { }

    public void FadeAway()
    {
        hasClicked = true;
        transform.position = new Vector3(0f, 0f);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        while (fadeProgress < 1.0f)
        {
            fadeProgress += fadeSpeed * Time.deltaTime;
            GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 1f - fadeProgress);
            yield return null;
        }
        // smoothly go between solid black and transparent here
    }

    public IEnumerator FadeOut()
    {
        while (fadeProgress > 0.0f)
        {
            fadeProgress -= fadeSpeed * Time.deltaTime;
            // Debug.Log("Progress:" + (fadeProgress * 100) + "%");
            GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 1f - fadeProgress);
            yield return null;
        }
    }

    public void MoveAway()
    {
        if (!hasClicked)
        {
            transform.position = new Vector3(0f, 900000f);
        }
    }
}
