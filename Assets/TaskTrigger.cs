using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField]
    private string playerName;

    [SerializeField]
    private string scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

}
