using UnityEngine;
using System.IO;
using Zenject;

public class SaveLoadSystem : MonoBehaviour
{
    private GameSet defaultGameSet;
    private GameData savedGameData;
    public bool isNewGame { get; private set; }
    private string savePath;
    public int levelsCount = 12;

    [Inject]
    private void Construct(GameSet defaultGameSet)
    {
        this.defaultGameSet = defaultGameSet;
        Debug.Log($"SaveLoadSystem constructed: defaultGameSet is {(defaultGameSet != null ? "not null" : "null")}");
    }

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savedata.json");

    }

    private void LoadSaves()
    {

        if (File.Exists(savePath))
        {
            try
            {
                string jsonData = File.ReadAllText(savePath);
                savedGameData = JsonUtility.FromJson<GameData>(jsonData);
                isNewGame = false;
            }
            catch (System.Exception e)
            {
                ConstructAndSaveData();
            }
        }
        else ConstructAndSaveData();
    }

    private void ConstructAndSaveData()
    {
        savedGameData = new GameData();

        //player
        if (!defaultGameSet) Debug.LogError("defaultGameSet is NULL!");
        savedGameData.playerData = defaultGameSet.playerData;
        //last level
        savedGameData.lastLevelIndex = 0;

        //enemys
        EnemyData[] enemysRandomList = new EnemyData[levelsCount];
        for(int i =0; i<levelsCount; i++)
        {
            if (i < defaultGameSet.enemyData.Count) enemysRandomList[i] = defaultGameSet.enemyData[i];
            else enemysRandomList[i] = defaultGameSet.enemyData[Random.Range(0, defaultGameSet.enemyData.Count)];
        }
        savedGameData.enemyDatas = enemysRandomList;

        SaveData();
    }

    public GameData getGameData()
    {
        if (savedGameData == null) ConstructAndSaveData();
        return savedGameData;

    }


    private void SaveData()
    {
        if (savedGameData == null)
        {
            Debug.LogError("Cannot save: savedGameData is null!");
            return;
        }

        try
        {
            string jsonData = JsonUtility.ToJson(savedGameData, true);
            File.WriteAllText(savePath, jsonData);
            Debug.Log("Game saved successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving game: {e}");
        }
    }

    public void SavePlayerData(int playerCurrentHealth, int playerPoison)
    {
        if (savedGameData?.playerData == null)
        {
            Debug.LogError("Cannot save player health: savedGameData or playerData is null!");
            return;
        }

        savedGameData.playerData.currentHealth = playerCurrentHealth;
        savedGameData.playerData.currentPoison = playerPoison;
        SaveData();
    }

    public void SaveLevel(int level)
    {
        if (savedGameData == null)
        {
            Debug.LogError("Cannot save level: savedGameData is null!");
            return;
        }

        savedGameData.lastLevelIndex = level;
        SaveData();
    }
}