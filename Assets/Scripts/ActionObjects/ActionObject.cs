using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum ActionObjectType { atack=0, armor=1, health=2, poison=3}
public class ActionObject : MonoBehaviour
{
    private ActionObjectPool _pool;

    [SerializeField]private ActionObjectType _type;
    public ActionObjectType type => _type;

    private float moveSpeed = 10.0f;
    private float scaleTime = 1.0f;
    private Ease showScaleEase = Ease.InOutBounce;


    private Sequence sequence;

    public void SetPool(ActionObjectPool pool)=> _pool = pool;


    public void Show()
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.0f, scaleTime).SetEase(showScaleEase));
    }

    public void Show(float scale)
    {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(scale, scaleTime).SetEase(showScaleEase));
    }


    public void MoveToTarget(Vector3 target, Ease moveEase, UnityAction onMoveDone)
    {
        float moveTime = getMoveTime(target);

        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(target, moveTime).SetEase(moveEase)).
            OnComplete(()=>GoToPool(()=>onMoveDone.Invoke()));
    }


    public void GoToPool(UnityAction doBeforeBack)
    {
        doBeforeBack.Invoke();
        _pool.ReturnToPool(this);

    }
    public void GoToPool()
    {
        _pool.ReturnToPool(this);

    }


    private float getMoveTime (Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        return distance / moveSpeed;
    }
}
