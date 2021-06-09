using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    public class PlayerMovementSound : MonoBehaviour
    {
        public float minRPM = 0;
        public float maxRPM = 5.5f;
        PlayerMovement script;

        FMODUnity.StudioEventEmitter soundEventEmitter;

        void Awake()
        {
            script = GetComponentInParent<PlayerMovement>();

            // Get the FMOD sound emitter
            soundEventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        }

        void Update()
        {
            //print(script.ForwardSpeed);

            float speed = script != null ? script.ForwardSpeed : 0;

            speed = Mathf.Abs(speed);

            float amount = Mathf.Lerp(minRPM, maxRPM, speed / maxRPM);

            print(script.ForwardSpeed);

            // set RPM value for the FMOD event
            float effectiveRPM = amount;

            // Update the emitter
            soundEventEmitter.SetParameter("Speed", effectiveRPM);
        }
    }
}
