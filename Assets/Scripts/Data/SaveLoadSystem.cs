using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    private GameData savedGameData;
    [SerializeField] private GameSet defoultData;
    public bool isNewGame{ get; private set; }

    private string savePath;

    private void Awake()
    {
        //SetDataDefoult();
        Debug.Log("SLS awake");
        LoadSaves();
        Debug.Log("SLS player health = "+savedGameData.savedPlayerData.currentHealth);
        SavePlayerHealth(10);
    }

    //load saved data if there is save
    private void LoadSaves()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savedata.json");
        if (File.Exists(savePath))
        {
            Debug.Log("SLS FILE EXISTS");
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
        Debug.Log("SLS FILE DEFOULT");

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
