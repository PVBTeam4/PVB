using System.Collections.Generic;
using System.Linq;
using Global;
using TaskSystem;
using TaskSystem.Objectives;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    /// <summary>
    /// this class causes the enemies to spawn 
    /// before spawning it will get all spawn points and will pick one at random
    /// then it will spawn as the child of the EnemyHolderObject
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        // Enemy prefab
        private GameObject[] Prefab;

        // GameObject with spawning points
        private Transform[] _spawnPoints;

        //hold all enemies that are Instantiate
        private GameObject _enemyHolderObject;

        //to prevent them from spawning in the same position
        private int _currentSpawnIndex, _previousSpawnIndex, _SpawnIndex;

        [SerializeField]
        // the time that it wil be spawned
        private int spawnTimeInSeconds = 7;

        // Whether enemies should be spawning
        private bool _shouldSpawnEnemies = true;

        private void Awake()
        {
            InitializeBulletHolder();

            TaskController.TaskEndedAction += OnTaskEnd;
        }
        /// <summary>
        /// Creates Enemy holder, that holds all Instantiated Enemies
        /// </summary>
        private void InitializeBulletHolder()
        {   //name the game object Enemy Holder
            _enemyHolderObject = new GameObject { name = "Enemy Holder" };
        }

        // Start is called before the first frame update
        void Start()
        {
            GetAllChildrenOfThisObject();
            InvokeRepeating("SpawnEnemies", 1, spawnTimeInSeconds);

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
        /// this function first gets a random index
        /// and check if the currentSpawnIndex is the same as the previousSpawnIndex, 
        /// if it is it should call itself to recalculate
        /// if not, set the currentSpawnIndex to the previousSpawnIndex one 
        /// add a nummber to the enemycount
        /// Instances of a new enemy with this current SpawnPoints using the currentSpawnIndex
        /// then set the EnemyHolderObject as the parent for this enemy
        /// cancel this function if enemies are greater than or equal to the maximum spawn
        /// </summary>
        private void SpawnEnemies()
        {
            if (!_shouldSpawnEnemies) return;
            //get any index from range 0 to length of spawning points
            _currentSpawnIndex = Random.Range(0, _spawnPoints.Length);

            //check if the currentSpawnIndex is the same as the previousSpawnIndex
            if (_currentSpawnIndex == _previousSpawnIndex)
            {   // call this function to get a different index
                SpawnEnemies();
                return;
            }
            //set the current index to the previous index
            _previousSpawnIndex = _currentSpawnIndex;

            _SpawnIndex = Random.Range(0, Prefab.Length);
            print(_SpawnIndex);
            // Instantiate a new EnemyPrefab on the the current spawn point. 
            // And set the parent to that of the EnemyHolderObject
            Instantiate(Prefab[_SpawnIndex], _spawnPoints[_currentSpawnIndex].position, Prefab[_SpawnIndex].transform.rotation).transform.parent = _enemyHolderObject.transform;
        }

        private void OnTaskEnd(ToolType toolType, bool isCompleted)
        {
            if (toolType != ToolType.CANNON) return;

            DestroyAllEnemies();

            _shouldSpawnEnemies = false;
            CancelInvoke(nameof(SpawnEnemies));
        }

        private void DestroyAllEnemies()
        {
            foreach (KillObjective killObjective in GetAllEnemies())
            {
                killObjective.DamageBy(killObjective.maxHealth, false);
            }
        }

        private KillObjective[] GetAllEnemies()
        {
            return FindObjectsOfType<KillObjective>();
        }
    }
}
