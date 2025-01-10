using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ActionObjectPool : MonoBehaviour, IInitializable
{
    /// <summary>
    /// atack=0, armor=1, health=2, poison=3
    /// </summary>
    [SerializeField] private ActionObject damagePrefab;
    [SerializeField] private ActionObject armorPrefab;
    [SerializeField] private ActionObject healthPrefab;
    [SerializeField] private ActionObject poisonPrefab;

    private int poolLength = 5;

    private ActionObject[] prefabs;
    private List<List<ActionObject>> poolList = new List<List<ActionObject>>();

    public void Initialize()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        prefabs = new ActionObject[4];
        prefabs[0] = damagePrefab;
        prefabs[1] = armorPrefab;
        prefabs[2] = healthPrefab;
        prefabs[3] = poisonPrefab;

        for(int i = 0; i<prefabs.Length; i++)
        {
            ActionObject prefab = prefabs[i];
            List<ActionObject> pool = new List<ActionObject>();
            for(int p = 0; p<poolLength; p++)
            {
                ActionObject actionObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                actionObject.transform.localScale = Vector3.zero;
                actionObject.gameObject.SetActive(false);

                actionObject.SetPool(this);

                pool.Add(actionObject);
            }
            poolList.Add(pool);
        }
    }

    public ActionObject getActionObject(ActionObjectType type)
    {
        Debug.Log("asked type = " + type + " (int)type = "+ (int)type);
        if ((int)type >= poolList.Count) return null;
        List<ActionObject> pool = poolList[(int)type];
        ActionObject actionObject;
        if (pool.Count > 0)
        {
            actionObject = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            ActionObject prefab = prefabs[(int)type];
            actionObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            actionObject.transform.localScale = Vector3.zero;
            actionObject.gameObject.SetActive(false);

            actionObject.SetPool(this);
        }
        return actionObject;
    }

    public void ReturnToPool(ActionObject actionObject)
    {
        List<ActionObject> pool = poolList[(int)actionObject.type];
        actionObject.gameObject.SetActive(false);
        actionObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        actionObject.transform.localScale = (Vector3.zero);
        pool.Add(actionObject);
    }


}
