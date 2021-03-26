using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTaskMode : MonoBehaviour
{

    [SerializeField]
    private string shootingScene, extinguishingScene, craneOperatingScene;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if(other.gameObject.name == "ShootingTask")
        {
            SceneManager.LoadScene(shootingScene.ToString(), LoadSceneMode.Single);
        }
        else if(other.gameObject.name == "ExtinguishingTask")
        {
            SceneManager.LoadScene(extinguishingScene.ToString(), LoadSceneMode.Single);
        }
        else if(other.gameObject.name == "CraneOperatingTask")
        {
            SceneManager.LoadScene(craneOperatingScene.ToString(), LoadSceneMode.Single);
        }
    }

}
