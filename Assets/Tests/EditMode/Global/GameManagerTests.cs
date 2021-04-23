using Global;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Global
{
    public class GameManagerTests
    {

        [Test]
        public void EnterAndLeaveTaskMode()
        {
            GameObject gameObject = new GameObject();
            GameManager gameManager = gameObject.AddComponent<GameManager>();
            
            gameManager.OnEnterTaskMode(ToolType.CANNON);
            
            Assert.IsNotNull(gameManager.TaskController);
            Assert.IsNotNull(gameManager.ToolController);
            
            gameManager.OnEnterOverWorld();
        }
    }
}
