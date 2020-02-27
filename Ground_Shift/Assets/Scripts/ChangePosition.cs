using System.Collections;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        FreezePosition();
    }

    private void FreezePosition()
    {
        if (!GameManager.is3D)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.player.transform.position.z);
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
