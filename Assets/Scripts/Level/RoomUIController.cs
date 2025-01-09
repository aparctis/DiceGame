using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIController : MonoBehaviour
{
    [SerializeField] private RectTransform enemySide;
    [SerializeField] private RectTransform playerSide;

    [SerializeField] private Ease ease = Ease.InOutCubic;
    [SerializeField] private float appearTime = 1.0f;

    private Vector2 startEnemyPosition;
    private Vector2 startPlayerPosition;

    private Sequence sequence;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        InitPositions();
        HideAndShow();
    }

    private void InitPositions()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemySide);
        LayoutRebuilder.ForceRebuildLayoutImmediate(playerSide);

        startEnemyPosition = enemySide.anchoredPosition;
        startPlayerPosition = playerSide.anchoredPosition;
    }

    private void HideAndShow()
    {
        enemySide.anchoredPosition = startEnemyPosition + new Vector2(0, enemySide.rect.height);
        playerSide.anchoredPosition = startPlayerPosition + new Vector2(0, -playerSide.rect.height);

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(enemySide.DOAnchorPos(startEnemyPosition, appearTime).SetEase(ease)).
            Join(playerSide.DOAnchorPos(startPlayerPosition, appearTime).SetEase(ease));
    }
}