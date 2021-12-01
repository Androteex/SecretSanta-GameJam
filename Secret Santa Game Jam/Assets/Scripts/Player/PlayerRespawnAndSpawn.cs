using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawnAndSpawn : MonoBehaviour
{
    [HideInInspector] public Transform checkPoint;
    [HideInInspector] public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = GameObject.Find("Spawnpoint").transform;
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }

    public void Respawn()
    {
        if (checkPoint != null)
        {
            transform.position = checkPoint.position;
        }
        else
        {
            transform.position = spawnPoint.position;
        }
    }
}
