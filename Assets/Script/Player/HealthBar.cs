using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    void Update()
    {
        healthBarImage.fillAmount = Mathf.MoveTowards(healthBarImage.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
