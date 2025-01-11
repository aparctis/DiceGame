using UnityEngine;
[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public EnemyData[] enemyDatas;
    public int lastLevelIndex;
}

[System.Serializable]
public class PlayerData
{
    //Health
    public int maxHealth;
    public int currentHealth;

    //Armor
    public int defoultArmor;

    //poison
    public int currentPoison;

    //Dices
    public DiceActionSet hand_1;
    public DiceActionSet hand_2;
    public DiceActionSet armor;
    public DiceActionSet amulet;
    public DiceActionSet pet;
    
}

[System.Serializable]
public class EnemyData
{
    //Health
    public int maxHealth;
    public int currentHealth;

    //Armor
    public int currentArmor;

    //Avatar
    public int avatarModelIndex;

    //Dices
    public DiceActionSet diceSet;
}


