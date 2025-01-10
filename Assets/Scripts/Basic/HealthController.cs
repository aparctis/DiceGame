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

    private int poisonDamage;
    [SerializeField]private int maxPoisonDamage = 2;

    public UnityAction onDeath;

    public void SetHealth(int current, int max)
    {
        currentHealth = current;
        maxHealth = max;

        healthUI.SetStartHealth(current, max);
    }

    public void Damage(int damage)
    {
        currentHealth-=damage;

        if(currentHealth<=0)
        {
            currentHealth = 0;
            onDeath.Invoke();
        }

        healthUI.ShowDamage(currentHealth, maxHealth);
    }

    public void Poison(int damage)
    {
        poisonDamage += damage;
        if(poisonDamage>maxPoisonDamage) poisonDamage = maxPoisonDamage;
    }

    public void AplyPoisonDamage(UnityAction ifNotDead)
    {
        if (poisonDamage > 0)
        {
            currentHealth -= poisonDamage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            healthUI.ShowPoison(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                ifNotDead?.Invoke();
            }
        }

    }

    public void Hill(int hill)
    {
        poisonDamage-= hill;

        if (currentHealth < maxHealth)
        {
            currentHealth += hill;
            if(currentHealth>maxHealth) currentHealth = maxHealth;
            healthUI.ShowPoison(currentHealth, maxHealth);

            healthUI.ShowHill(currentHealth, maxHealth);

        }

    }
}
