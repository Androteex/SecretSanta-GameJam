using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void OpenThisDoor()
    {
        gameObject.SetActive(false);
    }
}
