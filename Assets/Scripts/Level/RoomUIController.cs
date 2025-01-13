using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomUIController : MonoBehaviour
{
    [SerializeField] private RectTransform enemySide;
    [SerializeField] private RectTransform playerSide;
    [SerializeField] private RoundInfoUI roundInfoUI;

    [SerializeField] private Ease ease = Ease.InOutCubic;
    [SerializeField] private float appearTime = 1.0f;

    private Vector2 startEnemyPosition;
    private Vector2 startPlayerPosition;

    private Vector2 hidenEnemyPosition;
    private Vector2 hidenPlayerPosition;

    [SerializeField] private FadeUIScreen winScreen;
    [SerializeField] private FadeUIScreen loseScreen;

    public UnityAction onSceneRebooted;
    public UnityAction onNextRaund;


    private Sequence sequence;

    private float tickTime = 0.1f;

    private LevelLoader levelLoader;
    private SaveLoadSystem saveLoadSystem;

    public void Initialize(LevelLoader _levelLoader, SaveLoadSystem _saveLoadSystem)
    {
        Debug.Log("ROOM UI INITIALIZE");

        levelLoader = _levelLoader;
        saveLoadSystem = _saveLoadSystem;
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        InitPositions();
        FastHide();

        roundInfoUI.IniitializePositions();
    }

    private void InitPositions()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemySide);
        LayoutRebuilder.ForceRebuildLayoutImmediate(playerSide);

        startEnemyPosition = enemySide.anchoredPosition;
        startPlayerPosition = playerSide.anchoredPosition;

        hidenEnemyPosition = startEnemyPosition + new Vector2(0, enemySide.rect.height);
        hidenPlayerPosition = startPlayerPosition + new Vector2(0, -playerSide.rect.height);
    }

    public void ShowPlayers( UnityAction callBack)
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(enemySide.DOAnchorPos(startEnemyPosition, appearTime).SetEase(ease)).
            Join(playerSide.DOAnchorPos(startPlayerPosition, appearTime).SetEase(ease)).
            OnComplete(()=>callBack?.Invoke());
    }

    public void ShowRoundInfo(int round, string enemyName, UnityAction callBack)
    {
        roundInfoUI.ShowRoundInfo(round, enemyName, callBack);
    }

    private void HidePlayers(UnityAction callBack)
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(enemySide.DOAnchorPos(hidenEnemyPosition, appearTime).SetEase(ease)).
            Join(playerSide.DOAnchorPos(hidenPlayerPosition, appearTime).SetEase(ease)).
            OnComplete(() => callBack?.Invoke());
    }

    private void FastHide()
    {
        enemySide.anchoredPosition = startEnemyPosition + new Vector2(0, enemySide.rect.height);
        playerSide.anchoredPosition = startPlayerPosition + new Vector2(0, -playerSide.rect.height);
    }



    public void WinUI()
    {
        winScreen.Show();
    }

    public void LoseUI()
    {
        loseScreen.Show();
    }



    public void NextRaund()
    {
        onNextRaund.Invoke();
        Restart();
    }

    public void Restart()
    {
        StartCoroutine(RebootRutine());
    }

    public void Exit()
    {
        Application.Quit();
    }


    private IEnumerator RebootRutine()
    {
        bool isNextMove = false;
        winScreen.Hide();
        loseScreen.Hide();

        HidePlayers(()=>isNextMove=true);
        while(!isNextMove) yield return new WaitForSeconds(tickTime);

        levelLoader.FakeLoad(Random.Range(0.5f, 1.5f), onSceneRebooted);
    }
}