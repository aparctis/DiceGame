using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PlayAble
{
    [SerializeField] private EnemyAvatarManager _enemyAvatarManager;
    [SerializeField] private EnemyDiceController _enemyDiceController;

    public void SetEnemyData(EnemyData enemyData)
    {
        //avatar
        avatarModel = _enemyAvatarManager.getEnemyAvatar(enemyData.avatarModelIndex);

        //health
        healthController.SetHealth(enemyData.currentHealth, enemyData.maxHealth, 0);

        //armor
        armorController.SetArmor(enemyData.currentArmor);

        //diceset
        _enemyDiceController.DecoreDice(enemyData.diceSet);
    }

    public override void RollDice(UnityAction onDone)
    {
        _enemyDiceController.RollDice(onDone);
    }

    public override void UseActions(UnityAction onDone)
    {
        _enemyDiceController.UseActions(onDone);
    }

    public override void PrepereDice(UnityAction onDone)
    {
        _enemyDiceController.PrepereDice(onDone);
    }
    internal override void Death()
    {
        Debug.Log("ENEMY DEATH");
        base.Death();
    }

    public override void StopAll()
    {
        _enemyDiceController.StopAll();
    }
}
