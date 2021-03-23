using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;
    [SerializeField]
    private float lifeSpan = 3;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

    }
}