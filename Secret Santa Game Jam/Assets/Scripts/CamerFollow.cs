using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;

    public GameObject player;

    private int zOffset = -10;

    private void LateUpdate()
    {
        if (player.transform.position.x <= -9)
        {
            transform.position = new Vector3(target1.transform.position.x, 0, zOffset);
        }
        else if (player.transform.position.x >= 9)
        {
            transform.position = new Vector3(target3.transform.position.x, 0, zOffset);
        }
        else
        {
            transform.position = new Vector3(target2.transform.position.x, 0, zOffset);
        }
    }
}
