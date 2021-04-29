using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    //Enemy prefab
    private GameObject EnemyPrefab;

    [SerializeField]
    //gameobject with spawningpoints
    private Transform[] SpawnPoints;

    //hold all Enemies
    private GameObject EnemyHolderObject;
    private int currentSpawnIndex, previousSpawnIndex, count;

    private void Awake()
    {
        InitializeBulletHolder();
    }
    /// <summary>
    /// Creates Enemy  holder, that holds all Instantiated Enemys
    /// </summary>
    private void InitializeBulletHolder()
    {
        EnemyHolderObject = new GameObject { name = "Enemy Holder" };
    }
    // Start is called before the first frame update
    void Start()
    {
        GetAllChildrenOfThisObject();
        InvokeRepeating("SpawnEnemies", 1, 7);

    }

    /// <summary>
    /// Creates transfrom (children of this object, and remove parrent)
    /// </summary>
    private void GetAllChildrenOfThisObject()
    {
        var transforms = new HashSet<Transform>(GetComponentsInChildren<Transform>(true));
        transforms.Remove(transform);
        SpawnPoints = transforms.ToArray();
        count = SpawnPoints.Length;

    }

    /// <summary>
    /// spawn object in  different spawning points
    /// </summary>
    private void SpawnEnemies()
    {
        currentSpawnIndex = Random.Range(0, count);
        print(currentSpawnIndex);
        if(currentSpawnIndex == previousSpawnIndex) 
        {
            SpawnEnemies();
            return;
        }
        previousSpawnIndex = currentSpawnIndex;

        Instantiate(EnemyPrefab, SpawnPoints[currentSpawnIndex].position, EnemyPrefab.transform.rotation).transform.parent = EnemyHolderObject.transform;

    }
}
