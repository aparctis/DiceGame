[System.Serializable]
public class GameData
{
    public PlayerData savedPlayerData;
    public int lastLevelIndex;
    public int lastEnemyIndex;
}

[System.Serializable]
public class PlayerData
{
    //Health
    public int maxHealth;
    public int currentHealth;

    //Armor
    public int currentArmor;

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
    public int defoultHealth = 20;

    //Armor
    public int currentArmor;

    //Avatar
    public int avatarModelIndex;

    //Dices
    public DiceActionSet diceSet;

}