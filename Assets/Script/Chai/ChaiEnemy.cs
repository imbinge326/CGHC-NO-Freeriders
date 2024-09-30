using UnityEngine;
using System.Collections;

public class ChaiEnemy : MonoBehaviour
{
    // 可设置的变量
    public int hp = 100; // 敌人生命值
    public float attackSpeed = 1f; // 攻击间隔
    public int attackPower = 10; // 攻击力量
    public float attackRange = 1f; // 攻击范围
    public float walkSpeed = 2f; // 行走速度
    public float detectionRange = 5f; // 检测玩家的距离
    public Transform pointA; // 巡逻点A
    public Transform pointB; // 巡逻点B

    private Transform targetPoint; // 当前目标点
    private Transform player; // 玩家对象
    private bool isChasing = false; // 是否正在追击玩家
    private float lastAttackTime = 0f; // 上一次攻击的时间
    private Animator animator; // 用于播放动画
    private SpriteRenderer spriteRenderer; // 用于显示敌人的视觉效果

    void Start()
    {
        // 设置初始巡逻目标点
        targetPoint = pointA;

        // 获取玩家和动画组件
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 如果敌人生命值小于等于0，敌人死亡
        if (hp <= 0)
        {
            Die();
            return;
        }

        // 检测玩家是否在检测范围内并且玩家没有隐身
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Invisible playerInvisible = player.GetComponent<Invisible>();

        if (distanceToPlayer <= detectionRange && !playerInvisible.isInvisible)
        {
            // 如果玩家在检测范围内并且不隐身，追击玩家
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            // 否则巡逻
            isChasing = false;
            Patrol();
        }

        // 如果玩家在攻击范围内，攻击玩家
        if (isChasing && distanceToPlayer <= attackRange)
        {
            Debug.Log("Attack Player");
            // AttackPlayer();
        }
    }

    // 巡逻：敌人在两个点之间来回移动
    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, walkSpeed * Time.deltaTime);

        // 到达目标点时切换目标
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    // 追击玩家
    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, walkSpeed * Time.deltaTime);
    }
    /*
    // 攻击玩家的逻辑
    void AttackPlayer()
    {
        // 检查攻击冷却时间
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            // 播放攻击动画
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // 扣除玩家生命值
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackPower);
            }

            // 记录攻击时间
            lastAttackTime = Time.time;
        }
    }
    */

    // 敌人受到伤害
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();

        }
    }

    // 敌人死亡的逻辑
    void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject, 1f);
    }

    // 可视化检测范围和攻击范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // 检测范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // 攻击范围
    }
}
