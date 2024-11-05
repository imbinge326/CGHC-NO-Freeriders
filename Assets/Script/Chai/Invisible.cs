using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour
{
    public int time; // 隐身持续时间
    public float cooldown = 5f; // 冷却时间
    public bool isInvisible = false; // 隐身状态
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isInvisible && !CooldownManager.Instance.IsCooldownActive())
        {
            StartCoroutine(BecomeInvisible());
        }
        else if (Input.GetKeyDown(KeyCode.X) && CooldownManager.Instance.IsCooldownActive())
        {
            // 调用 CooldownManager 中的 ShowCooldownMessage 方法
            CooldownManager.Instance.ShowCooldownMessage();
        }
    }

    IEnumerator BecomeInvisible()
    {
        isInvisible = true;

        Color color = spriteRenderer.color;
        color.a = 0.2f;
        spriteRenderer.color = color;

        Debug.Log("角色现在处于隐身状态！");

        yield return new WaitForSeconds(time);

        color.a = 1f;
        spriteRenderer.color = color;

        isInvisible = false;
        Debug.Log("角色现在可见！");

        CooldownManager.Instance.StartCooldown(cooldown);
    }
}
