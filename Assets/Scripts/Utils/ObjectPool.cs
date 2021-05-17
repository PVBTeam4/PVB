using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Objectpool of commonly used prefabs.
    /// </summary>
    [AddComponentMenu("Utils/ObjectPool")]
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }

        #region member

        /// <summary>
        /// Member class for a prefab entered into the object pool
        /// </summary>
        /// ObjectPool.instance.GetObjectForType("Cube", true); <---use to call 
        [Serializable]
        public class ObjectPoolEntry
        {
            /// <summary>
            /// the object to pre instantiate
            /// </summary>
            [SerializeField] private GameObject _prefab;

            public GameObject Prefab
            {
                get { return _prefab; }

                set { _prefab = value; }
            }

            /// <summary>
            /// quantity of object to pre-instantiate
            /// </summary>
            [SerializeField] private int _count;

            public int Count
            {
                get { return _count; }
                set { _count = value; }
            }
        }

        #endregion

        /// <summary>
        /// The object prefabs which the pool can handle
        /// </summary>
        [SerializeField] private ObjectPoolEntry[] entries;

        /// <summary>
        /// The pooled objects currently available
        /// </summary>
        private Dictionary<String, List<GameObject>> _pool;

        /// <summary>
        /// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
        /// </summary>
        private GameObject _containerObject;

        private void OnEnable()
        {
            Instance = this;
            
            _containerObject = gameObject;
            
            CreatePool();
        }

        private void CreatePool()
        {
            _pool = new Dictionary<string, List<GameObject>>();

            for (int i = 0; i < entries.Length; i++)
            {
                var currentPrefab = entries[i];
                _pool[currentPrefab.Prefab.name] = new List<GameObject>();

                for (int n = 0; n < currentPrefab.Count; n++)
                {
                    var newObject = CreateObject(currentPrefab.Prefab);
                    PoolObject(newObject);
                }
            }
        }

        private GameObject CreateObject(GameObject prefab)
        {
            var newObject = Instantiate(prefab) as GameObject;

            newObject.name = prefab.name;

            return newObject;
        }


        /// <summary>
        /// Gets an object from the object pool
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="onlyPooled"></param>
        /// <returns></returns>
        public GameObject GetObject(string objectName, bool onlyPooled = false)
        {
            foreach (var poolKey in _pool.Keys)
            {
                Debug.Log(poolKey);
            }

            if (!_pool.ContainsKey(objectName))
            {
                return null;
            }

            var targetPool = _pool[objectName];

            GameObject pooledObject = GetObjectFromPool(targetPool);

            if (pooledObject || onlyPooled)
            {
                return pooledObject;
            }

            var prefab = FindPrefab(objectName);
            GameObject newObject = CreateObject(prefab);
            return newObject;

        }

        private GameObject GetObjectFromPool(List<GameObject> targetPool)
        {
            if (targetPool.Count == 0)
            {
                return null;
            }

            var firstObjectInPool = targetPool[0];
            targetPool.RemoveAt(0);
            firstObjectInPool.transform.parent = null;
            firstObjectInPool.SetActive(true);
            return firstObjectInPool;
        }

        GameObject FindPrefab(String prefabName)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Prefab.name == prefabName)
                {
                    return entries[i].Prefab;
                }
            }

            return null;
        }

        /// <summary>
        /// Pools the object specified.  Will not be pooled if there is no prefab of that type.
        /// </summary>
        /// <param name='obj'>
        /// Object to be pooled.
        /// </param>
        public void PoolObject(GameObject obj)
        {
            if (!_pool.ContainsKey(obj.name))
            {
                return;
            }

            _pool[obj.name].Add(obj);

            obj.transform.parent = _containerObject.transform;

            obj.SetActive(false);
        }
    }
}
