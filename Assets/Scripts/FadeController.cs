using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeSpeed = 2f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeCanvas.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        while (fadeCanvas.alpha < 1)
        {
            fadeCanvas.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeCanvas.alpha = 1;
    }
}
    