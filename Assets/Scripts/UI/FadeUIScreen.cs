using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeUIScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup content;
    [SerializeField] private float fadeTime = 0.5f;
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
