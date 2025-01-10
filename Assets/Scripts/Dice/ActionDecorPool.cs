using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class ActionDecorPool : MonoBehaviour, IInitializable
{

    [Header("Values (max is 10)")]
    [SerializeField] private List<GameObject> valuePrefabs;
    private List<List<GameObject>> valuePoolList = new List<List<GameObject>>();
    [SerializeField] private int valuesInPoolCount = 1;


    [Header("Actions")]
    [SerializeField] private List<GameObject> decorPrefabs;
    private List<List<GameObject>> decorePoolList = new List<List<GameObject>>();
    [SerializeField] private int decoreInPoolCount = 1;

    public void Initialize()
    {
        CreatePools();
    }


    private void CreatePools()
    {
        //digit pool
        for (int i = 0; i < valuePrefabs.Count; i++)
        {
            GameObject _prefab = valuePrefabs[i];
            List<GameObject> _pool = new List<GameObject>();

            for (int c = 0; c < valuesInPoolCount; c++)
            {
                GameObject _object = Instantiate(_prefab);
                _object.SetActive(false);
                _pool.Add(_object);
            }
            valuePoolList.Add(_pool);
        }

        //action pool
        for (int i = 0; i < decorPrefabs.Count; i++)
        {
            GameObject _prefab = decorPrefabs[i];
            List<GameObject> _pool = new List<GameObject>();

            for (int c = 0; c < decoreInPoolCount; c++)
            {
                GameObject _object = Instantiate(_prefab);
                _object.SetActive(false);
                _pool.Add(_object);

            }

            decorePoolList.Add(_pool);
        }
    }


    public GameObject getActionObject(ActionType type)
    {
        int index = ((int)type);
        GameObject _object = null;
        GameObject _prefab = decorPrefabs[index];
        List<GameObject> _pool = decorePoolList[index];

        if (_pool.Count > 0)
        {
            _object = _pool[0];
            _pool.RemoveAt(0);
        }
        else
        {
            _object = Instantiate(_prefab);
            _object.SetActive(false);
        }

        return _object;
    }



    public void ReturnActionObject(GameObject _object, ActionType type)
    {
        int index = ((int)type);
        List<GameObject> _pool = decorePoolList[index];
        _object.SetActive(false);
        _pool.Add(_object);
    }


    public GameObject getDigitObject(int index)
    {
        GameObject _object = null;
        GameObject _prefab = valuePrefabs[index];
        List<GameObject> _pool = valuePoolList[index];

        if (_pool.Count > 0)
        {
            _object = _pool[0];
            _pool.RemoveAt(0);
        }
        else
        {
            _object = Instantiate(_prefab);
            _object.SetActive(false);
        }

        return _object;
    }

    public void ReturnDigitObject(GameObject _object, int index)
    {
        List<GameObject> _pool = valuePoolList[index];
        _object.SetActive(false);
        _pool.Add(_object);
    }



}
