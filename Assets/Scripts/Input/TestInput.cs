using Input.AbstractListeners;
using UnityEngine;

namespace Input
{
    public class TestInput : InputMonoBehaviour
    {
        protected override void OnInputReceived(ButtonInputType buttonInputType, float value)
        {
            Debug.Log("Input: " + buttonInputType + " - " + value);
        }
    }
}
