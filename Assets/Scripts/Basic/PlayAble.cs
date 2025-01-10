using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAble : MonoBehaviour, IActionReceiver
{
    [SerializeField] internal HealthController healthController;
    [SerializeField] internal ArmorController armorController;

    internal AvatarModel avatarModel;

    public virtual void ReceiveAction(DiceAction action)
    {
        
    }



}
