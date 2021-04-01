using UnityEngine;

public class Direction
{
    public readonly static Direction NORTH = new Direction(new Vector3(0, 1, 0));
    public readonly static Direction EAST = new Direction(new Vector3(1, 0, 0));
    public readonly static Direction SOUTH = new Direction(new Vector3(0, -1, 0));
    public readonly static Direction WEST = new Direction(new Vector3(-1, 0, 0));

    public Vector3 Vector { get; }

    private Direction(Vector3 vector)
    {
        this.Vector = vector;
        
    }
}

