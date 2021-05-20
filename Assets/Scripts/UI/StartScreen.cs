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
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
