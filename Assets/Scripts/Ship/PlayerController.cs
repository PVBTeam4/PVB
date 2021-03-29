using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the player movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float trust = 34000;

    [SerializeField]
    private float turningSpeed = 1;

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Get the components we need
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the ship using the Horizontal input axis
        RotatePlayer(turningSpeed, Input.GetAxis("Horizontal"));

        // Move the ship in the direction its heading
        MovePlayerInDirection(trust, Input.GetAxis("Vertical"));
    }

    /// <summary>
    /// Rotates the player
    /// </summary>
    /// <param name="_turningSpeed">How fast it needs to turn</param>
    /// <param name="_directionScale">-1 is Left. 1 Is Right</param>
    private void RotatePlayer(float _turningSpeed, float _directionScale)
    {
        float _turningSpeedDelta = _turningSpeed * Mathf.Abs(rigidbody.velocity.magnitude);
        float angle = (_turningSpeedDelta * _directionScale) * Time.deltaTime;

        transform.Rotate(0, angle, 0);
    }

    /// <summary>
    /// Move the player in the direction it's facing
    /// </summary>
    /// <param name="_thrust">Amount of force it will be applied</param>
    /// <param name="_thrustScale">-1 is Backwards. 1 Is Forward</param>
    private void MovePlayerInDirection(float _thrust, float _thrustScale)
    {
        // Move the player in the direction it's facing
        rigidbody.AddRelativeForce((Vector3.forward * _thrust * Time.fixedDeltaTime) * _thrustScale);

        // Clamp (Dont let the value exeed) the velocity so it does not go faster
        Vector3 _maxVelocity = new Vector3(_thrust, _thrust, _thrust);
        rigidbody.velocity.Clamp(_maxVelocity.Multiply(-1), _maxVelocity);
    }
}
