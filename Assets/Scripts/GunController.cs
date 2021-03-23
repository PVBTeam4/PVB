using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject BulletSpawnLocation;
    [SerializeField]
    private GameObject BulletList;

    void Update()
    {
        Rotate();
        Shoot();
    }

    void Rotate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(new Vector3(0, 0, xInput * -1) * rotationSpeed * Time.deltaTime);

    }
        
    void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(Bullet, BulletSpawnLocation.transform.position, transform.rotation).transform.parent = BulletList.transform;
        }
    }
}


