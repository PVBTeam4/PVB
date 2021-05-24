using System.Collections.Generic;
using System.Linq;
using Global;
using TaskSystem;
using TaskSystem.Objectives;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boat
{
    /// <summary>
    /// this class causes all boats to spawn 
    /// before spawning it will get all spawn points and will pick one at random
    /// then it will spawn as the child of the EnemyHolderObject
    /// </summary>
    public class BoatSpawner : MonoBehaviour
    {
        [SerializeField]
        // Enemy prefab
        private GameObject[] PrefabList;

        // GameObject with spawning points
        private Transform[] _spawnPoints;

        //holds all enemies that are Instantiated
        private GameObject _boatHolderObject;

        //to prevent them from spawning in the same position
        private int _currentSpawnIndex, _previousSpawnIndex;

        [SerializeField]
        // the time that it wil be spawned
        private int spawnTimeInSeconds = 7;

        //Amount of enemies that spawn before every refugee ship
        [SerializeField]
        private int enemiesPerRefugeeShip = 4;

        //[SerializeField]
        private int refugeeBoatAmount;// = 3;

        // Whether boats should be spawning
        private bool _shouldSpawnBoats = true;

        //Keeps track of the amount of spawned enemies
        private int _enemiesLeftToSpawn, refugeeBoatsSpawned = 0;


        private void Awake()
        {
            InitializeBoatHolder();

            TaskController.TaskEndedAction += OnTaskEnd;

            //change this later perhaps
            refugeeBoatAmount = Values.TaskValues.RefugeeShipsToSave;
        }
        /// <summary>
        /// Creates Enemy holder, that holds all Instantiated Enemies
        /// </summary>
        private void InitializeBoatHolder()
        {   //name the game object Enemy Holder
            _boatHolderObject = new GameObject { name = "Enemy Holder" };
        }

        // Start is called before the first frame update
        void Start()
        {
            GetAllChildrenOfThisObject();
            InvokeRepeating("SpawnBoats", 1, spawnTimeInSeconds);

            _enemiesLeftToSpawn = enemiesPerRefugeeShip;
        }

        /// <summary>
        /// Creates a HashSet with the transform component from these children and removes the parent
        /// then change the hash set into an array
        /// </summary>
        private void GetAllChildrenOfThisObject()
        {
            //a HashSet is a collection that contains no duplicate elements, and whose elements are in no particular order.
            //HashSet<T> class can be thought of as a Dictionary<TKey,TValue> collection without values.
            var transforms = new HashSet<Transform>(GetComponentsInChildren<Transform>(true));

            // remove the first transform with is the parent of these children
            transforms.Remove(transform);

            if (transforms.Count == 0)
            {
                Debug.LogError("Enemy Spawner doesn't hold any children for spawn points!");
                return;
            }

            //create an array from this HashSet
            _spawnPoints = transforms.ToArray();
        }


        /// <summary>
        /// This function spawns either an enemy ship or a refugee ship onto a random spawning point
        /// </summary>
        private void SpawnBoats()
        {
            if (!_shouldSpawnBoats) return;
            //get any index from range 0 to length of spawning points
            _currentSpawnIndex = Random.Range(0, _spawnPoints.Length);            

            //check if the currentSpawnIndex is the same as the previousSpawnIndex
            if (_currentSpawnIndex == _previousSpawnIndex)
            {   // call this function to get a different index
                SpawnBoats();
                return;
            }
            //set the current index to the previous index
            _previousSpawnIndex = _currentSpawnIndex;

            if(_enemiesLeftToSpawn == enemiesPerRefugeeShip/* && refugeeBoatsSpawned < refugeeBoatAmount*/)
            {
                Instantiate(PrefabList[1], _spawnPoints[_currentSpawnIndex].position, PrefabList[1].transform.rotation).transform.parent = _boatHolderObject.transform;
                _enemiesLeftToSpawn--;
                refugeeBoatsSpawned++;
            }else
            {
                Instantiate(PrefabList[0], _spawnPoints[_currentSpawnIndex].position, PrefabList[0].transform.rotation).transform.parent = _boatHolderObject.transform;
                _enemiesLeftToSpawn = _enemiesLeftToSpawn == 0 ? enemiesPerRefugeeShip : _enemiesLeftToSpawn - 1;
            }
        }

        private void OnTaskEnd(ToolType toolType, bool isCompleted)
        {
            if (toolType != ToolType.CANNON) return;

            DestroyAllEnemies();

            _shouldSpawnBoats = false;
            CancelInvoke(nameof(SpawnBoats));
        }

        private void DestroyAllEnemies()
        {
            foreach (KillableObject killableObject in GetAllBoats())
            {
                if(killableObject.CompareTag("Enemy"))
                {
                    killableObject.DamageBy(killableObject.maxHealth, false);
                }
                else
                {
                    Destroy(killableObject);
                }
            }
        }

        private KillableObject[] GetAllBoats()
        {
            return FindObjectsOfType<KillableObject>();
        }
    }
}
