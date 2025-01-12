using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DiceSetUI : MonoBehaviour
{
    public static DiceSetUI instance {  get; private set; }

    [SerializeField] private CanvasGroup allContent;
    [SerializeField] private CanvasGroup discriptionContent;

    [SerializeField] private DiceSideUI[] diceSidesUIs;
    [SerializeField] private ActionDiscription[] disriptions;
    [SerializeField] private Image selectedImage;
    [SerializeField] private TextMeshProUGUI selectedDiscription;

    private int selectedSideIndex;

    private Dictionary<ActionType, ActionDiscription> dictionary = new Dictionary<ActionType, ActionDiscription>();

    private Sequence sequence;
    private DiceActionSet selectedSet;
    private bool isShowing = false;

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
        allContent.alpha = 0f;
        allContent.interactable = false;
        allContent.blocksRaycasts = false;
        allContent.gameObject.SetActive(false);

        discriptionContent.alpha = 0f;


        for (int i = 0; i < diceSidesUIs.Length; i++)
        {
            diceSidesUIs[i].Initialize(i);
        }

        for(int i =0; i< disriptions.Length; i++)
        {
            dictionary.Add(disriptions[i].type, disriptions[i]);
        }

    }

    private void Show()
    {
        isShowing = true;
        allContent.interactable = true;
        allContent.blocksRaycasts = true;
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(allContent.DOFade(1, 1));
    }

    public void Hide()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(allContent.DOFade(0, 1)).OnComplete(OnComplete);

        void OnComplete()
        {
            discriptionContent.alpha = 0;
            allContent.alpha = 0f;
            allContent.interactable = false;
            allContent.blocksRaycasts = false;
            isShowing = false;
            allContent.gameObject.SetActive(false);
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
        if (sideIndex != selectedSideIndex)
        {
            diceSidesUIs[selectedSideIndex].UnSelect();
            diceSidesUIs[sideIndex].Select();
            selectedSideIndex = sideIndex;

            if (selectedSet != null)
            {
                DiceAction action = selectedSet.actions[sideIndex];
                selectedImage.sprite = dictionary[action.type].sprite;
                selectedDiscription.text = getDiscription(action);

            }
            discriptionContent.alpha = 1f;

        }

    }

    public void ShowSet(DiceActionSet set)
    {
        if (isShowing) return;
        Debug.Log("Show set");
        allContent.gameObject.SetActive(true);
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
        Show();
    }

    private string getDiscription(DiceAction action)
    {
        if (dictionary.ContainsKey(action.type))
        {
            string template = dictionary[action.type].description;
            return template.Replace("{value}", action.value.ToString()).
                Replace("{secondValue}", action.secondValue.ToString());
        }
        else return $"Can`t find info about {action.type}";


    }
}
