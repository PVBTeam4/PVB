using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailing : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    private float direction = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateShip(1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionScale">-1 is Left. 1 Is Right</param>
    private void RotateShip(float directionScale)
    {
        Quaternion _rotation = transform.rotation;
        //_rotation.y = GetRotationFormMousePosition().y * -1;

        transform.rotation = _rotation;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_speed"></param>
    /// <param name="_direction">Direction in wich the ship will move</param>
    private void MoveShip(float _speed, float _direction)
    {
        
    }

}
