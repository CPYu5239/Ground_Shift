using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData data;
    public GameObject attackObject;
    public bool isDash;           //是否在衝刺

    GameManager gameManager;
    LineRenderer lineRenderer;
    Rigidbody rig;
    Vector3 aimPoint;
    public string gotoElement;    //要變成的玩素

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rig = gameObject.GetComponent<Rigidbody>();
        gameObject.AddComponent<LineRenderer>();
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

        print("isDash=" + isDash);
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameManager.ChangeElement(gotoElement);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isDash = true;
            StartCoroutine(NoDash());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.nowElement != "Default")
        {
            Attack();
        }

        if (data.hp <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "敵人" && isDash)
        {
            isDash = false;
            Hit(10); //用衝次攻擊時自己也會受到損傷
            collision.gameObject.GetComponent<Enemy>().Hit(data.attack);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        gotoElement = collision.collider.tag;
    }

    private void OnCollisionExit(Collision collision)
    {
        gotoElement = "Default";
    }

    /// <summary>
    /// 3D模式的移動
    /// </summary>
    private void MoveIn3D()
    {
        rig.constraints = RigidbodyConstraints.FreezeRotation;

        float H = Input.GetAxis("Horizontal");  //取得水平
        float V = Input.GetAxis("Vertical");   //取得前後

        if (Input.anyKey || Input.anyKeyDown)
        {
            rig.AddForce(new Vector3(V * data.moveSpeed, 0, -H * data.moveSpeed));
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
        //float V = Input.GetAxis("Vertical");   //取得前後

        if (Input.anyKey || Input.anyKeyDown)
        {
            
            rig.AddForce(new Vector3(H * data.moveSpeed, 0, 0));

            RaycastHit hit;

            if (Physics.Raycast(transform.position, -transform.up, out hit,1))
            {
                if (hit.collider.gameObject.tag == "可站立物體")
                {
                    //衝刺
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        print("衝刺");
                        rig.AddForce(aimPoint * 250);
                   }

                   //跳躍
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        print("跳躍");
                        rig.AddForce(new Vector3(0, 250, 0));
                    }
                }   
            }
        }
    }

    /// <summary>
    /// 角色瞄準
    /// </summary>
    private void PlayerAim()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, 0);
        if (Vector3.Distance(playerPos, mousePos) < 2)
        {
            lineRenderer.SetPosition(0, playerPos);
            lineRenderer.SetPosition(1, mousePos);
            aimPoint = mousePos - playerPos;
        }
        else
        {
            Vector3 endPoint = Vector3.Lerp(playerPos, mousePos, 2f/ Vector3.Distance(mousePos, playerPos));
            lineRenderer.SetPosition(0, playerPos);
            lineRenderer.SetPosition(1, endPoint);
            aimPoint = endPoint - playerPos;
        }
    }

    /// <summary>
    /// 畫線判斷是否在地板
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //transform.forward,right,up 可以抓取物件的前方,右方跟上方(反向加-號)
        //繪製射線 (中心點,方向)
        Gizmos.DrawRay(transform.position, -transform.up);
    }

    /// <summary>
    /// 攻擊方法
    /// </summary>
    private void Attack()
    {
        data.energy -= 5;
        GameObject weapon = Instantiate(attackObject,gameObject.transform.position,Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(aimPoint * 500);
    }

    /// <summary>
    /// 受傷方法
    /// </summary>
    /// <param name="damage">受到的傷害值</param>
    public void Hit(float damage)
    {
        data.hp -= (int)damage;
    }

    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Death()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 收回衝刺狀態
    /// </summary>
    /// <returns>1秒後沒有衝次狀態</returns>
    private IEnumerator NoDash()
    {
        yield return new WaitForSeconds(1F);
        isDash = false;
    }
}