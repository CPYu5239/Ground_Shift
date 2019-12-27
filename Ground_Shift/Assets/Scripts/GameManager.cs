using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cam;

    private Animator ani;
    public static bool is3D;

    private void Start()
    {
        ani = GameObject.Find("Camera").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraSwitch();
        }
    }

    private void CameraSwitch()
    {
        if (is3D)
        {
            ani.SetTrigger("Switch2D");
            is3D = false;
        }
        else
        {
            ani.SetTrigger("Switch3D");
            is3D = true;
        }
    }
}
