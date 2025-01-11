using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Zenject;
using System;
using System.IO;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public DescriptionData[] datas;
    private Dictionary<string, int> disctionary = new Dictionary<string, int>
    {
        {"value", 0 },
        {"secondValue", 0 }
    };


}

[System.Serializable]
public class DescriptionData
{
    public Sprite sprite;
    public string description;
}





