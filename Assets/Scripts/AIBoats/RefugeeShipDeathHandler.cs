using System.Collections;
using System.Collections.Generic;
using TaskSystem;
using UnityEngine;

public class RefugeeShipDeathHandler : MonoBehaviour
{
    [SerializeField]
    private int ShipsKilledForGameOver = 3;

    private int _shipsKilled = 0;

    private void OnRefugeeShipDeath()
    {
        _shipsKilled++;
        if(_shipsKilled >= ShipsKilledForGameOver)
        {
            gameObject.GetComponent<TaskFail>().FailTask();
        }
    }
}
