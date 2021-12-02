using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonPlatform : MonoBehaviour
{
    public DoubleButton buttonScript1;
    public DoubleButton buttonScript2;
    public GameObject platform;

    private void Start()
    {
        platform.SetActive(false);
    }

    private void Update()
    {
        if (buttonScript1.active && buttonScript2.active)
        {
            platform.SetActive(true);
        }
        else
        {
            platform.SetActive(false);
        }
    }
}
