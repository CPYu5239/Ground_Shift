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

    /*private IEnumerator AnimationTime()
    {
        enabled = false;
        yield return new WaitForSeconds(1);
        enabled = true;
    }*/

    /*private IEnumerator CameraSwitch()
    {
        Vector3 newPos = new Vector3(transform.position.x - 25, transform.position.y + 14.5f, transform.position.z + 15);
        for (int i = 0; i >= 0; i++)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
            yield return new WaitForSeconds(0.1f);
            if (transform.position == newPos)
            {                
                GameManager.is3D = true;
                break;
            }
        }
        //transform.position = newPos;
    }*/
}
