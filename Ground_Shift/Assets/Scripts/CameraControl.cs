using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("玩家").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 pos = player.position;
        Vector3 posCam = transform.position;

        if (GameManager.is3D)
        {
            transform.position = new Vector3(pos.x - 25, posCam.y, posCam.z);
        }
        else
        {
            transform.position = new Vector3(pos.x, posCam.y, posCam.z);
        }
    }

}
