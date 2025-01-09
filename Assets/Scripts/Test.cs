using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public HealthUI healthUI;
    public ArmorUI armorUI;

    int max;
    int current;


    public int newArmor = 1;

    private void Start()
    {
        current = 20;
        max = 20;
        healthUI.SetStartHealth(20, 20);
        SetArmor();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Damage();
        if (Input.GetKeyDown(KeyCode.P)) Poison();
        if (Input.GetKeyDown(KeyCode.H)) Hill();

        if (Input.GetKeyDown(KeyCode.Space)) ChangeArmor();

    }

    private void Damage()
    {
        current--;
        healthUI.ShowDamage(current, max);
    }

    private void Poison()
    {
        current--;
        healthUI.ShowPoison(current, max);
    }

    private void Hill()
    {
        current++;
        healthUI.ShowHill(current, max);
    }


    private void SetArmor()
    {
        armorUI.SetArmor(0);
    }

    private void ChangeArmor()
    {
        armorUI.ShowArmorChange(newArmor);
    }
}
