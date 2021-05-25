using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class is added to the canvas of the enemy and the enemy will look at the camera
/// </summary>
public class LookAtCamera : MonoBehaviour
{

    /// <summary>
    /// get canvas component set the rendermode to worldspace and add de main camera as world camera
    /// </summary>
    void Start()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
      
    }

    /// <summary>
    /// look in the direction of the camera
    /// </summary>
    void Update()
    {
        transform.LookAt(Camera.main.transform);

    }
}
