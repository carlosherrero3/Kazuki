using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // asigna esto desde el inspector
    public float fadeDuration = 1f;
    public string sceneToLoad = "NombreDeTuEscena";

    private bool isFading = false;

    void Update()
    {
        if (!isFading && Input.anyKeyDown)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        isFading = true;
        float timer = 0f;

        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Carga la escena después del fade
        SceneManager.LoadScene(sceneToLoad);
    }
}
