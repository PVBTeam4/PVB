using Global;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class GameManagerTests
    {
        [Test]
        public void OnEnterTaskMode()
        {
            GameObject gameObject = new GameObject();
            GameManager gameManager = gameObject.AddComponent<GameManager>();
            
            gameManager.OnEnterTaskMode(ToolType.CANNON);
            
            Assert.IsNotNull(gameManager.TaskController);
            Assert.IsNotNull(gameManager.ToolController);
        }
        
        [Test]
        public void OnEnterOverWorld()
        {
            GameObject gameObject = new GameObject();
            GameManager gameManager = gameObject.AddComponent<GameManager>();
            
            gameManager.OnEnterTaskMode(ToolType.CANNON);
            
            Assert.IsNotNull(gameManager.TaskController);
            Assert.IsNotNull(gameManager.ToolController);
            
            gameManager.OnEnterOverWorld();
            
            Assert.IsNull(gameManager.TaskController);
            Assert.IsNull(gameManager.ToolController);
        }
    }
}
