using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Zenject;

public class Test : MonoBehaviour
{
    public Dice dice;
    public DiceActionSet actionSet;

    public RectTransform rectTransform;
    public Transform boardTrans;
    public float moveTime = 1.0f;
    [Inject] PositionConverter positionConverter;

    [Button]
    private void Decore()
    {
        dice.SetDiceActions(actionSet);
    }
    [Button]
    private void SetPositions()
    {
        Vector3 wait = positionConverter.GetWorldPosition(rectTransform, 2);
        Vector3 board = boardTrans.position;
        dice.SetPositions(wait, board);
    }
    [Button]

    private void MoveToBoard()
    {
        dice.MoveToBoard(moveTime, ()=>OnDone());
    }
    [Button]

    private void Roll()
    {
        dice.RollDice(() => OnDone());
    }
    [Button]

    private void ReturnDice()
    {
        dice.ReturnDice(moveTime, () => OnDone());
    }

    private void OnDone() => Debug.Log("ON DONE");
  
}
