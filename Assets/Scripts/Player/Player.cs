using UnityEngine;
using UnityEngine.Events;

public class Player : PlayAble
{
    [SerializeField] PlayerDiceController playerDiceController;

    public int healthLeft => healthController.healthLeft;
    public int poisonLeft => healthController.poisonLeft;

    public int playerHealthLeft => healthController.healthLeft;
    public void SetPlayerData(PlayerData data)
    {
        healthController.SetHealth(data.currentHealth, data.maxHealth, data.currentPoison);
        armorController.SetArmor(0);
        playerDiceController.DecoreDice(data.hand_1, data.hand_2, data.armor, data.amulet, data.pet);
    }

    public override void PrepereDice(UnityAction onDone)
    {
        playerDiceController.PrepereDice(onDone);
    }
    public override void RollDice(UnityAction onDone)
    {
        playerDiceController.RollDice(onDone);
    }

    public override void UseActions(UnityAction onDone)
    {
        Debug.Log("Player Use actions");
        playerDiceController.UseActions(onDone);
    }

    internal override void Death()
    {
        Debug.Log("PLAYER DEATH");
        base.Death();
    }

    public override void StopAll()
    {
        playerDiceController.StopAll();
    }
}
