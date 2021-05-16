using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        //Debug.Log("Start: SceneLoaded1");
    }
    // Update is called once per frame
    void Update()
    {   
        if(UnityEngine.Input.GetMouseButton(0))
        {
            //animator.SetTrigger("FadeOut");
        }
        if (UnityEngine.Input.GetMouseButton(1))
        {
            //animator.SetTrigger("FadeIn");

        }


    }
    private void OnSceneUnloaded(Scene current)
    {
        //Debug.Log("OnSceneUnloaded: " + current);
        animator.SetTrigger("FadeIn");


    }



}
