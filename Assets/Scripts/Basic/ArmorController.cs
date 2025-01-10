using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    [SerializeField] private ArmorUI armorUI;
    private int currentArmor;

    public void SetArmor(int newArmor)
    {
        currentArmor = newArmor;
        armorUI.SetArmor(currentArmor);
    }

    public int DamageAfterArmor(int incomeDamage)
    {
        if(currentArmor == 0)
        {
            return incomeDamage;
        }
        else
        {
            int newArmor = currentArmor - incomeDamage;
            if(newArmor< 0) newArmor = 0;
            armorUI.ShowArmorChange(newArmor);

            int trueDamage = incomeDamage - currentArmor;
            if(trueDamage < 0) trueDamage = 0;
            
            currentArmor = newArmor;
            return trueDamage;
        }
    }

    public void AddArmor(int incomeArmor)
    {
        currentArmor += incomeArmor;
        armorUI.ShowArmorChange(currentArmor);

    }
}
