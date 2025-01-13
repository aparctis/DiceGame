using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceMover diceMover;
    [SerializeField] private Side[] sides;

    [SerializeField] private PlayAble ally;
    [SerializeField] private PlayAble oponent;

    [SerializeField] private bool isEnemyDice;

    private Vector3 startPosition = Vector3.zero;
    private Vector3 waitPosition;
    private Vector3 boardPosition;
    private Vector3 middlePosition;

    private Side upperSide;

    private ActionObjectPool actionObjectPool;
    private bool canClick = false;
    private DiceActionSet actionSet;

    [Inject]
    private void Construct(ActionObjectPool pool)
    {
        actionObjectPool = pool;
    }

    public void SetDiceActions(DiceActionSet newActionSet)
    {
        if (!sides.Length.Equals(6)) Debug.LogError("WRONG DICE SIDES LENGTH!");
        for (int i = 0; i < sides.Length; i++)
        {
            sides[i].SetAction(newActionSet.actions[i]);
        }
        actionSet = newActionSet;
    }

    public void SetPositions(Vector3 newWaitposition, Vector3 newBoardPosition, Vector3 newMiddlePosition)
    {
        waitPosition = newWaitposition;
        boardPosition = newBoardPosition;
        middlePosition = newMiddlePosition;

        //set start position if it was not setted before
        if (startPosition.Equals(Vector3.zero)) startPosition = transform.position;
    }

    public void MoveToBoard(float moveTime, UnityAction onMoveDone)
    {
        canClick = false;
        diceMover.MoveDice(boardPosition, true, moveTime, onMoveDone);
    }

    public void RollDice(Vector2 swipeDirection, UnityAction onDone)
    {
        diceMover.RollDice(swipeDirection, onDone);
    }

    public void RollDice(UnityAction onDone)
    {
        diceMover.RollDice(onDone);
    }

    public void ReturnDice(float moveTime, UnityAction onMoveDone)
    {
        canClick = true;
        DefineUpperSide();
        diceMover.MoveDice(waitPosition, upperSide.corectAngle, moveTime, onMoveDone);
    }

    public void HideDice()
    {
        
        canClick = false;
        DiceSetUI.instance?.Hide();
        diceMover.StopMove();
        diceMover.MoveDice(startPosition, true, 1, ()=> canClick = true);
    }


    public void UseAction(UnityAction callBack)
    {
        DiceAction action = upperSide.action;
        switch (action.type)
        {
            case ActionType.damage:
                UseSingleAction(callBack, ActionObjectType.atack, action.value);
                break;
            case ActionType.poison:
                UseSingleAction(callBack, ActionObjectType.poison, action.value);
                break;
            case ActionType.hill:
                UseSingleAction(callBack, ActionObjectType.health, action.value);
                break;
            case ActionType.armor:
                UseSingleAction(callBack, ActionObjectType.armor, action.value);
                break;
            case ActionType.damageAndPoison:
                UseSingleAction(()=>UseSingleAction(callBack, ActionObjectType.poison, action.secondValue), ActionObjectType.atack, action.value);
                break;
            case ActionType.damageAndArmor:
                UseSingleAction(() => UseSingleAction(callBack, ActionObjectType.armor, action.secondValue), ActionObjectType.atack, action.value);
                break;
        }
    }

    private void UseSingleAction(UnityAction onDone, ActionObjectType type, int value)
    {
        Debug.Log("Dice UseSingleAction");

        IActionReceiver receiver;
        if (type == ActionObjectType.atack || type == ActionObjectType.poison)
        {
            receiver = oponent;
        }
        else receiver = ally;

        Vector3 targetPosition = receiver.recieverPosition();
        ActionObject actionObject = actionObjectPool.getActionObject(type);
        actionObject.transform.position = upperSide.transform.position;
        actionObject.gameObject.SetActive(true);
        actionObject.ApplyAction(value, middlePosition, receiver, targetPosition, isEnemyDice, onDone);
    }




    private void DefineUpperSide()
    {
        int upperIndex = 0;
        for(int i = 1;  i < sides.Length; i++)
        {
            if (sides[i].transform.position.y > sides[upperIndex].transform.position.y) upperIndex = i;
        }
        upperSide = sides[upperIndex];
    }


    private void OnMouseDown()
    {
        Debug.Log("Click on dice");
        if (canClick)
        {
            DiceSetUI.instance?.ShowSet(actionSet);
        }
    }
}
