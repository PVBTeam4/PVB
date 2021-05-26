using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBackBumper : MonoBehaviour
{   
    [SerializeField]
    //amount of force to bounce back
    private float bounceForce = 100;

    private Rigidbody PlayerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
         PlayerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "BounceBack")
        {   

            PlayerRigidbody.AddExplosionForce(bounceForce, collision.contacts[0].point, 0);
        }
    }
}
