using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;

    private int maxHealth;
    private int currentHealth;


    public int healthLeft => currentHealth;
    public int healthMax => maxHealth;
    public int poisonLeft => poisonDamage;

    private int poisonDamage;
    [SerializeField]private int maxPoisonDamage = 2;

    public UnityAction onDeath;

    public void SetHealth(int current, int max, int poison)
    {
        currentHealth = current;
        maxHealth = max;
        poisonDamage = Mathf.Clamp(poison, 0, maxPoisonDamage);

        healthUI.SetStartHealth(current, max);
    }

    public void Damage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthUI.ShowDamage(currentHealth, maxHealth);
        if(currentHealth.Equals(0))onDeath?.Invoke();
    }

    public void Poison(int damage)
    {
        poisonDamage = Mathf.Clamp(poisonDamage+damage, 0, maxPoisonDamage);
    }

    public void AplyPoisonDamage()
    {
        if (poisonDamage > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth-poisonDamage, 0, maxHealth);
            healthUI.ShowPoison(currentHealth, maxHealth);
            if (currentHealth.Equals(0)) onDeath?.Invoke();
        }
    }

    public void Hill(int hill)
    {
        poisonDamage-= hill;
        if(poisonDamage<0)poisonDamage = 0;

        if (currentHealth < maxHealth)
        {
            currentHealth += hill;
            if(currentHealth>maxHealth) currentHealth = maxHealth;
            healthUI.ShowHill(currentHealth, maxHealth);

        }

    }
}
