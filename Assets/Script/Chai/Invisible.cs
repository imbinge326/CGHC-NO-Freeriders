using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour
{
    public int time; // 隐身的持续时间
    public bool isInvisible = false; // 隐身模式，默认为false
    private SpriteRenderer spriteRenderer;
    private int originalLayer; // 保存原始的Layer

    void Start()
    {
        // 获取角色的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLayer = gameObject.layer; // 记录玩家的原始层
    }

    void Update()
    {
        // 按下X键时开启隐身模式
        if (Input.GetKeyDown(KeyCode.X) && !isInvisible)
        {
            StartCoroutine(BecomeInvisible());
        }
    }

    // 隐身并降低透明度
    IEnumerator BecomeInvisible()
    {
        isInvisible = true;

        // 使角色透明
        Color color = spriteRenderer.color;
        color.a = 0.2f;
        spriteRenderer.color = color;

        Debug.Log("角色现在处于隐身状态！");

        // 等待指定的时间
        yield return new WaitForSeconds(time);

        // 恢复可见性
        color.a = 1f;
        spriteRenderer.color = color;

        isInvisible = false;
        Debug.Log("角色现在可见！");
    }
}
