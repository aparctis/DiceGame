using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerDiceController : MonoBehaviour
{
    [SerializeField] private Dice hand_1;
    [SerializeField] private Dice hand_2;
    [SerializeField] private Dice armor;
    [SerializeField] private Dice amulet;
    [SerializeField] private Dice pet;

    private Dice[] allDices;

    [SerializeField] private float delay = 0.5f;
    private float tickTime = 0.05f;
    private float flyTime = 1.0f;

    private PositionConverter positionConverter;
    private SwipeDetector swipeDetector;


    [SerializeField] private RectTransform[] dicePlaceHolder;
    [SerializeField] private Transform boardBeforeRollTransform;
    [SerializeField] private Transform boardMiddle;

    private bool isWaitForSwipe = false;
    private Vector2 lastSwipe;

    [Inject]
    private void Construct(PositionConverter _converter, SwipeDetector _swipe)
    {
        positionConverter = _converter;
        swipeDetector = _swipe;
    }


    private void OnEnable()
    {
        swipeDetector.onAnySwipe += OnSwipe;
    }

    private void OnDisable()
    {
        swipeDetector.onAnySwipe -= OnSwipe;

    }

    public void DecoreDice(DiceActionSet hand_1_set, DiceActionSet hand_2_set, DiceActionSet armor_set, DiceActionSet amulet_set, DiceActionSet pet_set)
    {

        hand_1.SetDiceActions(hand_1_set);
        hand_2.SetDiceActions(hand_2_set);
        armor.SetDiceActions(armor_set);
        amulet.SetDiceActions(amulet_set);
        pet.SetDiceActions(pet_set);

        allDices = new Dice[]{ hand_1, hand_2, armor, amulet, pet};

        for (int i = 0; i<allDices.Length; i++)
        {
            Vector3 wait = positionConverter.GetWorldPosition(dicePlaceHolder[i], 2);
            allDices[i].transform.position = wait;

        }

    }

    public void RollDice(UnityAction onDone)
    {
        StartCoroutine(RollRutine(onDone));
    }

    public void UseActions(UnityAction onDone)
    {
        StartCoroutine(UseAllActionsRutine(onDone));
    }
    private IEnumerator UseAllActionsRutine(UnityAction onDone)
    {
        for(int i = 0; i < allDices.Length; i++)
        {
            bool isActionUsed = false;
            allDices[i].UseAction(() => isActionUsed = true);
            while(!isActionUsed) yield return new WaitForSeconds(tickTime);
        }
        yield return null;
        onDone?.Invoke();
    }


    private void SetPositions()
    {

        for(int i = 0; i< allDices.Length; i++)
        {
            Vector3 wait = positionConverter.GetWorldPosition(dicePlaceHolder[i], 2);
            Vector3 board = new Vector3(wait.x, boardBeforeRollTransform.position.y, boardBeforeRollTransform.position.z);
            Vector3 middle = boardMiddle.position;
            allDices[i].SetPositions(wait, board, middle);
        }
    }

    

    private IEnumerator RollRutine(UnityAction onDone)
    {
        SetPositions();

        //On position
        int dicesReady = 0;
        foreach (Dice dice in allDices)
        {
            dice.MoveToBoard(flyTime, () => dicesReady++);
        }
        while (dicesReady!=allDices.Length) yield return new WaitForSeconds(tickTime);
        yield return new WaitForSeconds(delay);

        //wait for swipe
        isWaitForSwipe = true;
        while (isWaitForSwipe) yield return new WaitForSeconds(tickTime);


        //roll in swipe diraction
        dicesReady = 0;
        foreach (Dice dice in allDices)
        {
            dice.RollDice(lastSwipe,()=> dicesReady++);
        }
        while (dicesReady != allDices.Length) yield return new WaitForSeconds(tickTime);

        dicesReady = 0;
        foreach (Dice dice in allDices)
        {
            dice.ReturnDice(flyTime, () => dicesReady++);
        }
        while (dicesReady != allDices.Length) yield return new WaitForSeconds(tickTime);
        onDone?.Invoke();
    }

    private void OnSwipe(Vector2 newSwipe)
    {
        if (isWaitForSwipe)
        {
            lastSwipe = newSwipe;
            isWaitForSwipe = false;
        }
    }
}
