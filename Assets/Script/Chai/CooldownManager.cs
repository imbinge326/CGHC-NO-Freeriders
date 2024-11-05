using UnityEngine;
using System.Collections;
using TMPro;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance;

    private float cooldownTime = 5f;
    private float cooldownTimer = 0f;
    private bool isCooldownActive = false;

    public TMP_Text cooldownMessage; // 引用 TextMeshProUGUI 组件

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 初始隐藏冷却提示文本
        if (cooldownMessage != null)
        {
            cooldownMessage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("CooldownMessage TextMeshProUGUI not found. Ensure it exists in the scene.");
        }
    }

    public void StartCooldown(float cooldown)
    {
        if (!isCooldownActive)
        {
            cooldownTime = cooldown;
            cooldownTimer = cooldown;
            isCooldownActive = true;
            StartCoroutine(CooldownRoutine());
        }
    }

    public bool IsCooldownActive()
    {
        return isCooldownActive;
    }

    private IEnumerator CooldownRoutine()
    {
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        isCooldownActive = false;
    }

    public float GetCooldownRemaining()
    {
        return Mathf.Max(0, cooldownTimer);
    }

    // 显示冷却剩余时间的提示文本
    public void ShowCooldownMessage()
    {
        if (cooldownMessage != null)
        {
            cooldownMessage.gameObject.SetActive(true);
            cooldownMessage.text = $"Skill cooling down, remaining time: {GetCooldownRemaining():F1} s";
            StartCoroutine(HideCooldownMessageAfterDelay());
        }
    }

    private IEnumerator HideCooldownMessageAfterDelay()
    {
        // 等待1秒钟，然后隐藏冷却提示文本
        yield return new WaitForSeconds(1f);
        cooldownMessage.gameObject.SetActive(false);
    }
}
