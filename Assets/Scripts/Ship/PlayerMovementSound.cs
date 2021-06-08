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

        void Awake()
        {
            script = GetComponentInParent<PlayerMovement>();
        }

        void Update()
        {
            //print(script.ForwardSpeed);

            float speed = script != null ? script.ForwardSpeed : 0;

            float amount = Mathf.Lerp(minRPM, maxRPM, speed / maxRPM);

            print(script.ForwardSpeed);

            // set RPM value for the FMOD event
            float effectiveRPM = amount;
            var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
            emitter.SetParameter("Speed", effectiveRPM);
        }
    }
}
