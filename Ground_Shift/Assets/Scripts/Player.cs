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
        lineRenderer.widthMultiplier = 0.025f;
        lineRenderer.positionCount = 2;
        lineRenderer.material.color = Color.black;
    }

    private void FixedUpdate()
    {
        if (GameManager.is3D)
        {
            MoveIn3D();
        }
        else
        {
            MoveIn2D();
        }
    }

    private void Update()
    {
        PlayerAim();
        //print(Input.mousePosition);
    }

    /// <summary>
    /// 3D模式的移動
    /// </summary>
    private void MoveIn3D()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);
        rig.constraints = RigidbodyConstraints.FreezeRotation;

        float H = Input.GetAxis("Horizontal");  //取得水平
        float V = Input.GetAxis("Vertical");   //取得前後

        if (H != 0 || V != 0)
        {
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
    }

    /// <summary>
    /// 2D模式的移動
    /// </summary>
    private void MoveIn2D()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        float H = Input.GetAxis("Horizontal");  //取得水平
        float V = Input.GetAxis("Vertical");   //取得前後

        if (H != 0 || V != 0)
        {
            rig.AddForce(new Vector3(H * speed, 0, 0));

            //衝刺
            if (Input.GetKeyDown(KeyCode.V))
            {
                rig.AddForce(new Vector3(H * speed * 1.5f, 0, 0));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.AddForce(new Vector3(0, 100, 0));
                //rig.velocity = new Vector3(0, 10, 0);
            }        
        }
    }

    private void PlayerAim()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, 0);
        if (Vector3.Distance(playerPos, mousePos) < 2)
        {
            lineRenderer.SetPosition(0, playerPos);
            lineRenderer.SetPosition(1, mousePos);
        }
        else
        {
            Vector3 endPoint = Vector3.Lerp(playerPos, mousePos, 2f/ Vector3.Distance(mousePos, playerPos));
            lineRenderer.SetPosition(0, playerPos);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
