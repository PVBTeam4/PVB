using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Private variable to hold this instance of the GameManager Class
    private GameManager instance;

    // Returns this instance of the GameManager Class
    public GameManager Instance { get { return instance; } }

    // Awake is called before Start
    void Awake()
    {
        // Set the instance to this GameManager Class
        instance = this;
    }

    // Switches to the right Scene via the ToolType enum
    public void EnterTaskMode(ToolType toolType)
    {
        //TODO Load the right scene
    }

    // Switches back to the Overworld Scene
    private void EnterOverWorld()
    {
        //TODO Load the right scene
    }
}
