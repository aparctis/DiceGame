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

    private float timeTick = 0.05f;
    private float delay = 0.5f;

    private GameData data;

    [SerializeField] private RoomUIController roomUIController;


    [Inject]
    private void Construct(SaveLoadSystem sls, LevelLoader loader)
    {
        saveLoadSystem = sls;
        levelLoader = loader;
    }

    private void OnEnable()
    {
        Suscribe();
    }

    private void OnDisable()
    {
        Unsuscribe();
    }

    private void Suscribe()
    {
        Debug.Log("ROOM SUSCRIBVE");
        player.onDeath += Lose;
        enemy.onDeath += Win;

        roomUIController.onNextRaund += NextLevel;
        roomUIController.onSceneRebooted += StartRaund;
    }
    private void Unsuscribe()
    {
        Debug.Log("ROOM UNSUSCRIBVE");
        player.onDeath -= Lose;
        enemy.onDeath -= Win;
        roomUIController.onNextRaund -= NextLevel;
        roomUIController.onSceneRebooted -= StartRaund;

    }

    private void Awake()
    {
        roomUIController.Initialize(levelLoader, saveLoadSystem);
    }

    private void Start()
    {
        StartRaund();
    }

    private void StartRaund()
    {
        Debug.Log("START RAUND");

        StartCoroutine(RaundRutine());
    }

    private IEnumerator RaundRutine()
    {
        Debug.Log("LoadData");

        if (saveLoadSystem == null) Debug.Log("SLS is NULL!");
        if (saveLoadSystem.getGameData() == null) Debug.Log("DATA is NULL!");

        GameData data = saveLoadSystem.getGameData();

        player.SetPlayerData(data.playerData);
        enemy.SetEnemyData(data.enemyDatas[data.lastLevelIndex]);


        bool isNextMove = false;

        //show players
        roomUIController.ShowPlayers(()=>isNextMove = true);
        while (!isNextMove) yield return new WaitForSeconds(timeTick);

        //show round info
        isNextMove = false;
        roomUIController.ShowRoundInfo(data.lastLevelIndex+1, data.enemyDatas[data.lastLevelIndex].name, () => isNextMove = true);
        while (!isNextMove) yield return new WaitForSeconds(timeTick);


        //show dices
        int dicesChowed = 0;
        player.PrepereDice(()=>dicesChowed++);
        enemy.PrepereDice(() => dicesChowed++);
        while (dicesChowed<2) yield return new WaitForSeconds(timeTick);



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

            Debug.Log("Use actions");

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

    private void Win()
    {
        Debug.Log("WIN");
        StopAllCoroutines();

        player.StopAll();
        enemy.StopAll();

        
        roomUIController.WinUI();
    }

    private void Lose()
    {
        Debug.Log("LOSE");
        StopAllCoroutines();

        player.StopAll();
        enemy.StopAll();

        
        roomUIController.LoseUI();
    }

    private void NextLevel()
    {
        currentRaundIndex++;
        saveLoadSystem.SaveLevel(currentRaundIndex);
        saveLoadSystem.SavePlayerData(player.healthLeft, player.poisonLeft);
    }
}
