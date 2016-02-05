using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Not implemented yet
public static class SceneFader
{
    static SpriteRenderer sprRenderer;
    static BoxCollider2D collider;
    static Sprite black;
    static Sprite white;

    static SceneFader()
    {
        Debug.Log("Scene Fader Created!");
    }

    public static void SomeMethod()
    {
        
    }

    public static IEnumerator FadeOut(float duration, string nextScene = null)
    {
        float fadeAlpha = 0;

        sprRenderer.enabled = true;
        sprRenderer.sprite = black;
        collider.enabled = true;
        while (fadeAlpha != 1)
        {
            // 시작시간과 지나가는 시간의 차이 / 지속시간 
            fadeAlpha = Mathf.MoveTowards(fadeAlpha, 1, Time.unscaledDeltaTime * (1 / duration));
            sprRenderer.color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }

        fadeAlpha = 1;
        sprRenderer.color = new Color(0, 0, 0, fadeAlpha);
        collider.enabled = true;
        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }
}