using Global;
using UnityEngine;

namespace TaskSystem
{
    public class TaskFail : MonoBehaviour
    {
        public void FailTask()
        {
            if (GameManager.Instance == null || GameManager.Instance.TaskController == null)
            {
                Debug.LogError("TaskController is null, be sure to enter this Task from the OverWorld!");
                return;
            } 
            GameManager.Instance.TaskController.CancelActiveTask();
        }
    }
}
