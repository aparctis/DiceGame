using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;

    [SerializeField] private Color backOnDamage = Color.yellow;
    [SerializeField] private Color backOnPoison = Color.green;
    [SerializeField] private Color backOnHill = Color.cyan;

    [SerializeField] private Ease ease = Ease.InOutCubic;

    /// <summary>
    /// Seconds to fill from 0 to 1.0
    /// </summary>
    [SerializeField] private float fillSpeed = 2.0f;

    private Sequence sequence;

    public void SetStartHealth(int currentInt, int maxInt)
    {
        text.text = (currentInt + "/" + maxInt);
        float fill = (float)currentInt / (float)maxInt;
        frontImage.fillAmount = fill;
        backImage.fillAmount = fill;
    }

    public void ShowDamage(int currentInt, int maxInt)
    {
        float newAmount = (float)currentInt / (float)maxInt;
        float time = moveTime(newAmount);

        backImage.color = backOnDamage;

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(frontImage.DOFillAmount(newAmount, time / 2).SetEase(ease)).
            Join(backImage.DOFillAmount(newAmount, time).SetEase(ease));
    }

    public void ShowPoison(int currentInt, int maxInt)
    {
        float newAmount = (float)currentInt / (float)maxInt;
        float time = moveTime(newAmount);

        backImage.color = backOnPoison;

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(frontImage.DOFillAmount(newAmount, time / 2).SetEase(ease)).
            Join(backImage.DOFillAmount(newAmount, time).SetEase(ease));
    }

    public void ShowHill(int currentInt, int maxInt)
    {
        float newAmount = (float)currentInt / (float)maxInt;
        float time = moveTime(newAmount);

        backImage.color = backOnDamage;

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(frontImage.DOFillAmount(newAmount, time).SetEase(ease)).
            Join(backImage.DOFillAmount(newAmount, time / 2).SetEase(ease));
    }

    private float moveTime(float newAmount)
    {
        float currentAmount = frontImage.fillAmount;
        float distance = Mathf.Abs(currentAmount - newAmount);
        return distance*fillSpeed;
    }
}
