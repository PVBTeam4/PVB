using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField]
    private string soundEventPath = "event:/Static/Seagull";

    [SerializeField]
    private Vector2 randomTimeRange = new Vector2(0.5f, 5f);

    private bool canSound = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSound)
        {
            canSound = false;

            StartCoroutine(Timer(Random.Range(randomTimeRange.x, randomTimeRange.y)));
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        canSound = true;

        //Play the sound
        FMODUnity.RuntimeManager.PlayOneShot(soundEventPath, transform.position);
    }
}
