using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public string nextSceneName;
    public bool canFadeOut;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // Inicia com o canvas totalmente transparente
        canvasGroup.alpha = 0;

        // Inicia o fade in
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // Aguarda por 4 segundos antes de iniciar o fade out
        yield return new WaitForSeconds(2.0f);

        // Inicia o fade out
        if (canFadeOut)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        // Carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }
}
