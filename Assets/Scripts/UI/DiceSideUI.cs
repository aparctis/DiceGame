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
    [SerializeField] private Image bg;

    private int index;
    public UnityAction<int> onClicked;

    public void Initialize(int i)
    {
        index = i;
    }

    public void Ckick()
    {
        onClicked?.Invoke(index);
        Select();
    }

    public void Select()
    {
        StartCoroutine(SelectAnimationRutine());
    }

    public void UnSelect()
    {
        StopAllCoroutines();
    }

    private IEnumerator SelectAnimationRutine()
    {
        while (true)
        {
            selection.DOKill();
            selection.DOFade(1, 1);

            selection.DOKill();
            selection.DOFade(0, 1);

        }
    }


}
