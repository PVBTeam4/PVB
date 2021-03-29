using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image HP;

    void OnTriggerEnter(Collider col)
    {   
        //decrease HP for UI
        HP.fillAmount -= 0.1f;

        if(HP.fillAmount <= 0)
        {
            //Destroy the gameObject after hp is 0
            Destroy(gameObject);
        }
    }
}
