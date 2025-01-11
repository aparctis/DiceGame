using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceSetUI : MonoBehaviour
{
    public static DiceSetUI instance {  get; private set; }

    [SerializeField] private DiceSideUI[] diceSidesUIs;
    [SerializeField] private ActionDiscription[] disriptions;
    [SerializeField] private Image selectedImage;
    [SerializeField] private TextMeshProUGUI selectedDiscription;

    private int selectedSide;

    private Dictionary<ActionType, ActionDiscription> dictionary = new Dictionary<ActionType, ActionDiscription>();


    private DiceActionSet selectedSet;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < diceSidesUIs.Length; i++)
        {
            diceSidesUIs[i].Initialize(i);
        }

        for(int i =0; i< disriptions.Length; i++)
        {
            dictionary.Add(disriptions[i].type, disriptions[i]);
        }
    }

    private void OnEnable()
    {
        foreach (DiceSideUI side in diceSidesUIs) side.onClicked += SelectSide;
    }
    private void OnDisable()
    {
        foreach (DiceSideUI side in diceSidesUIs) side.onClicked -= SelectSide;

    }

    private void SelectSide(int sideIndex)
    {
        if (sideIndex != selectedSide)
        {
            diceSidesUIs[selectedSide].UnSelect();
            diceSidesUIs[sideIndex].Select();
            selectedSide = sideIndex;

            if (selectedSet != null)
            {
                DiceAction action = selectedSet.actions[sideIndex];
                selectedImage.sprite = dictionary[action.type].sprite;
                selectedDiscription.text = getDiscription(action);
            }
            
        }
    }

    public void ShowSet(DiceActionSet set)
    {
        selectedSet = set;
        if (set.actions.Length != diceSidesUIs.Length)
        {
            Debug.LogError("ERROR! DiceActionSet Length dosent metches with diceSidesUIs.Length");
            return;
        }
        for(int i = 0; i<set.actions.Length; i++)
        {
            DiceSideUI sideUI = diceSidesUIs[i];
            DiceAction action = set.actions[i];

            Sprite sprite = dictionary[action.type].sprite;
            sideUI.SetImage(sprite);
        }
    }

    private string getDiscription(DiceAction action)
    {
        string template = dictionary[action.type].description;
        return template.Replace("{value}", action.value.ToString()).
            Replace("{secondValue}", action.secondValue.ToString());

    }
}
