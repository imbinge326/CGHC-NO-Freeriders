using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    [SerializeField] private static float sharedHealth = 100f; // 静态变量，三个Prefab共用同一个生命值
    private float maxHealth = 100f;
    private HealthBar healthBar;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();

        // 在游戏开始时，将静态共享的健康值设定为最大生命值
        sharedHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, sharedHealth);
    }

    public void Heal(float health)
    {
        sharedHealth += health;
        healthBar.UpdateHealthBar(maxHealth, sharedHealth);
        Debug.Log(gameObject.name + " took " + health + " damage. Remaining shared health: " + sharedHealth);

        if (sharedHealth >= 100)
        {
            sharedHealth = 100;
        }
    }

    public void TakeDamage(float damage)
    {
        sharedHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, sharedHealth);
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining shared health: " + sharedHealth);

        if (sharedHealth < 0.001f)
        {
            var spawn = GameObject.Find("SpawnPoint");
            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawn.transform.position;
            sharedHealth = 100f;
            healthBar.UpdateHealthBar(maxHealth, sharedHealth);
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died! Shared health is depleted!");

        // 销毁所有带有 "Player" tag 的角色，因为生命值是共享的
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

        RoleSwitcher roleSwitcher = GameObject.Find("SwitchRole").GetComponent<RoleSwitcher>();
        if (roleSwitcher == null)
            Debug.LogError("SwitchRole not found in hierarchy");

        roleSwitcher.canSwitch = false;

        // Death UI below
    }

    // 返回当前的共享生命值
    public float GetSharedHealth()
    {
        return sharedHealth;
    }
}
