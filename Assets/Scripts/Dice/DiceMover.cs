using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DiceMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider collider;
    [SerializeField] private Ease kinematicMoveEase = Ease.InOutCubic;


    public float throwForce = 5f;
    public float torqueForce = 10f;
    private float minimalMagnitude = 0.01f;
    private float timeTick = 0.05f;

    private Sequence sequence;

    private Coroutine velocityCheckRutine;

    private void Start()
    {
        if(!rb)rb = GetComponent<Rigidbody>();
        if(!collider)collider = GetComponent<Collider>();
    }

    public void RollDice(Vector2 swipeDirection, UnityAction onDone)
    {
        collider.isTrigger = false;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(swipeDirection.normalized * throwForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(-torqueForce, torqueForce),
            Random.Range(-torqueForce, torqueForce),
            Random.Range(-torqueForce, torqueForce)
        );
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        velocityCheckRutine = StartCoroutine(DiceVelocityCheck(onDone));
    }


    public void RollDice(UnityAction onDone)
    {
        Vector3 throwDirection = Vector3.up + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        RollDice(throwDirection, onDone);

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
        collider.isTrigger = false;
        rb.isKinematic = false;
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
