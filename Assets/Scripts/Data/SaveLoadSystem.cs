using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    private GameData savedGameData;
    public GameData gameData => savedGameData;

    [SerializeField] private GameSet defoultData;
    public bool isNewGame{ get; private set; }

    private string savePath;

    public int levelsCount = 12;

    private void Awake()
    {
        LoadSaves();
    }

    //load saved data if there is save
    private void LoadSaves()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savedata.json");
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            savedGameData = JsonUtility.FromJson<GameData>(jsonData);
            isNewGame = false;
            

        }
        else SetDataDefoult();
    }

    //load defoult data if there is no saved data
    private void SetDataDefoult()
    {
        if (savedGameData == null) savedGameData = new GameData();
        savedGameData.savedPlayerData = defoultData.playerData;
        savedGameData.lastEnemyIndex = 0;
        savedGameData.lastLevelIndex = 0;

        for(int i = 0; i< levelsCount; i++)
        {
            EnemyData data;
            if(i<=defoultData.enemyData.Count) data = defoultData.enemyData[i];
            else data = defoultData.enemyData[Random.RandomRange(0, defoultData.enemyData.Count)];
            savedGameData.enemys.Add(data);

        }

    }

    private void SaveData()
    {
        string jsonData = JsonUtility.ToJson(savedGameData, true); 
        File.WriteAllText(savePath, jsonData);
    }

    public void SavePlayerHealth(int playerCurrentHelth)
    {
        savedGameData.savedPlayerData.currentHealth = playerCurrentHelth;
        SaveData();
    }

    public void SaveLevel(int level, int enemyIndex)
    {
        savedGameData.lastEnemyIndex = enemyIndex;
        savedGameData.lastLevelIndex = level;
        SaveData();
    }
}
