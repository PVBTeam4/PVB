using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField]
    private Vector3 directionScale;

    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.Translate(directionScale * Time.deltaTime, Space.World);
    }
}
