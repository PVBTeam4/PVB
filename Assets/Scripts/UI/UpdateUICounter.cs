using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUICounter : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public void updateCounter(int min, int max)
    {
        text.text = min + "/" + max;
    }
}
