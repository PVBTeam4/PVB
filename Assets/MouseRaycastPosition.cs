using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycastPosition : MonoBehaviour
{
    [SerializeField]
    private Transform waypoint;

    private Vector3 waypointOldPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint)
        {
            Vector3 _mouseHitPosition = GetMouseToWaterPosition();
            

            if (_mouseHitPosition != waypointOldPosition && _mouseHitPosition != Vector3.zero)
            {
                _mouseHitPosition.y = 0f;

                waypoint.transform.position = _mouseHitPosition;
            }
                
        }
    }

    public Vector3 GetMouseToWaterPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 objectHitPosition = hit.point;
            return objectHitPosition;

            // Do something with the object that was hit by the raycast.
        }

        return new Vector3();
    }
}
