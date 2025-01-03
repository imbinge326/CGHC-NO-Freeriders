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
    [SerializeField]
    private GameObject bossDieExplosion;

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
    private BossPhase1 bossScript;
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
        bossScript = godNovusPhase1Object.GetComponent<BossPhase1>();
        if (bossScript == null)
            Debug.LogError("BossPhase1 Script not found");

        bossDieExplosion.SetActive(false);
        godNovusPhase1Object.SetActive(false);
        vulnerableIndicator.SetActive(false);

        // StartBossFight(); // DEBUGGING
    }

    // Start the boss fight
    public void StartBossFight()
    {
        godNovusPhase1Object.SetActive(true);
        StartCoroutine(StartAttack());
        SoundManager.Instance.StartFinalBossBGM();
    }

    public void BossDies()
    {
        // Boss Die Logic
        Animator bossAnimator = godNovusPhase1Object.GetComponent<Animator>();
        if (bossAnimator == null)
            Debug.LogError("Animator not found in boss prefab");

        bossAnimator.SetBool("BossDie", true);

        StartCoroutine(DelayBossDeath());
    }

    private IEnumerator DelayBossDeath()
    {
        yield return new WaitForSeconds(1f);

        bossDieExplosion.SetActive(true);

        yield return new WaitForSeconds(0.375f);

        godNovusPhase1Object.SetActive(false);

        yield return new WaitForSeconds(0.75f);

        bossDieExplosion.SetActive(false);

        FinalLevelManager.Instance.BossDies();
    }

    // Coroutine to handle random attack intervals
    private IEnumerator StartAttack()
    {
        isAttacking = true;
        while (isAttacking && bossScript.GetBossHealth() > 0)
        {
            float timeUntilNextAttack = Random.Range(minAttackInterval, maxAttackInterval);
            yield return new WaitForSeconds(timeUntilNextAttack);

            // Trigger a random attack
            TriggerRandomAttack();
        }

        if (!isAttacking || bossScript.GetBossHealth() <= 0)
        {
            isAttacking = false;
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
        // Sound
        SoundManager.Instance.PlaySoundEffect("ChargeUpSFX");
        StartCoroutine(PlaySFXAfterSeconds("AnimeShingSFX", 1.8f));


        Vector3 positionToAttack = GetPlayerPosition();
        GameObject attackInstance = Instantiate(bossAttack1, positionToAttack, Quaternion.identity);
        Destroy(attackInstance, bossAtk1Lifetime);
    }

    void Attack2()
    {
        // Sound
        SoundManager.Instance.PlaySoundEffect("ChargeUpSFX");
        StartCoroutine(PlaySFXAfterSeconds("FloorBreakSFX", 1.8f));


        Vector3 positionToAttack = GetPlayerPosition();
        GameObject attackInstance = Instantiate(bossAttack2, positionToAttack, Quaternion.identity);
        Destroy(attackInstance, bossAtk2Lifetime);
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

    private IEnumerator MakeBossVulnerable()
    {
        isAttacking = false;
        bossScript.isVulnerable = true;

        vulnerableIndicator.SetActive(true);

        yield return new WaitForSeconds(vulnerabilityDuration);

        StartCoroutine(StartAttack());
        bossScript.isVulnerable = false;

        vulnerableIndicator.SetActive(false);
    }

    private IEnumerator PlaySFXAfterSeconds(string sfxName, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SoundManager.Instance.PlaySoundEffect(sfxName);
    }
}
