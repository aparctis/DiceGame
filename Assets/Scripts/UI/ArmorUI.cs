using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ArmorUI : MonoBehaviour
{
    [SerializeField] private RectTransform line;
    [SerializeField] private RectTransform circle;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform textRect;

    [SerializeField, Range(0f, 1f)] private float scaleChangeOnBlink = 0.25f;

    private float blinkTime = 0.5f;
    private Ease ease = Ease.InOutCubic;

    private Sequence sequence;
    private int lastArmorValue;

    public void SetArmor(int startArmor)
    {
        text.text = startArmor.ToString();
        if(startArmor > 0)
        {
            line.localScale = Vector3.one;
            circle.localScale = Vector3.one;
            textRect.localScale = Vector3.one;
        }
        else
        {
            line.localScale = new Vector3(0, 1, 1);
            circle.localScale = Vector3.zero;
            textRect.localScale = Vector3.zero;
        }

        lastArmorValue = startArmor;
    }

    public void ShowArmorChange(int newArmor)
    {

        text.text = newArmor.ToString();

        if (newArmor == 0)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();

            sequence.Append(textRect.DOScale(0, blinkTime).SetEase(ease)).
                Join(line.DOScaleX(0, blinkTime).SetEase(ease)).
                Append(circle.DOScale(0, blinkTime).SetEase(ease));
        }
        else
        {
            float blinkScale;
            if (newArmor > lastArmorValue) blinkScale = 1 + scaleChangeOnBlink;
            else blinkScale = 1 - scaleChangeOnBlink;
            sequence?.Kill();
            sequence = DOTween.Sequence();

            if (lastArmorValue <= 0&&newArmor>0)
            {
                sequence.
                Append(circle.DOScale(blinkScale, blinkTime / 2)).
                Append(circle.DOScale(1, blinkTime / 2)).
                Append(textRect.DOScale(blinkScale, blinkTime / 2)).
                    Join(line.DOScaleX(blinkScale, blinkTime / 2)).
                    Append(textRect.DOScale(1, blinkTime / 2)).
                    Join(line.DOScaleX(1, blinkTime / 2));

            }

            else
            {

                sequence.
                    Append(textRect.DOScale(blinkScale, blinkTime / 2)).
                    Join(line.DOScaleX(blinkScale, blinkTime / 2)).
                    Append(textRect.DOScale(1, blinkTime / 2)).
                    Join(line.DOScaleX(1, blinkTime / 2));
            }

        }

        lastArmorValue = newArmor;
    }
}
