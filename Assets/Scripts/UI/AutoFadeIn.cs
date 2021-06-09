using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.01f;

    private void Awake()
    {
        StartCoroutine("fadeIn");
    }

    IEnumerator fadeIn()
    {
        for (float ft = 0f; ft <= 1; ft += fadeSpeed)
        {
            Color col = GetComponent<UnityEngine.UI.Image>().color;
            GetComponent<UnityEngine.UI.Image>().color = new Color(col.r, col.g, col.b, ft);
            yield return null;
        }
    }
}
