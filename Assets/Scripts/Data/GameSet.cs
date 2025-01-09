using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameDataSet")]

public class GameSet : ScriptableObject
{
    /// <summary>
    /// player start settings
    /// </summary>
    public PlayerData playerData;

    /// <summary>
    /// Enemys 
    /// </summary>
    public List<EnemyData> enemyData = new List<EnemyData>();
}
