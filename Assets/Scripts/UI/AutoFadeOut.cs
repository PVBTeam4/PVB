using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.01f;

    private void Awake()
    {
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= fadeSpeed)
        {
            Color col = GetComponent<UnityEngine.UI.Image>().color;
            GetComponent<UnityEngine.UI.Image>().color = new Color(col.r, col.g, col.b, ft);
            yield return null;
        }
    }
}
