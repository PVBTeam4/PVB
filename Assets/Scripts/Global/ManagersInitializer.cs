using UnityEngine;

namespace Global
{
    /// <summary>
    /// This script will initialize the managers prefab,
    /// if they have not been initialized yet.
    /// </summary>
    public class ManagersInitializer : MonoBehaviour
    {
        // Prefab that hols all managers
        [SerializeField]
        private GameObject managersPrefab;
        // Represents if managers have been initialized
        private static bool _initialized;

        private void Awake()
        {
            TryToInitializeManagers();
        }

        /// <summary>
        /// Do a attempt to initialize managers.
        /// Destroy afterwards
        /// </summary>
        private void TryToInitializeManagers()
        {
            if (!_initialized)
            {
                _initialized = true;
                InitializeManagers();
            }
            
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Initialize managers prefab
        /// </summary>
        private void InitializeManagers()
        {
            Instantiate(managersPrefab);
        }
    }
}
