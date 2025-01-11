using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DiceMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider collider;
    [SerializeField] private Ease kinematicMoveEase = Ease.InOutCubic;

    private float minimalMagnitude = 0.01f;
    private float timeTick = 0.05f;



    [SerializeField] private float force_min = 2.0f;
    [SerializeField] private float force_max =12.0f;

    private Sequence sequence;

    private Coroutine velocityCheckRutine;

    private void Start()
    {
        if(!rb)rb = GetComponent<Rigidbody>();
        if(!collider)collider = GetComponent<Collider>();
    }

    private void RollDice(Vector3 direction, float force, UnityAction onDone)
    {
        collider.isTrigger = false;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction * force, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(-1, 1),
            Random.Range(-1, 1),
            Random.Range(-1, 1)
        )*force;
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        velocityCheckRutine = StartCoroutine(DiceVelocityCheck(onDone));
    }

    //for roll on swipe
    public void RollDice(Vector2 swipe, UnityAction callBack)
    {
        //convert direction from canvas to world and add up direction
        Vector3 worldSwipeDirection = new Vector3(swipe.x, 0, swipe.y).normalized;
        Vector3 throwDirection = (worldSwipeDirection+Vector3.up).normalized;

        //set force depends of swipe langth
        float swipeLength = swipe.magnitude;
        float screenMinimalLength = Mathf.Min(Screen.width, Screen.height);
        float swipeForce = Mathf.Clamp((swipeLength/screenMinimalLength), 0, 1.0f);
        float force = Mathf.Lerp(force_min, force_max, swipeForce);

        RollDice(throwDirection, force, callBack);
    }

    //for random roll
    public void RollDice(UnityAction onDone)
    {
        Vector3 throwDirection = Vector3.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        float throwForce = Random.Range(force_min, force_max);
        RollDice(throwDirection, throwForce, onDone);

    }

    public void MoveDice(Vector3 targetPosition, bool isRandomRotation, float moveTime, UnityAction onMoveDone)
    {
        Vector3 targetRotation;
        if(isRandomRotation) targetRotation = new Vector3(randomAngle(), randomAngle(), randomAngle());
        else targetRotation = transform.rotation.eulerAngles;

        MoveDice(targetPosition, targetRotation, moveTime, onMoveDone);
    }


    public void MoveDice(Vector3 targetPosition, Vector3 targetRotation, float moveTime, UnityAction onMoveDone)
    {
        collider.isTrigger = true;
        rb.isKinematic = true;
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(targetPosition, moveTime).SetEase(kinematicMoveEase))
            .AppendInterval(0.1f)
            .Append(transform.DORotate(targetRotation, moveTime)).
            OnComplete(() => onMoveDone.Invoke());
    }

    private IEnumerator DiceVelocityCheck(UnityAction onDone)
    {
        yield return new WaitForSeconds(timeTick);
        while (rb.velocity.magnitude > minimalMagnitude || rb.angularVelocity.magnitude > minimalMagnitude) yield return new WaitForSeconds(timeTick);
        onDone?.Invoke();
    }

    private float randomAngle() => Random.Range(0, 360.0f);
}
