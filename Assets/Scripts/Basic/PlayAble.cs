using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayAble : MonoBehaviour, IActionReceiver
{
    [SerializeField] internal HealthController healthController;
    [SerializeField] internal ArmorController armorController;

    [SerializeField] internal RectTransform _avatarRect;


    public UnityAction onDeath;

    internal PositionConverter positionConverter;

    internal AvatarModel avatarModel;

    [Inject]
    private void Construct(PositionConverter _positionConverter)
    {
        positionConverter = _positionConverter;
    }

    private void OnEnable()
    {
        healthController.onDeath += Death;
    }

    private void OnDisable()
    {
        healthController.onDeath -= Death;

    }

    public Vector3 recieverPosition()
    {
        return positionConverter.GetWorldPosition(_avatarRect, 2.0f);
    }

    public void ApplyPoison(UnityAction onDone)
    {
        healthController.AplyPoisonDamage(onDone);
    }

    public virtual void RollDice(UnityAction onDone)
    {

    }

    public virtual void UseActions(UnityAction onDone)
    {

    }

    public void ReceiveAction(ActionObjectType type, int value)
    {
        switch (type)
        {
            case ActionObjectType.atack:
                ReceiveDamage(value); 
                break;
            case ActionObjectType.poison:
                ReceivePoison(value);
                break;
            case ActionObjectType.health:
                ReceiveHill(value);
                break;
            case ActionObjectType.armor:
                ReceiveArmor(value);
                break;

        }
    }

    internal virtual void ReceiveDamage(int damage)
    {
        int trueDamage = armorController.DamageAfterArmor(damage);
        healthController.Damage(trueDamage);
    }

    internal virtual void ReceivePoison(int poison)
    {
        healthController.Poison(poison);
    }

    internal virtual void ReceiveHill(int hill)
    {
        healthController.Hill(hill);
    }

    internal virtual void ReceiveArmor(int armor)
    {
        armorController.AddArmor(armor);
    }

    internal virtual void Death()
    {
        onDeath?.Invoke();
    }
}
