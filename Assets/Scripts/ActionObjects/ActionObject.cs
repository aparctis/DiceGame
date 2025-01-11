using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum ActionObjectType { atack=0, armor=1, health=2, poison=3}
public class ActionObject : MonoBehaviour
{
    private ActionObjectPool _pool;
    [SerializeField] private GameObject visual;
    [SerializeField]private ActionObjectType _type;
    public ActionObjectType type => _type;
    
    private float scaleTime = 1.0f;
    private Ease showScaleEase = Ease.InOutCubic;

    private float flyTime = 1.0f;
    private Ease flyEase = Ease.InBack;


    private Sequence sequence;

    public void SetPool(ActionObjectPool pool)=> _pool = pool;

    public void Show()
    {
        visual.SetActive(true);
    }

    public void Hide()
    {
        visual.SetActive(false);
    }

    public void ApplyAction(int value, Vector3 middlePoint, IActionReceiver reciever, Vector3 targetPosition, bool isEnemyDice, UnityAction callBack)
    {
        Debug.Log("Action object Apply Action");
        gameObject.SetActive(true);

        Vector3 targetRotation = Vector3.zero;
        if (isEnemyDice) targetRotation = new Vector3(0, 180, 0);

        sequence?.Kill();
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(middlePoint, scaleTime).SetEase(Ease.InOutCubic)).
            Join(transform.DOScale(1, scaleTime)).SetEase(showScaleEase).
            Join(transform.DORotate(targetRotation, scaleTime)).
            Append(transform.DOMove(targetPosition, flyTime)).SetEase(flyEase).
            OnComplete(OnComplete);

        void OnComplete()
        {
            Debug.Log("Action object OnComplete");

            reciever.ReceiveAction(_type, value);
            callBack?.Invoke();
            _pool.ReturnToPool(this);
        }
    }

    public void GoToPool()
    {
        _pool.ReturnToPool(this);

    }

}
