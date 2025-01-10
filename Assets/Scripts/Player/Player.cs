using UnityEngine;
using UnityEngine.Events;

public class Player : PlayAble
{
    [SerializeField] PlayerDiceController playerDiceController;

    public int playerHealthLeft => healthController.healthLeft;
    public void SetPlayerData(PlayerData data)
    {
        healthController.SetHealth(data.currentHealth, data.maxHealth);

        playerDiceController.DecoreDice(data.hand_1, data.hand_2, data.armor, data.amulet, data.pet);
    }

    public override void RollDice(UnityAction onDone)
    {
        playerDiceController.RollDice(onDone);
    }

    public override void UseActions(UnityAction onDone)
    {
        playerDiceController.UseActions(onDone);
    }

    internal override void Death()
    {
        base.Death();
    }
}
