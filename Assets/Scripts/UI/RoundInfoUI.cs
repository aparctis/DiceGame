using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;


public class RoundInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundInfoText;
    [SerializeField] private TextMeshProUGUI enemyNameText;

    [SerializeField] private RectTransform infoTransform;
    [SerializeField] private RectTransform enemyTransform;

    private Vector2 startEnemyPosition;
    private Vector2 startInfoPosition;

    private Vector2 hidenEnemyPosition;
    private Vector2 hidenInfoPosition;

    Sequence sequence;

    [SerializeField]private Ease appearEase = Ease.OutElastic;
    [SerializeField] private Ease hideEase = Ease.InBack;
    [SerializeField]private float moveTime = 1.0f;
    [SerializeField]private float delay = 2.0f;



    public void IniitializePositions()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemyTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(infoTransform);

        startEnemyPosition = enemyTransform.anchoredPosition;
        startInfoPosition = infoTransform.anchoredPosition;

        hidenEnemyPosition = startEnemyPosition + new Vector2(enemyTransform.rect.width, 0);
        hidenInfoPosition = startInfoPosition + new Vector2(-infoTransform.rect.width, 0);

        FastHide();
    }

    public void ShowRoundInfo(int round, string enemyName, UnityAction callBack)
    {
        enemyNameText.text = enemyName;
        roundInfoText.text = ($"ROUND {round}");

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(enemyTransform.DOAnchorPos(startEnemyPosition, moveTime).SetEase(appearEase)).Join(infoTransform.DOAnchorPos(startInfoPosition, moveTime).SetEase(appearEase)).
            AppendInterval(delay).
            Append(enemyTransform.DOAnchorPos(hidenEnemyPosition, moveTime).SetEase(hideEase)).Join(infoTransform.DOAnchorPos(hidenInfoPosition, moveTime).SetEase(hideEase)).
            OnComplete(()=>callBack.Invoke());


    }

    public void FastHide()
    {
        enemyTransform.anchoredPosition = hidenEnemyPosition;
        infoTransform.anchoredPosition = hidenInfoPosition;
    }
}
