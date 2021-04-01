using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that utilizes an 'OnTriggerEnter' event to switch to a given scene based on a given playername. This is used on colliders/gameobjects 
/// that each correspond with a certain scene.
/// </summary>
public class TaskTrigger : MonoBehaviour
{
    //string field to input the name of the player into
    [SerializeField]
    private string playerName;

    //string field to input the name of a scene into
    [SerializeField]
    private string scene;

    /// <summary>
    /// 'OnTriggerEnter' event to detect the player colliding with this object
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
}
