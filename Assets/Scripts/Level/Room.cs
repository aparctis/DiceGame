using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Room : MonoBehaviour
{
    private SaveLoadSystem saveLoadSystem;
    private LevelLoader levelLoader;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    private int currentRaundIndex;
    private int maxRaundIndex;

    private float timeTick = 0.05f;
    private float delay = 0.5f;

    [SerializeField] private RoomUIController roomUIController;


    [Inject]
    private void Construct(SaveLoadSystem sls, LevelLoader loader)
    {
        saveLoadSystem = sls;
        levelLoader = loader;
    }

    private void OnEnable()
    {
        player.onDeath += OnLose;
        enemy.onDeath += OnWin;
        levelLoader.onLoadingOver += PlayLoaded;
    }
    private void OnDisable()
    {
        player.onDeath -= OnLose;
        enemy.onDeath -= OnWin;
        levelLoader.onLoadingOver -= PlayLoaded;

    }

    private void LoadData()
    {
        player.SetPlayerData(saveLoadSystem.gameData.savedPlayerData);
        enemy.SetEnemyData(saveLoadSystem.gameData.enemys[saveLoadSystem.gameData.lastLevelIndex]);
        currentRaundIndex = saveLoadSystem.gameData.lastLevelIndex;
        maxRaundIndex = saveLoadSystem.levelsCount;
    }


    private IEnumerator RaundRutine()
    {
        LoadData();
        yield return null;

        bool isNextMove = false;

        while (true)
        {
            //poison
            isNextMove = false;
            enemy.ApplyPoison(()=>isNextMove=true);
            while(!isNextMove) yield return new WaitForSeconds(timeTick);

            isNextMove = false;
            player.ApplyPoison(() => isNextMove = true);
            while (!isNextMove) yield return new WaitForSeconds(timeTick);

            //enemy move
            isNextMove = false;
            enemy.RollDice(() => isNextMove = true);
            while (!isNextMove) yield return new WaitForSeconds(timeTick);

            //plaer move
            isNextMove = false;
            player.RollDice(() => isNextMove = true);
            while (!isNextMove) yield return new WaitForSeconds(timeTick);

            //player action
            isNextMove = false;
            player.UseActions(() => isNextMove = true);
            while (!isNextMove) yield return new WaitForSeconds(timeTick);

            yield return new WaitForSeconds(delay);

            //enemy action
            isNextMove = false;
            enemy.UseActions(() => isNextMove = true);
            while (!isNextMove) yield return new WaitForSeconds(timeTick);
        }

    }

    private void OnWin()
    {
        StopAllCoroutines();
        roomUIController.WinUI();
    }

    private void OnLose()
    {
        StopAllCoroutines();
        roomUIController.LoseUI();
    }


    //for Buttons
    private void PlayLoaded()
    {
        LoadData();
        StartCoroutine(RaundRutine());

    }


    public void ButtonRestart()
    {
        levelLoader.FakeLoad(1.05f);
    }


    public void ButtonNext()
    {
        currentRaundIndex++;
        saveLoadSystem.SaveLevel(currentRaundIndex, saveLoadSystem.gameData.lastEnemyIndex+1);

        levelLoader.FakeLoad(1.05f);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
