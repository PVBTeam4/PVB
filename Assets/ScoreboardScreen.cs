using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardScreen : MonoBehaviour
{
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
