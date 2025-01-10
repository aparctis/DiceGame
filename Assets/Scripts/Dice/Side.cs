using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Side : MonoBehaviour
{
    private ActionDecorPool _actionDecorPool;

    [Inject]
    private void Construct(ActionDecorPool actionDecorPool)
    {
        _actionDecorPool = actionDecorPool;
    }

    [SerializeField] private Vector3 _corectAngle;
    public Vector3 corectAngle => _corectAngle;


    [SerializeField] private Transform _actionHolder;
    [SerializeField] private Transform _valueHolder;
    [SerializeField] private Transform _secondValueHolder;


    private GameObject decoreObject;
    private GameObject decoreFirstValue;
    private GameObject decoreSecondValue;

    private DiceAction _action = null;
    public DiceAction action => _action;

    public void SetAction(DiceAction newAction)
    {
        if(_action!=null)
        {
            ActionType lastType = _action.type;
            int lastFirstValue = _action.value;
            int lastSecondValue = _action.secondValue;

            //return decore
            if(decoreObject!= null)
            {
                _actionDecorPool.ReturnActionObject(decoreObject, lastType);
                decoreObject = null;
            }

            //return value
            if (decoreFirstValue != null)
            {
                _actionDecorPool.ReturnDigitObject(decoreFirstValue, lastFirstValue);
                decoreFirstValue = null;
            }

            //return second value
            if (decoreSecondValue != null)
            {
                _actionDecorPool.ReturnDigitObject(decoreSecondValue, lastSecondValue);
                decoreSecondValue = null;
            }
        }

        //decore
        decoreObject = _actionDecorPool.getActionObject(newAction.type);
        decoreObject.transform.parent = _actionHolder;
        decoreObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        decoreObject.SetActive(true);

        if (newAction.value > 0)
        {
            decoreFirstValue = _actionDecorPool.getDigitObject(newAction.value);
            decoreFirstValue.transform.parent = _valueHolder;
            decoreFirstValue.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            decoreFirstValue.SetActive(true);
        }

        if (newAction.secondValue > 0)
        {
            decoreSecondValue = _actionDecorPool.getDigitObject(newAction.secondValue);
            decoreSecondValue.transform.parent = _secondValueHolder;
            decoreSecondValue.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            decoreSecondValue.SetActive(true);
        }

    }


}
