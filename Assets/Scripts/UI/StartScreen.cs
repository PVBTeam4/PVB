using Global;
using SceneSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        [SerializeField]
        private string gameSceneName;
        public void OnPlay()
        {
            //SceneManager.LoadScene(gameSceneName);
            StartCoroutine("loadNewLevel", gameSceneName);
        }

        IEnumerator loadNewLevel(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            float fadeSpeed = 0.01f;

            GameObject.Find("Play Button").SetActive(false);

            for (float ft = 0f; ft <= 1; ft += fadeSpeed)
            {
                Color col = GameObject.Find("FadeScreen").GetComponent<UnityEngine.UI.Image>().color;
                GameObject.Find("FadeScreen").GetComponent<UnityEngine.UI.Image>().color = new Color(col.r, col.g, col.b, ft);
                yield return null;
            }
            operation.allowSceneActivation = true;
        }
    }
}
