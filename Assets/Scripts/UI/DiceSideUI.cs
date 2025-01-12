using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;




public class DiceSideUI : MonoBehaviour
{
    [SerializeField] private Image actionImage;
    [SerializeField] private Image selection;

    private int index;
    public UnityAction<int> onClicked;

    private Sequence sequence;

    public void Initialize(int i)
    {
        index = i;
    }

    public void SetImage(Sprite image)
    {
        actionImage.sprite = image;
    }

    public void Ckick()
    {
        onClicked?.Invoke(index);
    }

    public void Select()
    {
        StartCoroutine(SelectAnimationRutine());
    }

    public void UnSelect()
    {
        StopAllCoroutines();
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(selection.DOFade(0, 0.25f));
    }

    private IEnumerator SelectAnimationRutine()
    {
        float blinkTime = 0.5f;
        while (true)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.
                Append(selection.DOFade(1, blinkTime / 2).SetEase(Ease.InOutCubic)).
                Append(selection.DOFade(0, blinkTime / 2).SetEase(Ease.InOutCubic));

            yield return new WaitForSeconds(blinkTime);
            yield return null;
        }
    }


}
