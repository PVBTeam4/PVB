using Global;
using Input;
using NUnit.Framework;
using ToolSystem;

namespace Tests.EditMode.ToolSystem
{
    public class ToolControllerTests
    {
        /// <summary>
        /// Test if input listener functions are properly registered & un-registered
        /// </summary>
        [Test]
        public void InputListeners()
        {
            // Create instance of ToolController
            ToolController toolController = new ToolController(ToolType.CANNON);
            
            // Check if InputManager's actions are not null. Input listener functions are registered
            Assert.IsNotNull(InputManager.ButtonInputAction);
            Assert.IsNotNull(InputManager.MouseMovementAction);
            
            // Un-register input listener functions
            toolController.DeConstruct();
            
            // Check if InputManager's actions are null. Input listener functions are not registered
            Assert.IsNotNull(InputManager.ButtonInputAction);
            Assert.IsNotNull(InputManager.MouseMovementAction);
        }
    }
}
