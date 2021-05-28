using System.Collections;
using System.Collections.Generic;
using Ship;
using UnityEngine;

public class PlayerBoatTilting : MonoBehaviour
{
    // Transform of the boat model that we will tilt 
    private Transform _boatModelTransform;

    [SerializeField]
    // How much the ship will tilt forward or backwards when the ship moves in that opposite direction
    private float modelTiltScaleForward = 1.3f;

    [SerializeField]
    // How much the ship will tilt sidewards when the ship rotates in that opposite direction
    private float modelTiltScaleSide = 4f;


    private PlayerMovement _playerMovement;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
        
        _rigidbody = transform.parent.GetComponent<Rigidbody>();
        
        // Get the transform of the boat model
        _boatModelTransform = gameObject.transform.GetComponent<Transform>();
        
        // Tilting effects
        TileShipSidewards(_rigidbody);

        TileShipForward(_rigidbody);
    }

    // Update is called once per frame
    void Update()
    {
        // Tilting effects
        TileShipSidewards(_rigidbody);

        TileShipForward(_rigidbody);
    }
    
    /// <summary>
    /// Tilts the ship sidewards by the sidewards (X axis) velocity
    /// </summary>
    private void TileShipSidewards(Rigidbody rigidbody)
    {
        float _scale = -modelTiltScaleSide;
        float _angulatVelocity = Mathf.Abs(rigidbody.angularVelocity.y);

        // Get the X velocity of the rigidbody from World to Local space
        _playerMovement.SidewardsSpeed = _boatModelTransform.InverseTransformDirection(rigidbody.velocity).x;

        // Scale the velocity
        float _sideVelocityScaled = _playerMovement.SidewardsSpeed * _scale;

        _boatModelTransform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, _sideVelocityScaled);
    }

    /// <summary>
    /// Tilts the ship forward by the forward (Z axis) velocity
    /// </summary>
    private void TileShipForward(Rigidbody rigidbody)
    {
        float _scale = modelTiltScaleForward;

        // Get the Z velocity of the rigidbody from World to Local space
        _playerMovement.ForwardSpeed = _boatModelTransform.InverseTransformDirection(rigidbody.velocity).z;

        // Scale the velocity
        float _forwardVelocityScaled = -_playerMovement.ForwardSpeed * _scale;

        _boatModelTransform.rotation = Quaternion.Euler(_forwardVelocityScaled, transform.eulerAngles.y, _boatModelTransform.eulerAngles.z);
    }
}
