using UnityEngine;

namespace UI
{
    public class FollowCursor : MonoBehaviour
    {
        
        /**
         * Update cursor every frame
         */
        void Update()
        {
            // Make cursor invisible
            Cursor.visible = false;
            // Set position of transform to mousePosition on screen
            transform.position = UnityEngine.Input.mousePosition;
        }
    }
}
