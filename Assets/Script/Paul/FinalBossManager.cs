using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossManager : MonoBehaviour
{
    [Header("Boss Particle Settings")]
    [SerializeField]
    private GameObject bossAttack1;
    [SerializeField]
    private float bossAtk1Lifetime;
    [SerializeField]
    private GameObject bossAttack2;
    [SerializeField]
    private float bossAtk2Lifetime;
    [SerializeField]
    private GameObject vulnerableIndicator;

    [Header("Attack Timing Settings")]
    [SerializeField]
    private float minAttackInterval = 2f; // Minimum time between attacks
    [SerializeField]
    private float maxAttackInterval = 5f; // Maximum time between attacks


    private GameObject player;
    public static FinalBossManager Instance { get; private set; } // Singleton

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        bossAttack1.SetActive(false);
        bossAttack2.SetActive(false);
        vulnerableIndicator.SetActive(false);
    }

    // Start the boss fight
    public void StartBossFight()
    {
        StartCoroutine(StartAttack());
    }

    // Coroutine to handle random attack intervals
    private IEnumerator StartAttack()
    {
        while (true) // Loops infinitely, modify as needed for the boss fight
        {
            // Random time interval within the range
            float timeUntilNextAttack = Random.Range(minAttackInterval, maxAttackInterval);
            yield return new WaitForSeconds(timeUntilNextAttack);

            // Trigger a random attack
            TriggerRandomAttack();
        }
    }

    // Function to trigger a random boss attack
    private void TriggerRandomAttack()
    {
        int attackChoice = Random.Range(0, 2); // Assuming two attacks: 0 or 1
        if (attackChoice == 0)
        {
            Attack1();
        }
        else
        {
            Attack2();
        }
    }

    void Attack1()
    {
        Vector3 positionToAttack = GetPlayerPosition();
        bossAttack1.transform.position = positionToAttack;
        bossAttack1.SetActive(true);
        StartCoroutine(DeactivateAttackGOAfterSeconds(bossAttack1, bossAtk1Lifetime));
    }

    void Attack2()
    {
        Vector3 positionToAttack = GetPlayerPosition();
        bossAttack2.transform.position = positionToAttack;
        bossAttack2.SetActive(true);
        StartCoroutine(DeactivateAttackGOAfterSeconds(bossAttack2, bossAtk2Lifetime));
    }

    private Vector3 GetPlayerPosition()
    {
        // Find player reference in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
        }
        return player.transform.position;
    }

    private IEnumerator DeactivateAttackGOAfterSeconds(GameObject attackGameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        attackGameObject.SetActive(false);
    }
}
