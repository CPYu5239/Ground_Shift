using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料")]
    public EnemyData data;

    Animator animator;
    bool isOpen;
    bool ran;
    NavMeshAgent agent;
    Rigidbody rig;
    protected float timer;

    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        timer = data.attackCD;
    }

    private void Update()
    {
        //print(Vector3.Distance(GameManager.player.transform.position, transform.position));
        if (Vector3.Distance(GameManager.player.transform.position, transform.position) <= data.detectRange && !ran)
        {
            //到一定距離時自動打開
            StartCoroutine(Open());
            ran = true;
        }
        else if (Vector3.Distance(GameManager.player.transform.position, transform.position) > data.detectRange && ran)
        {
            //超過一定距離時關閉
            StartCoroutine(Idle());
            ran = false;
        }

        Move();

        if (data.hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// 移動方法
    /// </summary>
    private void Move()
    {
        if (GameManager.is3D)
        {
            rig.constraints = RigidbodyConstraints.None;
        }
        else
        {
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        if (isOpen)
        {
            if (Vector3.Distance(GameManager.player.transform.position, transform.position) <= data.attackRange)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                if (timer <= data.attackCD)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(GameManager.player.transform.position);
                agent.speed = data.moveSpeed;
                animator.SetBool("Walk", true);
                animator.SetBool("Attack", false);
            }
            
            Vector3 posTarget = GameManager.player.transform.position;  //取得目標
            posTarget.y = transform.position.y;   //固定y軸
            transform.LookAt(posTarget); //看向目標
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
        }
    }

    /// <summary>
    /// 開啟
    /// </summary>
    /// <returns>等待動畫秒數</returns>
    private IEnumerator Open()
    {
        
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(3.3f);
        isOpen = true;
    }

    /// <summary>
    /// 玩家超過距離先做找玩家的動作在關閉
    /// </summary>
    /// <returns>等待動畫秒數</returns>
    private IEnumerator Idle()
    {
        isOpen = false;
        agent.isStopped = true;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(3.4f);
        animator.SetTrigger("Close");
    }

    /// <summary>
    ///攻擊方法
    /// </summary>
    private void Attack()
    {
        timer = 0;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");
        
        StartCoroutine(DelayAttack());
    }

    /// <summary>
    /// 受傷方法
    /// </summary>
    public void Hit(float damage)
    {
        data.hp -= (int)damage;
    }

    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Death()
    {
        agent.isStopped = true;
        animator.SetTrigger("Close");
        StartCoroutine(WaitDie());
        animator.SetBool("Die", true);
        
    }

    private IEnumerator WaitDie()
    {
        isOpen = false;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //transform.forward,right,up 可以抓取物件的前方,右方跟上方(反向加-號)
        //繪製射線 (中心點,方向)
        Gizmos.DrawRay(transform.position + new Vector3(-1, 0.5f, 0), transform.forward);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(data.attackDelay);

        RaycastHit hit;   //射線碰撞資訊

        //物理.射線碰撞(中心點,方向,射線碰撞資訊(加上ont修飾詞可以佔存資訊),長度) 傳回bool
        //利用物理的射線碰撞來判斷是否有打到玩家
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, data.attackRange))
        {
            //print("打到" + hit.collider.gameObject.name + "了!!");
            //GameObject player = GameObject.Find(hit.collider.gameObject.name);
            //player.GetComponent<Player>().Hit(data.attack);
            hit.collider.gameObject.GetComponent<Player>().Hit(data.attack);    //取得碰撞的物件並呼叫Player內的Hit()方法
        }
    }
}
