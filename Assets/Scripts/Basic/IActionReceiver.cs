using UnityEngine;

public interface IActionReceiver
{
    public void ReceiveAction(ActionObjectType type, int value);

    public Vector3 recieverPosition();
}
