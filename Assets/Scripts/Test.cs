using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Zenject;
using System;
using System.IO;

public class Test : MonoBehaviour
{
    [SerializeField] private TestScriptableData sd_input;
    [SerializeField] private TestScriptableData sd_out;

    public TestData data_out;
    [Inject]
    SaveLoadSystem sls;
    public GameData game_data;

    string savePas;

    private void Awake()
    {
        savePas = Path.Combine(Application.persistentDataPath, "testData_DELETE_IT.json");
    }
    [Button]
    private void LoadPlayerData()
    {
        game_data=(sls.getGameData());

    }

    [Button]
    private void SaveInput()
    {
        SaveDataFrom(sd_input.data);
    }

    [Button]
    private void LoadFromJSon()
    {
        string jsonData = File.ReadAllText(savePas);
        data_out = JsonUtility.FromJson<TestData>(jsonData);

        sd_out.data = data_out;
    }


    private void SaveDataFrom (TestData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePas, jsonData);
    }

}

[Serializable]
public class TestData
{
    public DiceActionSet set;
}





