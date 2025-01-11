using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class FadeUIScreen : MonoBehaviour
{
    public bool showSettings = false;

    [ShowIf("showSettings")]
    [SerializeField] private CanvasGroup content;
    [ShowIf("showSettings")]
    [SerializeField] private float fadeTime = 0.5f;
    [ShowIf("showSettings")]
    [SerializeField] private Ease fadeEase = Ease.InOutCubic;

    private Sequence sequence;


    public void Show()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(content.DOFade(1, fadeTime)).SetEase(fadeEase).OnComplete(OnShowed);

        void OnShowed()
        {
            content.interactable = true;
            content.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(content.DOFade(0, fadeTime)).SetEase(fadeEase).OnComplete(OnShowed);

        void OnShowed()
        {
            content.interactable = false;
            content.blocksRaycasts = false;
        }
    }
}
