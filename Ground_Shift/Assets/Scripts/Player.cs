using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(10,50)]
    public float speed = 10;
    public LineRenderer lineRenderer;

    private Rigidbody rig;
    Vector3 originalPosition;

    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {
        PlayerAim();
        print(Input.mousePosition);
        if (GameManager.is3D)
        {
            MoveIn3D();
        }
        else
        {
            MoveIn2D();
        }
    }

    /// <summary>
    /// 3D模式的移動
    /// </summary>
    private void MoveIn3D()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);
        rig.constraints = RigidbodyConstraints.None;

        float H = Input.GetAxis("Horizontal");  //取得水平
        float V = Input.GetAxis("Vertical");   //取得前後

        if (Input.GetKeyDown(KeyCode.V))  //衝刺
        {
            rig.velocity = new Vector3(V * speed * 10, 0, H * -speed * 10);
        }
        else  //一般移動
        {
            rig.velocity = new Vector3(V * speed, 0, H * -speed);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //跳躍
        {
            rig.AddForce(new Vector3(0, 1000, 0));
        } 
    }

    /// <summary>
    /// 2D模式的移動
    /// </summary>
    private void MoveIn2D()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -6.5f);
        rig.constraints = RigidbodyConstraints.FreezePositionZ;

        float H = Input.GetAxis("Horizontal");  //取得水平
        float V = Input.GetAxis("Vertical");   //取得前後

        rig.velocity = new Vector3(H * speed, 0, 0);

        //衝刺
        if (Input.GetKeyDown(KeyCode.V))
        {
            rig.velocity = new Vector3(H * speed * 2, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rig.AddForce(new Vector3(0, 100, 0));
            rig.velocity = new Vector3(0, 10, 0);
        }
    }

    private void PlayerAim()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, 0);
        lineRenderer.SetPosition(0, playerPos);
        lineRenderer.SetPosition(1, mousePos);
    }
}
