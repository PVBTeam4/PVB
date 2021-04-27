using UnityEngine;

public class Sheep : MonoBehaviour
{
    public static readonly float MoveDuration = 1.5f; // 1.5 seconds

    public void Move(Vector3 vector3)
    {
        Debug.Log("Move direction: " + vector3);

        gameObject.transform.position += vector3;
    }
}
