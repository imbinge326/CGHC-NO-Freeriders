using UnityEngine;
using System.Collections;

public class ChaiEnemy : MonoBehaviour
{
    public int hp = 100; // 敌人生命值
    public float attackSpeed = 1.5f; // 攻击间隔（1.5秒冷却时间）
    public int attackPower = 10; // 攻击力量
    public float attackRange = 1f; // 攻击范围
    public float walkSpeed = 2f; // 行走速度
    public float detectionRange = 5f; // 检测玩家的距离

    private Transform player; // 玩家对象
    private bool isChasing = false; // 是否正在追击玩家
    private bool isAttacking = false; // 是否正在攻击
    private float lastAttackTime = 0f; // 上一次攻击的时间
    private Animator animator; // 用于播放动画

    void Start()
    {
        // 获取Animator组件（如果有）
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 动态查找Player对象（每帧都查找）
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        // 如果玩家对象未找到，直接返回
        if (player == null) return;

        // 检测玩家是否在检测范围内
        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x); // 只计算X轴距离

        // 检查玩家是否处于隐身状态
        Invisible playerInvisible = player.GetComponent<Invisible>();

        // 如果玩家在检测范围内且不处于隐身状态，追击玩家
        if (distanceToPlayer <= detectionRange && (playerInvisible == null || !playerInvisible.isInvisible) && !isAttacking)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            // 玩家不在范围内或处于隐身状态，停止追击
            isChasing = false;

            // 停止追击动画
            if (animator != null)
            {
                animator.SetBool("isChasing", false);
            }
        }

        // 如果玩家在攻击范围内并且在追击，进行攻击
        if (isChasing && distanceToPlayer <= attackRange && !isAttacking)
        {
            Debug.Log("Attacking!!!!!!!!!!");
            // ***********************
            AttackPlayer();
        }
    }

    // 追击玩家的逻辑
    void ChasePlayer()
    {
        // 判断玩家在左边还是右边
        if (player.position.x < transform.position.x)
        {
            // 玩家在左边，向左移动
            transform.position += Vector3.left * walkSpeed * Time.deltaTime;
        }
        else if (player.position.x > transform.position.x)
        {
            // 玩家在右边，向右移动
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
        }

        // 播放追击动画（如果有）
        if (animator != null)
        {
            animator.SetBool("isChasing", true);
        }
    }

    //*
    // ********************************************************
    // 攻击玩家的逻辑
    void AttackPlayer()
    {
        // 检查攻击冷却时间
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            // 播放攻击动画（如果有）
            if (animator != null)
            {
                animator.SetTrigger("Attack"); // 确保你的动画控制器有一个"Attack"触发器
            }
            
            // 标记为正在攻击，防止敌人在攻击过程中重复追击
            isAttacking = true;
            /*
            // 扣除玩家生命值（假设玩家有一个PlayerHealth脚本）
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackPower);
            }
            */
            // 记录攻击时间
            lastAttackTime = Time.time;

            // 启动冷却结束的计时器，1.5秒后恢复可以攻击状态
            StartCoroutine(AttackCooldown());
            
        }
    }
    //

    // 攻击冷却逻辑
    IEnumerator AttackCooldown()
    {
        // 等待攻击冷却时间结束
        yield return new WaitForSeconds(attackSpeed);

        // 恢复可以攻击状态
        isAttacking = false;
    }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invisible playerInvisible = collision.gameObject.GetComponent<Invisible>();
            if (playerInvisible != null && playerInvisible.isInvisible)
            {
                // 玩家处于隐身状态，忽略碰撞
                return;
            }

            // 执行其他碰撞逻辑
        }
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
