using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float fadeTime = 1.0f;
    Sequence sequence;

    private void Awake()
    {
        StartCoroutine(AnimationRutine());
    }

    public void Show()
    {
        StartCoroutine(AnimationRutine());
        sequence?.Kill();
        sequence = DOTween.Sequence();

        sequence.Append(group.DOFade(0, fadeTime));
    }

    public void Hide()
    {
        Debug.Log("Hide loading screen");
        sequence?.Kill();
        sequence = DOTween.Sequence();

        sequence.Append(group.DOFade(0, fadeTime)).OnComplete(OnFaded);

        void OnFaded()
        {
            StopAllCoroutines();
            group.blocksRaycasts = false;
        }
    }


    private IEnumerator AnimationRutine()
    {
        group.blocksRaycasts = true;

        int dotsCount = 3;
        float delay = 0.2f;
        while (true)
        {
            text.text = "Loading";
            for (int i = 0; i < dotsCount; i++)
            {
                text.text += ".";
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
