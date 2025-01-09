using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAble : MonoBehaviour, IActionReceiver
{
    [SerializeField] internal HealthController _healthController;

    public virtual void ReceiveAction(DiceAction action)
    {
        
    }



}
