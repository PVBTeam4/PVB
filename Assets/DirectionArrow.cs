using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject thisObject;

    [SerializeField]
    private GameObject targetObject;

    private void Update()
    {
        Vector3 distanceVector = targetObject.transform.position - thisObject.transform.position;
        float distance = distanceVector.magnitude;

        float zPosDifference = targetObject.transform.position.z - thisObject.transform.position.z;
        float xPosDifference = targetObject.transform.position.x - thisObject.transform.position.x;

        float zAngle = Mathf.Asin(xPosDifference / distance);//Mathf.Atan(xPosDifference / zPosDifference);
        zAngle *= Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -zAngle + 90);
    }

}
