using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    //Reference to the main menu
    [SerializeField] private string mainMenu;

    void Start()
    {
        StartCoroutine("playVideo");
    }

    /// <summary>
    /// Load both the next scene and the video to ensure that both are executed fast and smoothly
    /// </summary>
    /// <returns></returns>
    IEnumerator playVideo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(mainMenu);
        operation.allowSceneActivation = false;

        GetComponent<VideoPlayer>().Prepare();
        while(!GetComponent<VideoPlayer>().isPrepared)
        {
            yield return null;
        }
        GetComponent<VideoPlayer>().Play();

        while (GetComponent<VideoPlayer>().isPlaying)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}
