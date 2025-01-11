using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemyDiceController : MonoBehaviour
{
    [SerializeField] private Dice dice;
    [SerializeField] private float delay = 0.5f;
    private float tickTime = 0.05f;
    private float flyTime = 1.0f;

    private PositionConverter positionConverter;
    [SerializeField] private RectTransform dicePlaceHolder;
    [SerializeField] private Transform beforeRollposition;
    [SerializeField] private Transform boardMiddle;

    [Inject]
    private void Construct(PositionConverter _converter)
    {
        positionConverter = _converter;
    }

    public void DecoreDice(DiceActionSet actionSet)
    {
        dice.SetDiceActions(actionSet);
    }

    public void PrepereDice(UnityAction callBack)
    {
        SetPositions();
        StartCoroutine(ReturnRutine(callBack));
    }
    public void RollDice(UnityAction onDone)
    {
        StartCoroutine(RollRutine(onDone));
    }

    public void UseActions(UnityAction onDone)
    {
        dice.UseAction(onDone);
    }
    private void SetPositions()
    {
        Vector3 wait = positionConverter.GetWorldPosition(dicePlaceHolder, 2);
        Vector3 board = beforeRollposition.position;
        Vector3 middle = boardMiddle.position;
        dice.SetPositions(wait, board, middle);
    }

    private IEnumerator RollRutine(UnityAction callBack)
    {
        SetPositions();

        bool isDone = false;
        dice.MoveToBoard(flyTime, () => isDone = true);
        while (!isDone) yield return new WaitForSeconds(tickTime);
        yield return new WaitForSeconds(delay);

        isDone = false;
        dice.RollDice(() => isDone = true);
        while (!isDone) yield return new WaitForSeconds(tickTime);
        yield return new WaitForSeconds(delay);

        StartCoroutine(ReturnRutine(callBack));
    }


    private IEnumerator ReturnRutine(UnityAction callBack)
    {
        bool isDone = false;
        dice.ReturnDice(flyTime, () => isDone = true);
        while (!isDone) yield return new WaitForSeconds(tickTime);
        callBack?.Invoke();
    }
}
