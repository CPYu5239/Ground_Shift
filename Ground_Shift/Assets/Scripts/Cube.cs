using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
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
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
