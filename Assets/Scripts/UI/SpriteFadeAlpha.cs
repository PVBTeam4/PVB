using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFadeAlpha : MonoBehaviour
{
    [SerializeField]
    // Time before the fade starts in Secconds
    private float timeBeforeFade = 2;

    [SerializeField]
    // How long it will take to dissapear completely in Secconds
    private float timeToDissapear = 2;

    private float fadeAmountCurrent = 0;

    //private bool canFade = true;

    private Image spriteRenderer;

    public Action UISpriteFadeEvent;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<Image>();

        if (!spriteRenderer)
        {
            Debug.LogError("No SpriteRenderer found");
            return;
        }

        // Reset the alpha
        SetSpriteAlpha(spriteRenderer, 0);

        // Subscribe the event
        UISpriteFadeEvent += StartFade;
    }

    /// <summary>
    /// Starts the fade logic
    /// </summary>
    /// <returns></returns>
    public void StartFade()
    {
        if (spriteRenderer)
        {
            // Reset the alpha
            SetSpriteAlpha(spriteRenderer, 1);

            // Start the StartFadeTimer
            StopAllCoroutines();
            StartCoroutine(StartFadeTimer(timeBeforeFade));
        }
    }

    private void SetSpriteAlpha(Image _spriteRenderer, float amount)
    {
        if (_spriteRenderer)
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, amount);
        else
            Debug.LogError("No SpriteRenderer found");
    }

    /// <summary>
    /// Timer
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator StartFadeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        StartCoroutine(SpriteFade(spriteRenderer, 0, timeToDissapear));
    }

    /// <summary>
    /// Function that will set the fade over time
    /// </summary>
    /// <param name="_spriteRenderer"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator SpriteFade(Image _spriteRenderer, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = 1;// _spriteRenderer.color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fadeAmountCurrent = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            SetSpriteAlpha(_spriteRenderer, fadeAmountCurrent);

            //_spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
}
