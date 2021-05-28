using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldTasks : MonoBehaviour
{
    [SerializeField]
    private GameObject[] taskLines;

    public Action<int> CompleteTaskEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Disable all task lines
        ToggleObjects(taskLines, false);

        // Subscribe a function to the CompleteTaskEvent
        CompleteTaskEvent += CompleteTask;
    }

    /// <summary>
    /// Enables a line object from the task lines array
    /// </summary>
    /// <param name="taskIndex"></param>
    private void CompleteTask(int taskIndex)
    {
        ToggleObject(taskLines[taskIndex], true);
    }

    /// <summary>
    /// Enables or Disables all the objects in the list depending on the given state
    /// </summary>
    /// <param name="gameObjects"></param>
    /// <param name="state"></param>
    private void ToggleObjects(GameObject[] gameObjects, bool state)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Enables or Disables a given GameObject depending on the given state
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="state"></param>
    private void ToggleObject(GameObject gameObject, bool state)
    {
        gameObject.SetActive(state);
    }
}
