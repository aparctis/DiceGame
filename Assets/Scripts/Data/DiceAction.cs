using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionType { damage=0, armor=1, hill=2, poison=3, damageAndArmor=4, damageAndPoison=5}
[System.Serializable]
public class DiceAction
{
    [SerializeField] private ActionType _type;
    public ActionType type => _type;

    [SerializeField] private int _value;

    //for poison (or other multiple and delayed effects)
    //for defined count of poison hits
    /*[SerializeField] */private int _hitCount;

    [SerializeField] private int _hitValue;

    public int value => _value;
    public int hitCount => _hitCount;
    public int hitValue => _hitValue;

    public DiceAction(ActionType actionType, int actionValue, int actionHitCount, int hitValue)
    {
        this._type = actionType;
        this._value = actionValue;
        this._hitCount = actionHitCount;
        this._hitValue = hitValue;
    }
}

[System.Serializable]
public class DiceActionSet
{
    [SerializeField] private DiceAction[] _actions = new DiceAction[6];
    public DiceAction[] actions => _actions;

    public DiceActionSet() { _actions = new DiceAction[6]; }

    public void ReplaceAction(DiceAction newAction, int inSetIndex)
    {
        if (inSetIndex >= 0 && inSetIndex < _actions.Length)
        {
            _actions[inSetIndex] = newAction;
        }
        else
        {
            Debug.LogError($"Index {inSetIndex} is out of array");
        }
    }

    /// <summary>
    /// Array length must be 6.
    /// </summary>
    /// <param name="newActions"></param>
    public DiceActionSet(DiceAction[] newActions)
    {
        if (newActions.Length != 6)
        {
            throw new System.ArgumentException("Array length must be 6");
        }

        _actions = newActions;
    }

    public DiceActionSet(DiceAction action1, DiceAction action2, DiceAction action3, DiceAction action4, DiceAction action5, DiceAction action6)
    {
        _actions[0] = action1;
        _actions[1] = action2;
        _actions[2] = action3;
        _actions[3] = action4;
        _actions[4] = action5;
        _actions[5] = action6;
    }
}

