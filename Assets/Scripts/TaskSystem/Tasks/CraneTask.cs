using System;
using System.Collections;
using Global;
using Values;
using UnityEngine;
using Properties.Tags;

public class CraneTask : MonoBehaviour
{
    [SerializeField, TagSelector]
    private string intelTagName;

    [SerializeField, TagSelector]
    private string inteltagNameUI;

    [Header("On Task Completion")]

    [SerializeField]
    public GameObject[] objectsToEnable;

    [SerializeField]
    public GameObject[] objectsToDisable;

    // Private
    private int _intelAmountToCollect = 0;

    private int _intelAmountCurrent = 0;

    public Action<int> onTaskCompleted;

    public Action<int> onIntelCollected;

    public Action<int, int> updateIntelUI;

    public void Start()
    {
        // Get the amount of intel to collect
        int intelAmount = GameObject.FindGameObjectsWithTag(intelTagName).Length;

        if (intelAmount == 0)
        {
            Debug.LogError("No intel objects with tagname: " + intelTagName + ", found in scene");
        }

        _intelAmountToCollect = intelAmount;

        // Subscribe functions to events
        onIntelCollected += CollectIntel;
        onTaskCompleted += TaskCompleted;

        GameObject _intelCollectedUI = GameObject.FindGameObjectWithTag(inteltagNameUI);

        if (_intelCollectedUI)
        {
            updateIntelUI += _intelCollectedUI.GetComponent<UpdateUICounter>().updateCounter;

            // Update the UI
            updateIntelUI.Invoke(_intelAmountCurrent, _intelAmountToCollect);
        }
        else
            Debug.LogError("No IntelUI object found with tag: " + inteltagNameUI);
    }

    /// <summary>
    /// Will run when the "onIntelCollected" event is called
    /// </summary>
    /// <param name="value"></param>
    private void CollectIntel(int value)
    {
        _intelAmountCurrent++;

        // Update the UI
        updateIntelUI.Invoke(_intelAmountCurrent, _intelAmountToCollect);

        Debug.Log("Intel collected");

        if (_intelAmountCurrent == _intelAmountToCollect)
            onTaskCompleted.Invoke(0);
    }

    /// <summary>
    /// Will run when the "onTaskCompleted" event is called
    /// </summary>
    /// <param name="value"></param>
    private void TaskCompleted(int value)
    {
        Debug.Log("TASK COMPLETED");

        // Call the intel task completed event
        OverworldTasks overworldTasks = FindObjectOfType<OverworldTasks>();
        if (overworldTasks) overworldTasks.CompleteTaskEvent.Invoke(1);

        // Disable / Enable objects
        ToggleObjects(objectsToEnable, true);
        ToggleObjects(objectsToDisable, false);
    }

    /// <summary>
    /// Enables or Disables all the objects in the list depending on the given state
    /// </summary>
    /// <param name="gameObjects"></param>
    /// <param name="state"></param>
    private void ToggleObjects(GameObject[] gameObjects, bool state)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(state);
        }
    }
}
