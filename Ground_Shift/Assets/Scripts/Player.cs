using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(10,50)]
    public float speed = 10;

    private Rigidbody rig;
    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
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

    /// <summary>
    /// 3D模式的移動
    /// </summary>
    private void MoveIn3D()
    {
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
}
