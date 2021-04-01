using UnityEngine;

public class Sheep : MonoBehaviour
{
    public static readonly float MoveDuration = 1.5f; // 1.5 seconds

    public void Move(Direction direction)
    {
        Debug.Log("Move direction: " + direction.Vector);

        gameObject.transform.position += direction.Vector;
    }
}
