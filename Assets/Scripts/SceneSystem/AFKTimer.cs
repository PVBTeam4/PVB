using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AFKTimer : MonoBehaviour
{
    [SerializeField]
    private float AFKLimit = 60;

    [SerializeField]
    private string mainMenuName = "Main Menu";

    private float _timer;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != mainMenuName)
        {
            _timer += Time.deltaTime;

            if (_timer >= AFKLimit)
            {
                _timer = 0;
                SceneManager.LoadScene(mainMenuName);
                Cursor.visible = true;
            }

            if (UnityEngine.Input.anyKey)
            {
                _timer = 0;
            }
        }

    }

}
