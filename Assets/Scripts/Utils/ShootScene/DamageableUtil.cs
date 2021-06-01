using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.ShootScene
{
    public static class DamageableUtil
    {
        private static readonly string[] DamageableTags = {"ShipsKilledUI", "Enemy", "Explosive"};

        public static bool IsGameObjectDamageable(GameObject gameObject)
        {
            return DamageableTags.Contains(gameObject.tag);
        }

        public static GameObject[] GetDamageableGameObjectsWithinRange(Vector3 originPosition, float range)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (string damageableTag in DamageableTags)
            {
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag(damageableTag)
                    // Only add GameObjects that are within given range
                    .Where(gameObject => Vector3.Distance(originPosition, gameObject.transform.position) <= range));
            }
            return gameObjects.ToArray();
        }
    }
}
