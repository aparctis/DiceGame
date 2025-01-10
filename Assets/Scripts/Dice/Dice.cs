using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceMover diceMover;
    [SerializeField] private Side[] sides;

    
    private Vector3 waitPosition;
    private Vector3 boardPosition;

    private Side upperSide;

    public void SetDiceActions(DiceActionSet newActionSet)
    {
        if (!sides.Length.Equals(6)) Debug.LogError("WRONG DICE SIDES LENGTH!");
        for (int i = 0; i < sides.Length; i++)
        {
            sides[i].SetAction(newActionSet.actions[i]);
        }
    }

    public void SetPositions(Vector3 newWaitposition, Vector3 newBoardPosition)
    {
        waitPosition = newWaitposition;
        boardPosition = newBoardPosition;
    }

    public void MoveToBoard(float moveTime, UnityAction onMoveDone)
    {
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
        DefineUpperSide();
        diceMover.MoveDice(waitPosition, upperSide.corectAngle, moveTime, onMoveDone);
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
}
