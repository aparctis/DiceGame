using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayAble
{
    [SerializeField] private EnemyAvatarManager _enemyAvatarManager;


    public void SetEnemyData(EnemyData enemyData)
    {
        //avatar
        _enemyAvatarManager.ActivateAvatar(enemyData.avatarModelIndex);
        //health
        _healthController.SetHealth(enemyData.currentHealth, enemyData.maxHealth);

        //armor

        //diceset
    }
}
