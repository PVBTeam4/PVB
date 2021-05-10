using Global;
using UnityEngine;

namespace TaskSystem
{
    public class TaskFail : MonoBehaviour
    {
        public void FailTask()
        {
            GameManager.Instance.TaskController.CancelActiveTask();
        }
    }
}
