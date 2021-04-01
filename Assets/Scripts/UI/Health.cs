using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;
/// <summary>
/// This class is designed to have lifetime in the game
/// this shows the hp of the character it is added to
/// hp loses when hit by a trigger prefab and when hp is 0 it wi be destroyed
/// for now i have used this class for enemy and the trigger is the prefab (BulletForShip)
/// </summary>
public class Health : MonoBehaviour
{   
    [SerializeField]
    // is an image that shows the character's life
    private Image healthImage;

    /// <summary>
    ///if it interacts with an object that has trigger enabled, it will activate this function to decrease de life
    /// after the life is zero it wil be destroyed
    /// </summary>
    private void OnTriggerEnter(Collider col)
    {
        //decrease HP for UI
        healthImage.fillAmount -= 0.1f;

        //if healthImage is lower or equal to zero destroy it self
        if (healthImage.fillAmount <= 0)
        {
            //Destroy the gameObject after hp is 0
            Destroy(gameObject);
        }
    }
}
