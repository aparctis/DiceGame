using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class DiceSidesManager : MonoBehaviour
{

    [SerializeField] private Side[] sides;

    private DiceActionSet actionSet;
    private ActionDecorPool _actionDecorPool;


    [Inject]
    private void Construct(ActionDecorPool actionDecorPool)
    {
        _actionDecorPool = actionDecorPool;
    }

    public void SetActionSet(DiceActionSet newActionSet)
    {
        actionSet = newActionSet;
        SetDecore();
    }



    private void SetDecore()
    {
        if (!sides.Length.Equals(6)) Debug.LogError("WRONG DICE SIDES LENGTH!");
        for(int i = 0; i < sides.Length; i++)
        {
            sides[i].SetAction(actionSet.actions[i]);
        }
    }


}
