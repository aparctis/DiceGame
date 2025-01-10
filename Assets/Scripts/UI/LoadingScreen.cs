using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;


public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float fadeTime = 1.0f;
    public float time => fadeTime;

    public UnityAction onLoadingScreenHided;

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
        sequence?.Kill();
        sequence = DOTween.Sequence();

        sequence.Append(group.DOFade(0, fadeTime)).OnComplete(OnFaded);

        void OnFaded()
        {
            StopAllCoroutines();
            group.blocksRaycasts = false;
            onLoadingScreenHided?.Invoke();
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
