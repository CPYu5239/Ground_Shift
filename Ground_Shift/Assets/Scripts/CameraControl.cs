using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform player;

    private void LateUpdate()
    {
        Track();
    }

    /// <summary>
    /// 攝影機追蹤
    /// </summary>
    private void Track()
    {
        player = GameManager.player.transform;
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
