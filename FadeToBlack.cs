using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float fadeSpeed = 1.0f;
    private float fadeProgress = 0.0f;

    void Awake()
    {
        // set FadeToBlack color to solid black
        GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update() { }

    public IEnumerator FadeIn()
    {
        while (fadeProgress < 1.0f)
        {
            fadeProgress += fadeSpeed * Time.deltaTime;
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f - fadeProgress);
            yield return null;
        }
        // smoothly go between solid black and transparent here
    }
}
