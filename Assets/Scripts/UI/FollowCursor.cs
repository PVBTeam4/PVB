using Global;
using UnityEngine;

namespace UI
{
    public class FollowCursor : MonoBehaviour
    {
        private bool cursorVisible = false;

        private void OnEnable()
        {
            TaskSystem.TaskController.TaskEndedAction += TaskEnded;
        }

        private void OnDisable()
        {
            TaskSystem.TaskController.TaskEndedAction -= TaskEnded;
        }

        /**
         * Update cursor every frame
         */
        void Update()
        {
            // Make cursor invisible
            Cursor.visible = cursorVisible;
            // Set position of transform to mousePosition on screen
            transform.position = UnityEngine.Input.mousePosition;
        }

        private void TaskEnded(ToolType nextOverWorldIndex, bool isTaskSuccess)
        {
            cursorVisible = true;
        }
    }
}
