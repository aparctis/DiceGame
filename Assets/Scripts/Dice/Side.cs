using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    private int _sideIndex;
    public int sideIndex => _sideIndex;

    [SerializeField] private Vector3 _corectAngle;
    public Vector3 corectAngle => _corectAngle;

    [SerializeField] private Transform _actionHolder;
    public Transform actionHolder => _actionHolder;


    [SerializeField] private Transform _valueHolder;
    public Transform valueHolder => _valueHolder;

    public void SetIndex(int index)
    {
        _sideIndex = index;
    }
}
