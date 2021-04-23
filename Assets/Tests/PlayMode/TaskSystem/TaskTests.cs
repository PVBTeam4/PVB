using System.Collections;
using System.Linq;
using Global;
using NUnit.Framework;
using TaskSystem;
using TaskSystem.Objectives;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Tests.PlayMode.TaskSystem
{
    public class TaskTests
    {
        [UnityTest]
        public IEnumerator TestCannonTaskInitialization()
        {
            // First create objectives
            KillObjective[] killObjectives = CreateKillObjectives(10, 100);
            
            ToolType toolType = ToolType.CANNON;
            
            GameObject managerGameObject = new GameObject();
            GameManager gameManager = managerGameObject.AddComponent<GameManager>();
            
            gameManager.OnEnterTaskMode(toolType);

            yield return new WaitForSeconds(1f);

            TaskController taskController = gameManager.TaskController;
            
            foreach (Objective activeTaskActiveObjective in taskController.ActiveTask.ActiveObjectives)
            {
                if (!killObjectives.Contains(activeTaskActiveObjective))
                {
                    Assert.Fail("Objectives do not match");
                }
            }
        }

        private KillObjective[] CreateKillObjectives(int amount, float maxHealth)
        {
            KillObjective[] killObjectives = new KillObjective[amount];
            for (int i = 0; i < amount; i++)
            {
                GameObject killGameObject = new GameObject();
                KillObjective killObjective = killGameObject.AddComponent<KillObjective>();
            
                SerializeUtil.SetSerializedFields(killObjective, serializedObject =>
                {
                    serializedObject.FindProperty("maxHealth").floatValue = maxHealth;
                });

                killObjectives[i] = killObjective;
            }

            return killObjectives;
        }
    }
}
