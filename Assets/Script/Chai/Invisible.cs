using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour
{
    public int time; // 隐身的持续时间
    public bool isInvisible = false; // 隐身模式，默认为false
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 获取角色的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // 1. 使角色透明 (隐身效果)
        Color color = spriteRenderer.color;
        color.a = 0.2f; // 将透明度设为0.2
        spriteRenderer.color = color;

        Debug.Log("Character is now in invisible mode!");

        // 等待5秒（隐身持续时间）
        yield return new WaitForSeconds(time);

        // 2. 恢复透明度
        color.a = 1f; // 将透明度设为1，恢复可见
        spriteRenderer.color = color;

        isInvisible = false;
        Debug.Log("Character is now visible again!");
    }
}
