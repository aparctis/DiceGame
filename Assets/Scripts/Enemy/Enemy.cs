using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayAble
{
    [SerializeField] private EnemyAvatarManager _enemyAvatarManager;
    [SerializeField] private EnemyDiceController _enemyDiceController;

    public void SetEnemyData(EnemyData enemyData)
    {
        //avatar
        avatarModel = _enemyAvatarManager.ActivateAvatar(enemyData.avatarModelIndex);

        //health
        healthController.SetHealth(enemyData.currentHealth, enemyData.maxHealth);

        //armor
        armorController.SetArmor(enemyData.currentArmor);

        //diceset
    }
}
