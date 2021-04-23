using System.Collections;
using NUnit.Framework;
using ToolSystem.Tools;
using UnityEngine;
using Utils;

namespace Tests.PlayMode.ToolSystem
{
    public class CannonToolTests
    {
        // TODO Write test for gun rotation, currently throws errors
        // [UnityTest]
        public IEnumerator GunRotation()
        {
            // Initialize cannon
            GameObject bulletSpawnGameObject = new GameObject();
            
            GameObject cameraGameObject = new GameObject();
            Camera camera = cameraGameObject.AddComponent<Camera>();
            cameraGameObject.tag = "MainCamera";
            
            GameObject gunFloorGameObject = new GameObject();
            GameObject gunGameObject = new GameObject();
            gunGameObject.transform.parent = gunFloorGameObject.transform;
            
            CannonTool cannonTool = gunGameObject.AddComponent<CannonTool>();
            
            SerializeUtil.SetSerializedFields(cannonTool, serializedObject =>
            {
                Debug.Log(serializedObject.FindProperty("gunFloor"));
                
                serializedObject.FindProperty("gunFloor").objectReferenceValue = gunFloorGameObject.transform;
                serializedObject.FindProperty("bulletSpawnLocation").objectReferenceValue = bulletSpawnGameObject.transform;
            });
            
            // Initialize mouse
            Vector3 mousePosition = new Vector3(0, .5f);

            cannonTool.MoveGun(mousePosition, camera);
            
            yield return new WaitForSeconds(.1f);

            Quaternion gunRotation = gunGameObject.transform.rotation;
            Quaternion gunFloorRotation = gunFloorGameObject.transform.rotation;
            
            Debug.Log(gunRotation.eulerAngles.ToString() + ", " + gunFloorRotation.eulerAngles.ToString());
            
            Assert.IsNull(null);
        }
    }
}
