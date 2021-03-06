using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SheepTest
    {
    
        [UnityTest]
        public IEnumerator MoveNorth()
        {
            var gameObject = new GameObject();
            var sheep = gameObject.AddComponent<Sheep>();
        
            sheep.Move(Vector3.forward);
        
            yield return new WaitForSeconds(Sheep.MoveDuration);
        
            Assert.AreEqual(new Vector3(0, 1, 0), gameObject.transform.position);
        }
    
        [UnityTest]
        public IEnumerator MoveEast()
        {
            var gameObject = new GameObject();
            var sheep = gameObject.AddComponent<Sheep>();
        
            sheep.Move(Vector3.down);
        
            yield return new WaitForSeconds(Sheep.MoveDuration);
        
            Assert.AreEqual(new Vector3(1, 0, 0), gameObject.transform.position);
        }
    
        [UnityTest]
        public IEnumerator MoveSouth()
        {
            var gameObject = new GameObject();
            var sheep = gameObject.AddComponent<Sheep>();
        
            sheep.Move(Vector3.down);
        
            yield return new WaitForSeconds(Sheep.MoveDuration);
        
            Assert.AreEqual(new Vector3(0, -1, 0), gameObject.transform.position);
        }
    
        [UnityTest]
        public IEnumerator MoveWest()
        {
            var gameObject = new GameObject();
            var sheep = gameObject.AddComponent<Sheep>();
        
            sheep.Move(Vector3.down);
        
            yield return new WaitForSeconds(Sheep.MoveDuration);
        
            Assert.AreEqual(new Vector3(-1, 0, 0), gameObject.transform.position);
        }
    }
}
