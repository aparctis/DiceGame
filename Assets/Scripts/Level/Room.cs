using System.Collections;
using UnityEngine;
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
        Debug.Log("Enable)");
        player.onDeath += OnLose;
        enemy.onDeath += OnWin;
        levelLoader.onLoadingOver += PlayLoaded;
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable)");

        player.onDeath -= OnLose;
        enemy.onDeath -= OnWin;
        levelLoader.onLoadingOver -= PlayLoaded;

    }

    private void LoadData()
    {
        Debug.Log("LoadData");

        if (saveLoadSystem == null) Debug.Log("SLS is NULL!");
        if(saveLoadSystem.getGameData()==null) Debug.Log("DATA is NULL!");

        GameData data = saveLoadSystem.getGameData();
        Debug.Log(data.playerData.currentHealth);


        player.SetPlayerData(data.playerData);
        enemy.SetEnemyData(data.enemyDatas[data.lastLevelIndex]);
        maxRaundIndex = saveLoadSystem.levelsCount;
    }


    private IEnumerator RaundRutine()
    {
        Debug.Log("RaundRutine");

        LoadData();
        yield return null;

        bool isNextMove = false;

        while (true)
        {
            Debug.Log("poison");

            //poison
            enemy.ApplyPoison();
            yield return new WaitForSeconds(delay);
            player.ApplyPoison();
            yield return new WaitForSeconds(delay);

            Debug.Log("move");

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
        Debug.Log("PlayLoaded");
        StartCoroutine(RaundRutine());
    }


    public void ButtonRestart()
    {
        levelLoader.FakeLoad(1.05f);
    }


    public void ButtonNext()
    {
        currentRaundIndex++;
        levelLoader.FakeLoad(1.05f);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
