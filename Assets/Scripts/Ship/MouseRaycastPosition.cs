using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Set the position of the Waypoint object that is get from the position of the mouse casted in the world via a raycast
    /// </summary>
    public class MouseRaycastPosition : MonoBehaviour
    {
        [SerializeField]
        // The Transform that needs to change when the mouse moves
        private Transform waypoint;

        // Setter of the waypoint position
        private Vector3 waypointPositionCurrent
        {
            set
            {
                // If the waypoint exists, run the code
                if (waypoint)
                {
                    // Check if the new position is not the same as the old one. And check if it is not Zero
                    if (value != waypointOldPosition && value != Vector3.zero)
                    {
                        // Dont change the y position
                        value.y = waypoint.transform.position.y;

                        // Set the position of the waypoint
                        waypoint.transform.position = value;
                    }
                }
            }
        }

        // This is used to check if the new position is not the same as the old position
        private Vector3 waypointOldPosition;

        // Update is called once per frame
        void Update()
        {
            // Set the current position of the waypoint to that of the: Mouse position on the water
            waypointPositionCurrent = GetMouseToWorldPosition();
        }

        /// <summary>
        /// Get the position of the mouse casted in the world via a raycast
        /// </summary>
        /// <returns>Vector3 The hit postition of the raycast to an collider or an empty Vector3 when nothing is hit</returns>
        public Vector3 GetMouseToWorldPosition()
        {
            RaycastHit hit;
            // Cast a ray from the screen to the world
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Get the position where the raycast has hit the collider
                Vector3 objectHitPosition = hit.point;

                // Return the hit position 
                return objectHitPosition;
            }

            // Return an empty Vector3 when nothing is hit
            return new Vector3();
        }
    }
}
