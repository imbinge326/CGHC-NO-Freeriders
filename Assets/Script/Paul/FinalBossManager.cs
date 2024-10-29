using System.Collections;
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
    [SerializeField]
    private bool isAttacking;

    [Header("Vulnerability Settings")]
    [SerializeField]
    private int attacksBeforeVulnerability = 5; // Number of attacks before boss becomes vulnerable
    [SerializeField]
    private float vulnerabilityDuration = 3f; // Duration the boss remains vulnerable

    [Header("Phase 1")]
    [SerializeField]
    private GameObject godNovusPhase1Object;

    private int attackCounter = 0; // Tracks the number of attacks
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
        godNovusPhase1Object.SetActive(false);
        bossAttack1.SetActive(false);
        bossAttack2.SetActive(false);
        vulnerableIndicator.SetActive(false);

        //StartBossFight(); // DEBUGGING
    }

    // Start the boss fight
    public void StartBossFight()
    {
        godNovusPhase1Object.SetActive(true);
        StartCoroutine(StartAttack());
    }

    public void BossDies()
    {
        // Boss Die Logic
    }

    // Coroutine to handle random attack intervals
    private IEnumerator StartAttack()
    {
        isAttacking = true;
        while (isAttacking)
        {
            float timeUntilNextAttack = Random.Range(minAttackInterval, maxAttackInterval);
            yield return new WaitForSeconds(timeUntilNextAttack);

            // Trigger a random attack
            TriggerRandomAttack();
        }

        if (!isAttacking)
        {
            yield return null;
        }
    }

    // Function to trigger a random boss attack
    private void TriggerRandomAttack()
    {
        int attackChoice = Random.Range(0, 2);
        if (attackChoice == 0)
        {
            Attack1();
        }
        else
        {
            Attack2();
        }

        // Increment the attack counter and check for vulnerability
        attackCounter++;
        if (attackCounter >= attacksBeforeVulnerability)
        {
            attackCounter = 0; // Reset counter after vulnerability
            StartCoroutine(MakeBossVulnerable());
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

    private IEnumerator MakeBossVulnerable()
    {
        BossPhase1 phase1Script = godNovusPhase1Object.GetComponent<BossPhase1>();
        if (phase1Script == null)
            Debug.LogError("BossPhase1 Script not found");

        isAttacking = false;
        phase1Script.isVulnerable = true;

        vulnerableIndicator.SetActive(true);

        yield return new WaitForSeconds(vulnerabilityDuration);

        StartCoroutine(StartAttack());
        phase1Script.isVulnerable = false;

        vulnerableIndicator.SetActive(false);
    }
}
